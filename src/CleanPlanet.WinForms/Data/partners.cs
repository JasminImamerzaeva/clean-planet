using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CleanPlanet.WinForms.Data;

[Table("partners", Schema = "cp")]
[Index("name", Name = "IX_partners_name")]
[Index("name", Name = "UQ_partners_name", IsUnique = true)]
public partial class partners
{
    [Key]
    public int id { get; set; }

    public int partner_type_id { get; set; }

    [StringLength(200)]
    public string name { get; set; } = null!;

    [StringLength(150)]
    public string? head { get; set; }

    [StringLength(255)]
    public string? email { get; set; }

    [StringLength(50)]
    public string? phone { get; set; }

    [StringLength(400)]
    public string? legal_address { get; set; }

    [StringLength(12)]
    public string? inn { get; set; }

    public byte? rating { get; set; }

    public DateTime created_at { get; set; }

    [InverseProperty("partner")]
    public virtual ICollection<partner_service_history> partner_service_history { get; set; } = new List<partner_service_history>();

    [ForeignKey("partner_type_id")]
    [InverseProperty("partners")]
    public virtual partner_types partner_type { get; set; } = null!;
}
