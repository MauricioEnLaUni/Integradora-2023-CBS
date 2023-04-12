using Microsoft.AspNetCore.Authentication.JwtBearer;

using Fictichos.Constructora.Repository;
using Fictichos.Constructora.Utilities;
using Fictichos.Constructora.Utilities.MongoDB;
using Fictichos.Constructora.Options;
using Fictichos.Constructora.Auth;
using Fictichos.Constructora.Abstraction;

DotEnvManager env = new();

EntityMapper.MapClasses();

var builder = WebApplication.CreateBuilder(args);
// var cs = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();

// Add services to the container.
builder.Services.AddSingleton(serviceProvider => 
{
    return new MongoSettings();
});

builder.Services.AddSingleton<IJwtProvider, JwtProvider>();
builder.Services.AddSingleton<TokenService>();
builder.Services.AddSingleton<ExternalPersonService>();
builder.Services.AddSingleton<CompanyService>();
builder.Services.AddSingleton<AreaService>();
builder.Services.AddSingleton<FTaskService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<ProjectService>();
builder.Services.AddSingleton<EmailService>();
builder.Services.AddSingleton<TimeTrackerService>();
builder.Services.AddSingleton<PersonService>();
builder.Services.AddSingleton<MaterialCategoryService>();
builder.Services.AddSingleton<MaterialService>();
builder.Services.AddSingleton<AccountService>();

var AllowOrigins = "_allowOrigins";
builder.Services.AddCors(options => 
{
    options.AddPolicy(name: AllowOrigins,
    policy => 
    {
        policy.WithOrigins("http://127.0.0.1:5173")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

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
