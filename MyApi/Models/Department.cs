using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Models;

public class Department
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid OrganizationId { get; set; }
    [ForeignKey("OrganizationId")]
    public virtual Organization? Organization { get; set; }

    [MaxLength(255)]
    public string? Name { get; set; }

    public virtual ICollection<Employee>? Employees { get; set; }
}
