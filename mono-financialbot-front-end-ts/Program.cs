


var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add services to the container.
// Add services to the container.
#region Configuration of services
builder.Services.AddDataBaseConfiguration(builder.Configuration);
builder.Services.AddIdentityWrapper();
builder.Services.AddControllersWithViews();
builder.Services.AddAutomapperConfiguration();
builder.Services.AddNewServices();
builder.Services.AddSignalR(opt => { opt.ClientTimeoutInterval = TimeSpan.FromMinutes(60); opt.KeepAliveInterval = TimeSpan.FromMinutes(30); }).AddJsonProtocol();

builder.Services.AddSecurty(builder.Configuration);
builder.Services.AddJsonWebTokenAuthentication(builder.Configuration);
builder.Services.AddRabbitMQConfiguration(builder.Configuration);

#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAllPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
    endpoints.MapHub<ChatService>("/hub/chat");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;
app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";

    if (app.Environment.IsDevelopment())
    {
        spa.UseAngularCliServer(npmScript: "start");
    }
});
app.Run();
