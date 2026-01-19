using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CleanPlanet.WinForms.Data;

[Table("material_types", Schema = "cp")]
[Index("name", Name = "UQ_material_types_name", IsUnique = true)]
public partial class material_types
{
    [Key]
    public int id { get; set; }

    [StringLength(100)]
    public string name { get; set; } = null!;

    [Column(TypeName = "decimal(5, 2)")]
    public decimal overuse_percent { get; set; }

    public DateTime created_at { get; set; }

    [InverseProperty("material_type")]
    public virtual material_prices? material_prices { get; set; }

    [InverseProperty("material_type")]
    public virtual ICollection<service_material_norms> service_material_norms { get; set; } = new List<service_material_norms>();
}
