using Cafetown.DL;
using Cafetown.BL;
using Cafetown.API.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Turn on CORs for localhost
builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

// Dependency Injection 
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));

// Phân hệ nhân viên
builder.Services.AddScoped<IEmployeeBL, EmployeeBL>();
builder.Services.AddScoped<IEmployeeDL, EmployeeDL>();

builder.Services.AddScoped<IConnectionDL, MySqlConnectionDL>();

var app = builder.Build();

// Get connection string from file appsettings.Development.json
DataContext.ConnectionString = builder.Configuration.GetConnectionString("MySql");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Dùng CORs
app.UseCors("MyCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
