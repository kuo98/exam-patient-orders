using System.Collections.Generic;

namespace PatientOrders.Api.Models;

public class Patient
{
    public int PatientId { get; set; }
    public required string PatientName { get; set; } = default!;
}
