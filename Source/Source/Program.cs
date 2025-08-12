using Source.Features.Settings;

var builder = WebApplication.CreateBuilder(args);
#region ConfigureServices
builder.Services.RegisterServices();
#endregion
var app = builder.Build();
app.MapCarter();

app.Run();
