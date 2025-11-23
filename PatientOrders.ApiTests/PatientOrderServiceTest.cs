using NSubstitute;
using PatientOrders.Api;
using PatientOrders.Api.Models;
using PatientOrders.Api.Repos;
using PatientOrders.Api.Services;

namespace PatientOrders.ApiTests;

public class Tests
{
    private IPatientOrderRepo _patientOrderRepo;
    private PatientOrderService _patientOrderService;
    [SetUp]
    public void Setup()
    {
        _patientOrderRepo = Substitute.For<IPatientOrderRepo>();
        _patientOrderService = new PatientOrderService(_patientOrderRepo);
    }

    [Test]
    public void Should_Throw_Exception_If_Patient_Does_Not_Exist()
    {
        var order = new Order
        {
            OrderId = 1,
            OrderMessage = "OrderMessage",
            PatientId = 99
        };
        _patientOrderRepo.GetPatients().Returns([]);

        Assert.ThrowsAsync<BadRequestException>(() => _patientOrderService.CreateNewOrder(order));
        _patientOrderRepo.DidNotReceive().InsertOrder(Arg.Any<Order>());
    }
    
    [Test]
    public async Task Should_Insert_Order_If_Patient_Exists()
    {
        var order = new Order
        {
            OrderId = 1,
            OrderMessage = "OrderMessage",
            PatientId = 99
        };
        _patientOrderRepo.GetPatients().Returns([
            new Patient
            {
                PatientId = 99,
                PatientName = "Henry"
            }
        ]);

        await _patientOrderService.CreateNewOrder(order);

        await _patientOrderRepo.Received(1).InsertOrder(order);
    }
}