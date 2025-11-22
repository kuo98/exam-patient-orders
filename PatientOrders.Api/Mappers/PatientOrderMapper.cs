using PatientOrders.Api.Entities;
using PatientOrders.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace PatientOrders.Api.Mappers;

public static class PatientOrderMapper
{
    public static PatientEntity ToEntity(this Patient patient)
    {
        return new PatientEntity
        {
            Name = patient.PatientName,
            Id = patient.PatientId
        };
    }

    public static Patient ToDomain(this PatientEntity patientEntity)
    {
        return new Patient
        {
            PatientId = patientEntity.Id,
            PatientName = patientEntity.Name
        };
    }
    
    public static OrderEntity ToEntity(this Order order)
    {
        return new OrderEntity
        {
            Message = order.OrderMessage,
            PatientId = order.PatientId,
            Id = order.OrderId
        };
    }

    public static Order ToDomain(this OrderEntity orderEntity)
    {
        return new Order
        {
            OrderId = orderEntity.Id,
            OrderMessage = orderEntity.Message,
            PatientId = orderEntity.PatientId
        };
    }
}
