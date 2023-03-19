using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;

DotEnvManager env = new();

var builder = WebApplication.CreateBuilder(args);
// var cs = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();

// Add services to the container.
builder.Services.AddSingleton(serviceProvider => 
{
    return new MongoSettings();
});
builder.Services.AddSingleton<UserService>();

var AllowOrigins = "_allowOrigins";
builder.Services.AddCors(options => 
{
    options.AddPolicy(name: AllowOrigins,
    policy => 
    {
        policy.WithOrigins("*");
    });
});

builder.Services.AddAuthentication(Constants.COOKIENAME).AddCookie(Constants.COOKIENAME, options =>
{
    options.Cookie.Name = Constants.COOKIENAME;
});

builder.Services.AddControllers()
    .AddNewtonsoftJson();
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

app.UseCors(AllowOrigins);
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
