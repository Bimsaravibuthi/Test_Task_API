using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication("JwtAuth")
    .AddJwtBearer("JWTAuth", options =>
    {
        var keyBytes = Encoding.UTF8.GetBytes("Mokak hari text ekak.");
        var key = new SymmetricSecurityKey(keyBytes);

        options.TokenValidationParameters = new()
        {
            ValidIssuer = "Test_Task_API",
            ValidAudience = "Test_Task_CRUD",
            IssuerSigningKey = key
        };
    });

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
