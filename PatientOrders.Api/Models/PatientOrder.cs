using System.Text.Json.Serialization;

namespace PatientOrders.Api.Models;

public class PatientOrder
{
    public required int PatientId { get; set; }
    public required string PatientName { get; set; } = default!;
    
    public int? OrderId { get; set; }
    
    public string? OrderMessage { get; set; }
}
