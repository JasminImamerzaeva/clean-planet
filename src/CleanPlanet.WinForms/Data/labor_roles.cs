using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CleanPlanet.WinForms.Data;

[Table("labor_roles", Schema = "cp")]
[Index("name", Name = "UQ_labor_roles_name", IsUnique = true)]
public partial class labor_roles
{
    [Key]
    public int id { get; set; }

    [StringLength(100)]
    public string name { get; set; } = null!;

    public DateTime created_at { get; set; }

    [InverseProperty("labor_role")]
    public virtual labor_rates? labor_rates { get; set; }

    [InverseProperty("labor_role")]
    public virtual ICollection<service_labor_norms> service_labor_norms { get; set; } = new List<service_labor_norms>();
}
