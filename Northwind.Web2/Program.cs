var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


#region Konfiguration av HTTP pipelione och routing
if (!app.Environment.IsDevelopment()) 
{ 
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!");
#endregion

app.Run();
WriteLine("Detta exekveras efter att web server har stoppats!");
