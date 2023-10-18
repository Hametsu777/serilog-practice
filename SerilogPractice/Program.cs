using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Need to configure Serilog before it can be used. Look up Serilig documentation to learn more.
// The file path is in my application folder. A folder named logs is created and a file named myBeautifulLog (log file)
// with a timestamp is created. Can open file with vsCode, format with prettier and Json to turn into Json objects.
// builder.Configuration is for appsetings.json file.
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Host.UseSerilog();
//.MinimumLevel.Information()
//.WriteTo.Console()
//.WriteTo.File("logs/myBeautifulLog-.txt", rollingInterval: RollingInterval.Day)
//.CreateLogger();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// UseSeriLogRequest gives a lot more information. In order to use it, need to add builder.Host.UseSeriLog().
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
