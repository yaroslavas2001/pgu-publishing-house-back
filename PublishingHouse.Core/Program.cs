using PublishingHouse.Helpers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.InjectDependencies(builder.Configuration);

var app = builder.Build();
app.ConfigureApplication();
app.Run();