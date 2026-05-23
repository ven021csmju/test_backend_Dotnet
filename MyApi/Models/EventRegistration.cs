using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Models;

public class EventRegistration
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid EventId { get; set; }
    [ForeignKey("EventId")]
    public virtual SafetyEvent? Event { get; set; }

    public Guid EmployeeId { get; set; }
    [ForeignKey("EmployeeId")]
    public virtual Employee? Employee { get; set; }

    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
}
