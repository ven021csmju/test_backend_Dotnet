using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Models;

public class Checkin
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid EventId { get; set; }
    [ForeignKey("EventId")]
    public virtual SafetyEvent? Event { get; set; }

    public Guid EmployeeId { get; set; }
    [ForeignKey("EmployeeId")]
    public virtual Employee? Employee { get; set; }

    [Column(TypeName = "decimal(10,7)")]
    public decimal? Latitude { get; set; }

    [Column(TypeName = "decimal(10,7)")]
    public decimal? Longitude { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? DistanceMeters { get; set; }

    public string? SelfiePhotoUrl { get; set; }

    public DateTime CheckedInAt { get; set; } = DateTime.UtcNow;
}
