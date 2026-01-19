using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CleanPlanet.WinForms.Data;

[Table("service_material_norms", Schema = "cp")]
[Index("material_type_id", Name = "IX_smn_material_type_id")]
[Index("service_id", Name = "IX_smn_service_id")]
[Index("service_id", "material_type_id", Name = "UQ_service_material_norms", IsUnique = true)]
public partial class service_material_norms
{
    [Key]
    public int id { get; set; }

    public int service_id { get; set; }

    public int material_type_id { get; set; }

    [Column(TypeName = "decimal(18, 3)")]
    public decimal qty_per_unit { get; set; }

    public DateTime created_at { get; set; }

    [ForeignKey("material_type_id")]
    [InverseProperty("service_material_norms")]
    public virtual material_types material_type { get; set; } = null!;

    [ForeignKey("service_id")]
    [InverseProperty("service_material_norms")]
    public virtual services service { get; set; } = null!;
}
