namespace PatientOrders.Api.Entities;

public class PatientEntity
{
    public required int Id { get; set; }
    public required string Name { get; set; } = default!;
}
