namespace PatientOrders.Api.Models;

public class Order
{
    public int OrderId { get; set; }
    public required string OrderMessage { get; set; } = default!;
    public required int PatientId { get; set; }
}
