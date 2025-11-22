using PatientOrders.Api.Models;
using PatientOrders.Api.Repos;

namespace PatientOrders.Api.Services;

public interface IPatientOrderService
{
    Task<List<PatientOrder>> GetPatientOrders();
    Task CreateNewPatient(Patient patient);
    Task CreateNewOrder(Order order);
}

public class PatientOrderService : IPatientOrderService
{
    private readonly IPatientOrderRepo _patientOrderRepo;

    public PatientOrderService(IPatientOrderRepo patientOrderRepo)
    {
        _patientOrderRepo = patientOrderRepo;
    }

    public async Task<List<PatientOrder>> GetPatientOrders()
    {
        return await _patientOrderRepo.GetPatientOrders();
    }

    public async Task CreateNewPatient(Patient patient)
    {
        await _patientOrderRepo.InsertPatient(patient);
    }

    public async Task CreateNewOrder(Order order)
    {
        var patients = await _patientOrderRepo.GetPatients();
        if (patients.All(x => x.PatientId != order.PatientId))
        {
            throw new BadRequestException("Patient not found");
        }
        
        await _patientOrderRepo.InsertOrder(order);
    }
}
