using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("LocalDatabase"));
app.MapGet("/", () => "Hello World!");

app.Run();
