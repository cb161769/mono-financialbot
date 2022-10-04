
namespace mono_financialbot_front_end_ts.Extensions
{
    public static class ProgramExtensions
    {
        /// <summary>
        /// Represent a dbContext configuration
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configuration">IConfiguration</param>
        public static void AddDataBaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "Database"), ServiceLifetime.Scoped, ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Add a configuration of automapper
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public static void AddAutomapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CommonProfile));
        }


        public static void AddIdentityWrapper(this IServiceCollection services)
        {
            services.AddDefaultIdentity<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

        }

        public static void AddNewServices(this IServiceCollection services)
        {

            services.AddScoped<IUser, UserService>();
            services.AddScoped<JsonWebToken>();
            services.AddSingleton<IRabbitMQMessageSender, RabbitMQService>();
            services.AddHostedService<RabbitMQRecieverService>();
        }
        public static void AddSecurty(this IServiceCollection services, IConfiguration configuration)
        {
            var securitySettings = configuration.GetSection("Security").Get<SecurityModel>();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder.WithOrigins(securitySettings.Cors));
                options.AddDefaultPolicy(builder => builder.WithOrigins("http://localhost:4200"));

                options.AddPolicy("AllowAllPolicy",
                    builder =>
                    {
                        builder
                        .WithOrigins("http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod().AllowCredentials();

                    });
            });
        }

        public static void AddJsonWebTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JsonWebToken");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                    ValidAudience = jwtSettings.GetSection("Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("Key").Value))
                };
            });

        }
        public static void AddRabbitMQConfiguration(this IServiceCollection service, IConfiguration configuration)
        {
            var config = configuration.GetSection("Rabbit");
            service.Configure<RabbitMQConfiguration>(config);
        }
    }
}
