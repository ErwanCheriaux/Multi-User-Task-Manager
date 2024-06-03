using Microsoft.EntityFrameworkCore;
using MultiUserTaskManager.Api.Data;
using MultiUserTaskManager.Api.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlServersettings = builder
    .Configuration.GetSection(nameof(SqlServerSettings))
    .Get<SqlServerSettings>();
    
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(sqlServersettings?.ConnectionString)
);

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
