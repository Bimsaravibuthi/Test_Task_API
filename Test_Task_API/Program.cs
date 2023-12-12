using Microsoft.IdentityModel.Tokens;
using System.Text;
using Test_Task_API.BLL;
using Test_Task_API.Helpers;
using Test_Task_API.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication("JwtAuth")
    .AddJwtBearer("JWTAuth", options =>
    {
        var keyBytes = Encoding.UTF8.GetBytes(Constants.SecretKey);
        var key = new SymmetricSecurityKey(keyBytes);

        options.TokenValidationParameters = new()
        {
            ValidIssuer = Constants.Issuer,
            ValidAudience = Constants.Audiance,
            IssuerSigningKey = key
        };
    });

builder.Services.AddScoped<IUserRepository, UserLogic>();
builder.Services.AddScoped<ITaskRepository, TaskLogic>();

builder.Services.AddControllers().AddNewtonsoftJson();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
