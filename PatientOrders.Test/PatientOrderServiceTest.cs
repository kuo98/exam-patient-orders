using NSubstitute;
using NUnit.Framework;
using PatientOrders.Api;
using PatientOrders.Api.Models;
using PatientOrders.Api.Repos;
using PatientOrders.Api.Services;

namespace PatientOrders.Test;

[TestFixture]
public class PatientOrderServiceTest
{
    private IPatientOrderRepo _patientOrderRepo = null!;
    private PatientOrderService _patientOrderService = null!;

    [SetUp]
    public void SetUp()
    {
        _patientOrderRepo = Substitute.For<IPatientOrderRepo>();
        _patientOrderService = new PatientOrderService(_patientOrderRepo);
    }

    [Test]
    public void CreateNewOrder_WhenPatientDoesNotExist_ThrowsBadRequest()
    {
        var order = new Order
        {
            OrderId = 1,
            OrderMessage = "Add medication",
            PatientId = 99
        };
        _patientOrderRepo.GetPatients().Returns(Task.FromResult(new List<Patient>()));

        Assert.ThrowsAsync<BadRequestException>(() => _patientOrderService.CreateNewOrder(order));
        _patientOrderRepo.DidNotReceive().InsertOrder(Arg.Any<Order>());
    }

    [Test]
    public async Task CreateNewOrder_WhenPatientExists_InsertsOrder()
    {
        var order = new Order
        {
            OrderId = 1,
            OrderMessage = "Add medication",
            PatientId = 99
        };
        _patientOrderRepo.GetPatients().Returns(Task.FromResult(new List<Patient>
        {
            new()
            {
                PatientId = 99,
                PatientName = "Existing Patient"
            }
        }));

        await _patientOrderService.CreateNewOrder(order);

        await _patientOrderRepo.Received(1).InsertOrder(order);
    }
}