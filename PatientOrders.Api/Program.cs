using System.Data;
using System.Text.Json.Serialization;
using Npgsql;
using PatientOrders.Api;
using PatientOrders.Api.Repos;
using PatientOrders.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = 
        JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// move the connection string to vault in production env
const string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=2270";

builder.Services.AddScoped<IDbConnection>(_ =>
{
    var connection = new NpgsqlConnection(connectionString);
    connection.Open();
    return connection;
});

builder.Services.AddScoped<IPatientOrderRepo, PatientOrderRepo>();
builder.Services.AddTransient<IPatientOrderService, PatientOrderService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
