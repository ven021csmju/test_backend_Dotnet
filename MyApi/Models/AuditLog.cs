using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Models;

public class AuditLog
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid? UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual User? User { get; set; }

    [MaxLength(255)]
    public string? Action { get; set; }

    [MaxLength(255)]
    public string? TableName { get; set; }

    public Guid? RecordId { get; set; }

    [Column(TypeName = "jsonb")]
    public string? OldData { get; set; }

    [Column(TypeName = "jsonb")]
    public string? NewData { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
