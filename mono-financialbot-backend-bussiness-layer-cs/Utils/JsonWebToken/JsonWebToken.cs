
namespace mono_financialbot_backend_bussiness_layer_cs.Utils.JsonWebToken
{
    public class JsonWebToken
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        private readonly ILogger<JsonWebToken> _logger;
        public JsonWebToken(IConfiguration configuration, ILogger<JsonWebToken> logger)
        {
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JsonWebToken");
            _logger = logger;
        }
        public SigningCredentials GetSigningCredentials()
        {
            try
            {
                _logger.LogInformation($" start process to sing JWT credentials ");

                var skey = _jwtSettings.GetSection("key").Value;
            var key = Encoding.UTF8.GetBytes(skey);
            var secret = new SymmetricSecurityKey(key);
                _logger.LogInformation($" finish process to sing JWT credentials succesfull ");
                return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
            }
            catch (Exception e)
            {
                _logger.LogError($" process to sing JWT credentials failed, details: {e.ToString()} ");

                throw new Exception(e.Message);
            }
        }

        public List<Claim> GetClaims(IdentityUser user)
        {
            _logger.LogInformation($" start process to get Claims username: {user.UserName} ");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName, ClaimValueTypes.String),
                new Claim("username", user.UserName, ClaimValueTypes.String)
            };
            _logger.LogInformation($" finished process to get Claims username: {user.UserName} ");

            return claims;
        }

        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            _logger.LogInformation($" start process to get GenerateTokenOptions ");

            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings.GetSection("Issuer").Value,
                audience: _jwtSettings.GetSection("Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection("expirationMinutes").Value)),
                signingCredentials: signingCredentials);
            _logger.LogInformation($"  process to get GenerateTokenOptions finished succesfully ");

            return tokenOptions;

        }
    }
}
