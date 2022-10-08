






using mono_financialbot_backend_cs_external_serivces.Providers.Hubs.Services;

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
builder.Services.AddDataBaseConfiguration(builder.Configuration);
builder.Services.AddSecurty(builder.Configuration);
builder.Services.AddJsonWebTokenAuthentication(builder.Configuration);
builder.Services.AddIdentityWrapper();
builder.Services.AddControllers();
builder.Services.AddAutomapperConfiguration();
builder.Services.AddNewServices();
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
app.UseStaticFiles();
app.UseRouting();
app.UseCors(builder =>
builder.WithOrigins("http://localhost:4200")
      .AllowAnyHeader()
      .AllowAnyMethod()
      .AllowCredentials());
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
    endpoints.MapHub<ChatService>("/hub/chat");
});

app.MapControllers();


//app.UseAuthorization();

app.Logger.LogInformation("Starting app");

app.Run();
