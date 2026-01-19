using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CleanPlanet.WinForms.Data;

[Table("material_prices", Schema = "cp")]
[Index("material_type_id", Name = "IX_material_prices_material_type")]
[Index("material_type_id", Name = "UQ_material_prices_material_type", IsUnique = true)]
public partial class material_prices
{
    [Key]
    public int id { get; set; }

    public int material_type_id { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal price_per_unit { get; set; }

    [StringLength(3)]
    public string currency { get; set; } = null!;

    public DateTime updated_at { get; set; }

    [ForeignKey("material_type_id")]
    [InverseProperty("material_prices")]
    public virtual material_types material_type { get; set; } = null!;
}
