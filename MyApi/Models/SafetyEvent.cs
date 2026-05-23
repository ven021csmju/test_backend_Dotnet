using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Models;

public class SafetyEvent
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(255)]
    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public Guid? CreatedBy { get; set; }
    [ForeignKey("CreatedBy")]
    public virtual User? Creator { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public virtual ICollection<EventRegistration>? Registrations { get; set; }
    public virtual ICollection<GpsConfig>? GpsConfigs { get; set; }
    public virtual ICollection<Checkin>? Checkins { get; set; }
}
