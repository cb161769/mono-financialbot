



namespace mono_financialbot_backend_cs_external_serivces.Providers.Hubs.Services
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatService : Hub, IChatGroup
    {
        private readonly IRabbitMQMessageSender _sender;
        private static IList<ChatGroups> _groups = new List<ChatGroups>();
        private static IList<ChatUsers> _connectedUsers = new List<ChatUsers>();
        private static IList<RabbitMQMessage> _mesages = new List<RabbitMQMessage>();
        private readonly ILogger<ChatService> _logger;
        public ChatService(IRabbitMQMessageSender sender, ILogger<ChatService> logger)
        {
            _sender = sender;
            _logger = logger;
        }
        public async Task Add(string group)

        {
            try
            {
                _logger.LogInformation("Starting operation to Add messages to group");
                var found = _groups.Any(individuals => individuals.name == group);
                if (!found)
                {
                    _logger.LogInformation($"The group of name {group}, was not found, proceed to add to the list of group messages");
                    _groups.Add(new ChatGroups { name = group });
                }
                 await Clients.All.SendAsync(Events.GroupChanged, _groups);
                _logger.LogInformation($"The group of name {group}, was changed, information regarding the event whas sent succesfully");



            }
            catch (Exception e)
            {
                _logger.LogError("An internal error has occurred while a if the adding a new group, error", e);

                throw new Exception(e.Message);
            }
        }

        public void AddToCurrentMessage(RabbitMQMessage message)
        {
            try
            {
                _logger.LogInformation("Starting operation to AddToCurrentMessage {message} to group");
                _mesages.Add(message);
                if (_mesages.Count() > 50)
                {
                    _logger.LogInformation("There were more than 50 messages, proceed to delete the first message");
                    _mesages.RemoveAt(0);

                }
            }
            catch (Exception e )
            {
                _logger.LogError("an internal error has occurred while checking if the connection was available, error", e);
                throw new Exception(e.Message);
            }
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogInformation("proceed to disconnect chatHub", exception);

            return base.OnDisconnectedAsync(exception);
        }

        public async Task Disconnect(string group)
        {
            try
            {
                _logger.LogInformation("Starting operation to disconnect users to group");
                var users = _connectedUsers.Any(user => user.username == group);
                if (users)
                {
                    _logger.LogInformation("Users found, proceed to disconnect");
                    _connectedUsers = _connectedUsers.Where(actualUser => actualUser.username != group).ToList();
                    _logger.LogInformation("procced to update the list of connected users");

                    await Clients.All.SendAsync(Events.UsersChanged, _connectedUsers);
                    _logger.LogInformation("Event was updated succesfully");

                }
                else
                {
                    _logger.LogInformation("Users was not found, can't disconnect");

                }

            }
            catch (Exception e)
            {

                _logger.LogError("An internal error has occurred while a if the Save  messages group, error", e);
                throw new Exception(e.Message);

            }
        }

        public void Save(RabbitMQMessage message)
        {
            try
            {
                _logger.LogInformation("Starting operation to Save messages to group, proceed to invoke the AddToCurrentMessage method");

                AddToCurrentMessage(message);
                _logger.LogInformation(" operation to Save messages to group was finshed succesfully");
            }
            catch (Exception e)
            {

                _logger.LogError("An internal error has occurred while a if the Save  messages group, error", e);

                throw new Exception(e.Message);
            }
        }

        public async Task SendMessage(RabbitMQMessage message)
        {
            try
            {
                _logger.LogInformation("Starting operation to send messages to group");

                await Clients.All.SendAsync(Events.NewMessage, message);
                _logger.LogInformation("Information was sent to the client, proceed to invoke the AddToCurrentMessage method");

                AddToCurrentMessage(message);
                if (message.Message.Contains("/stock="))
                {
                    _logger.LogInformation("[/stock=] command was identified, proceed to send information to rabbitMQ");
                    _sender.SendMessage(message);
                    _logger.LogInformation("[/stock=] information sent to rabbitMQ succesfully");
                }

            }
            catch (Exception e)
            {
                _logger.LogError("An internal error has occurred while a if the Save  messages group, error", e);

                throw new Exception(e.Message);
            }
        }
        public override  Task OnConnectedAsync()
        {
            try
            {
                _logger.LogInformation("Starting operation to get connection");

                string user = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;
                var connectedUsers = _connectedUsers.Any(actualUser => actualUser.username.ToLower() == user.ToLower());
                if (!connectedUsers)
                {
                    _logger.LogInformation("[connectedUsers] was identified, proceed to add users to list");
                    _connectedUsers.Add(new ChatUsers { ConnectionId = new Guid(connectionId), username = user });
                    _logger.LogInformation("user {user} added to list, proceed to notify information to rabbitMQ");
                    Clients.All.SendAsync(Events.UsersChanged, _connectedUsers);
                    _logger.LogInformation("event 'User changed' was sent to rabbitMQ succesfully");

                }
                else
                {
                    Clients.Caller.SendAsync(Events.UsersChanged, _connectedUsers);
                    _logger.LogInformation("event 'User changed' was sent to rabbitMQ succesfully");
                }
                Clients.Caller.SendAsync(Events.ActualMessages, _mesages);
                _logger.LogInformation("event 'ActualMessages' was sent to rabbitMQ succesfully");
                return base.OnConnectedAsync();

            }
            catch (Exception e)
            {
                _logger.LogError("An internal error has occurred while a if the Save  messages group, error", e);
                throw new Exception(e.Message);
            }
        }
    }
}
