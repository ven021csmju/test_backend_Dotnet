using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Models;

public class GpsConfig
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid EventId { get; set; }
    [ForeignKey("EventId")]
    public virtual SafetyEvent? Event { get; set; }

    [Column(TypeName = "decimal(10,7)")]
    public decimal? Latitude { get; set; }

    [Column(TypeName = "decimal(10,7)")]
    public decimal? Longitude { get; set; }

    public int? RadiusMeters { get; set; }

    public DateTime? CheckinStart { get; set; }

    public DateTime? CheckinEnd { get; set; }
}
