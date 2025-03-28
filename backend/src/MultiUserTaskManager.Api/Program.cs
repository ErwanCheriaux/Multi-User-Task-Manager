using Microsoft.EntityFrameworkCore;
using MultiUserTaskManager.Api.Data;
using MultiUserTaskManager.Api.Entities;
using MultiUserTaskManager.Api.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowOrigin",
        builder =>
            builder.WithOrigins(allowedOrigins).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<User>();
app.UseCors("AllowOrigin");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
