using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CleanPlanet.WinForms.Data;

[Table("services", Schema = "cp")]
[Index("name", Name = "IX_services_name")]
[Index("code", Name = "UQ_services_code", IsUnique = true)]
public partial class services
{
    [Key]
    public int id { get; set; }

    public int service_type_id { get; set; }

    [StringLength(200)]
    public string name { get; set; } = null!;

    [StringLength(50)]
    public string code { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal min_partner_price { get; set; }

    public DateTime created_at { get; set; }

    [InverseProperty("service")]
    public virtual ICollection<partner_service_history> partner_service_history { get; set; } = new List<partner_service_history>();

    [InverseProperty("service")]
    public virtual ICollection<service_labor_norms> service_labor_norms { get; set; } = new List<service_labor_norms>();

    [InverseProperty("service")]
    public virtual ICollection<service_material_norms> service_material_norms { get; set; } = new List<service_material_norms>();

    [ForeignKey("service_type_id")]
    [InverseProperty("services")]
    public virtual service_types service_type { get; set; } = null!;
}
