using Microsoft.AspNetCore.Mvc;
using PatientOrders.Api.Models;
using PatientOrders.Api.Repos;
using PatientOrders.Api.Services;

namespace PatientOrders.Api.Controllers;

[ApiController]
[Route("PatientOrders")]
public class PatientOrdersController : ControllerBase
{
    private readonly IPatientOrderService _patientOrderService;

    public PatientOrdersController(IPatientOrderService patientOrderService)
    {
        _patientOrderService = patientOrderService;
    }

    [HttpGet("GetPatientOrders")]
    [ProducesResponseType(typeof(List<PatientOrder>), 200)]
    public async Task<List<PatientOrder>> GetPatientOrders()
    {
        var patientOrders = await _patientOrderService.GetPatientOrders();
        return patientOrders;
    }

    [HttpPost("CreateOrder")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderReqDto createOrderReqDto)
    {
        await _patientOrderService.CreateNewOrder(new Order
        {
            OrderMessage = createOrderReqDto.OrderMessage,
            PatientId = createOrderReqDto.PatientId
        });
        return Ok();
    }
    
    [HttpPost("CreatePatient")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> CreatePatient([FromBody] CreatePatientReqDto createPatientReqDto)
    {
        await _patientOrderService.CreateNewPatient(new Patient
        {
            PatientName = createPatientReqDto.PatientName
        });
        
        return Ok();
    }
}

public class CreateOrderReqDto
{
    public required int PatientId { get; set; }
    public required string OrderMessage { get; set; } = default!;
}

public class CreatePatientReqDto
{
    public required string PatientName { get; set; } = default!;
}