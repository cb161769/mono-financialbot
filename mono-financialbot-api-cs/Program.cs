






var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region configuration
builder.Services.AddLogging();
builder.Services.AddSignalR(opt => { opt.ClientTimeoutInterval = TimeSpan.FromMinutes(60); opt.KeepAliveInterval = TimeSpan.FromMinutes(30); }).AddJsonProtocol();

var configuration = builder.Configuration.GetSection("Rabbit");
builder.Services.Configure<RabbitMQConfiguration>(configuration);
builder.Services.AddSingleton<IBot, StockService>();
builder.Services.AddSingleton<IRabbitMQMessageSender, RabbitMQService>();
builder.Services.AddHostedService<RabbitMQRecieverService>();
#endregion
var app = builder.Build();
app.Logger.LogInformation("Services succesfully instantained");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.Logger.LogInformation("[development mode enabled], starting to add swagger");
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Logger.LogInformation("Starting app");

app.Run();
