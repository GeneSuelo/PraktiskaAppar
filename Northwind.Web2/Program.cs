using Northwind.DataContext.SqlServer;
using Northwind.EntityModels;

#region Konfigurera web server host och tjänster
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddNorthwindContext();

var app = builder.Build();
#endregion

#region Konfiguration av HTTP pipelione och routing
if (!app.Environment.IsDevelopment()) 
{ 
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapRazorPages();

app.MapGet("/hello", () => $"Environment is {app.Environment.EnvironmentName}");
#endregion

app.Run();
WriteLine("Detta exekveras efter att web server har stoppats!");
