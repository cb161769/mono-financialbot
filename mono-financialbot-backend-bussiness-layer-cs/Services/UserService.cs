
namespace mono_financialbot_backend_bussiness_layer_cs.Services
{
    public class UserService : IUser
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly JsonWebToken _jwt;
        public UserService(ApplicationDbContext context,
            IMapper mapper,
            UserManager<User> userManager, ILogger<UserService> logger, JsonWebToken jwt)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
            _jwt = jwt;

        }
        public async Task<ResponseBase> Login(LoginDto input)
        {
            try
            {
                _logger.LogInformation($" start process to login username: {input.UserName} ");

                var user = await _userManager.FindByNameAsync(input.UserName);
                if (user != null)
                {
                    _logger.LogInformation($" username: {input.UserName} found in the database, proceed to check password ");
                    var isPasswordChecked = await _userManager.CheckPasswordAsync(user, input.Password);
                    if (isPasswordChecked)
                    {
                        _logger.LogInformation($"  username: {input.UserName} password checked succesfully, proceed to generate JWT ");
                        var signingCredentials = _jwt.GetSigningCredentials();
                        var claims = _jwt.GetClaims(user);
                        var tokenOptions = _jwt.GenerateTokenOptions(signingCredentials, claims);
                        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                        return new ResponseBase
                        {
                            IsSuccess = true,
                            Token = token
                        };

                    }
                    else
                    {
                        _logger.LogWarning($"  username: {input.UserName} Password verification unsuccesfull ");

                        return new ResponseBase
                        {
                            IsSuccess = false,
                            Message = "Password verification unsuccesfull"
                        };
                    }
                }
                else
                {
                    _logger.LogWarning($"  username: {input.UserName} User is not found ");

                    return new ResponseBase
                    {
                        IsSuccess = false,
                        Message = "User is not found"
                    };
                }

            }
            catch (Exception e)
            {

                _logger.LogError($" an interal error occured while process to login username: {input.UserName}, error: {e.ToString()} ");

                throw new Exception(e.Message);
            }
        }

        public async Task<ResponseBase> Register(UserRegistrationDto input)
        {
            try
            {
                _logger.LogInformation($" start process to register username: {input.UserName} ");
                User user = new User();
                user.UserName = input.UserName;
                user.Email = "email@gmail.com";
                //var user = _mapper.Map<User>(user);
                _logger.LogInformation($" succesfully mapped information related to user: {input.UserName} ");
                IdentityResult result = await _userManager.CreateAsync(user, input.Password);
                if (!result.Succeeded)
                {
                    _logger.LogWarning($"an error occured while registering user: {result.Errors.ToString()}");
                    var errors = result.Errors.Select(e => e.Description);
                    return new ResponseBase { IsSuccess = false, Message = errors.ToString()  };

                }
                else
                {
                    _logger.LogInformation($" process to register username: {input.UserName} was successful ");

                    return new ResponseBase { IsSuccess = true, Message = result.ToString() };

                }

            }
            catch (Exception e)
            {
                _logger.LogError($" an interal error occured while process to register username: {input.UserName}, error: {e.ToString()} ");

                throw new Exception(e.Message);
            }
        }
    }
}
