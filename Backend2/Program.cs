using Fictichos.Constructora.Utilities;

DotEnvManager env = new();

var builder = WebApplication.CreateBuilder(args);
// var cs = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();

// Add services to the container.
builder.Services.AddSingleton(serviceProvider => 
{
    Console.WriteLine();
    return new MongoSettings();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
