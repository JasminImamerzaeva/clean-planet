using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CleanPlanet.WinForms.Data;

[Table("labor_rates", Schema = "cp")]
[Index("labor_role_id", Name = "IX_labor_rates_role")]
[Index("labor_role_id", Name = "UQ_labor_rates_role", IsUnique = true)]
public partial class labor_rates
{
    [Key]
    public int id { get; set; }

    public int labor_role_id { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal hourly_rate { get; set; }

    [StringLength(3)]
    public string currency { get; set; } = null!;

    public DateTime updated_at { get; set; }

    [ForeignKey("labor_role_id")]
    [InverseProperty("labor_rates")]
    public virtual labor_roles labor_role { get; set; } = null!;
}
