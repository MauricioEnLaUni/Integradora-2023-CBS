using MongoDB.Driver;
using MongoDB.Driver.Linq;

using Fictichos.Constructora.Utilities;

DotEnvManager env = new();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMongoClient>(serviceProvider => 
{
    var settings = MongoClientSettings.FromConnectionString(
      Environment.GetEnvironmentVariable("MONGODB__SECRET")
    );
    settings.LinqProvider = LinqProvider.V3;
    return new MongoClient(settings);
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
