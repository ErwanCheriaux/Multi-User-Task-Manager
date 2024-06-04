using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MultiUserTaskManager.Api.Data;
using MultiUserTaskManager.Api.Entities;
using MultiUserTaskManager.Api.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowOrigin",
        builder =>
            builder
                .WithOrigins("http://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
    );
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlServersettings = builder
    .Configuration.GetSection(nameof(SqlServerSettings))
    .Get<SqlServerSettings>();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(sqlServersettings?.ConnectionString)
);

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<User>();

// return user information based on cookie
app.MapGet(
        "/status",
        (ClaimsPrincipal user) =>
        {
            var email = user.FindFirstValue(ClaimTypes.Email);
            var firstname = user.FindFirstValue(ClaimTypes.GivenName);
            var lastname = user.FindFirstValue(ClaimTypes.Surname);
            return Results.Json(
                new
                {
                    email,
                    firstname,
                    lastname
                }
            );
        }
    )
    .RequireAuthorization();

app.UseCors("AllowOrigin");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
