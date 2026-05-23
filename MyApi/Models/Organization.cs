using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Models;

public class Organization
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(50)]
    public string? Code { get; set; }

    [MaxLength(255)]
    public string? Name { get; set; }

    [MaxLength(50)]
    public string? Type { get; set; }

    public Guid? ParentId { get; set; }
    [ForeignKey("ParentId")]
    public virtual Organization? Parent { get; set; }

    public virtual ICollection<Organization>? Children { get; set; }
    public virtual ICollection<Department>? Departments { get; set; }
    public virtual ICollection<Employee>? Employees { get; set; }
}
