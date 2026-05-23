using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyApi.Models;

public class Position
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(255)]
    public string? Name { get; set; }

    public virtual ICollection<Employee>? Employees { get; set; }
}
