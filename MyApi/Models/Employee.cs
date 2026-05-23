using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Models;

public class Employee
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(100)]
    public string? EmployeeCode { get; set; }

    [MaxLength(255)]
    public string? FirstName { get; set; }

    [MaxLength(255)]
    public string? LastName { get; set; }

    [MaxLength(255)]
    public string? Email { get; set; }

    public Guid? OrganizationId { get; set; }
    [ForeignKey("OrganizationId")]
    public virtual Organization? Organization { get; set; }

    public Guid? DepartmentId { get; set; }
    [ForeignKey("DepartmentId")]
    public virtual Department? Department { get; set; }

    public Guid? PositionId { get; set; }
    [ForeignKey("PositionId")]
    public virtual Position? Position { get; set; }

    [MaxLength(50)]
    public string? EmployeeType { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public virtual ICollection<EventRegistration>? EventRegistrations { get; set; }
    public virtual ICollection<Checkin>? Checkins { get; set; }
}
