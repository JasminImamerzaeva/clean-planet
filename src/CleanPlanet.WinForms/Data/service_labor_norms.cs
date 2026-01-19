using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CleanPlanet.WinForms.Data;

[Table("service_labor_norms", Schema = "cp")]
[Index("labor_role_id", Name = "IX_sln_labor_role_id")]
[Index("service_id", Name = "IX_sln_service_id")]
[Index("service_id", "labor_role_id", Name = "UQ_sln_service_role", IsUnique = true)]
public partial class service_labor_norms
{
    [Key]
    public int id { get; set; }

    public int service_id { get; set; }

    public int labor_role_id { get; set; }

    [Column(TypeName = "decimal(10, 3)")]
    public decimal hours_per_unit { get; set; }

    public DateTime created_at { get; set; }

    [ForeignKey("labor_role_id")]
    [InverseProperty("service_labor_norms")]
    public virtual labor_roles labor_role { get; set; } = null!;

    [ForeignKey("service_id")]
    [InverseProperty("service_labor_norms")]
    public virtual services service { get; set; } = null!;
}
