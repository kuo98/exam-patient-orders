using System.Data;
using Dapper;
using PatientOrders.Api.Entities;
using PatientOrders.Api.Mappers;
using PatientOrders.Api.Models;

namespace PatientOrders.Api.Repos;

public interface IPatientOrderRepo
{
    Task<List<PatientOrder>> GetPatientOrders();
    Task InsertPatient(Patient patient);
    Task InsertOrder(Order order);
    Task<List<Patient>> GetPatients();
}

public class PatientOrderRepo : IPatientOrderRepo
{
    private readonly IDbConnection _dbConnection;

    private const string InsertPatientSql = "INSERT INTO patients (name) VALUES (@Name)";
    private const string InsertOrderSql = "INSERT INTO orders (message, patient_id) VALUES (@Message, @PatientId)";

    private const string SelectPatientSql = """
                                            SELECT  
                                                id AS Id,
                                                name AS Name
                                            FROM patients
                                            """;
    
    private const string SelectPatientOrdersSql = """
                                                  SELECT 
                                                      p.id AS PatientId,
                                                      p.name AS PatientName,
                                                      o.id AS OrderId,
                                                      o.message AS OrderMessage
                                                  FROM patients p
                                                  LEFT JOIN orders o ON p.id = o.patient_id
                                                  """;
    
    public PatientOrderRepo(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<List<PatientOrder>> GetPatientOrders()
    {
        var patientOrders = await _dbConnection.QueryAsync<PatientOrder>(SelectPatientOrdersSql);
        return patientOrders.ToList();
    }

    public async Task InsertPatient(Patient patient)
    {
        await _dbConnection.ExecuteAsync(InsertPatientSql, new { Name = patient.PatientName });
    }

    public async Task InsertOrder(Order order)
    {
        await _dbConnection.ExecuteAsync(InsertOrderSql, new { Message = order.OrderMessage, PatientId = order.PatientId });
    }

    public async Task<List<Patient>> GetPatients()
    {
        var patientEntities = await _dbConnection.QueryAsync<PatientEntity>(SelectPatientSql);
        return patientEntities.Select(x => x.ToDomain()).ToList();
    }
}
