namespace PatientOrders.Api.Entities;

public class OrderEntity
{
    public int Id { get; set; }
    public required string Message { get; set; } = default!;
    public required int PatientId { get; set; }
}
