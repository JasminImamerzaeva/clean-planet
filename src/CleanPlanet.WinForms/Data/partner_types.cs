using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CleanPlanet.WinForms.Data;

[Table("partner_types", Schema = "cp")]
[Index("name", Name = "UQ_partner_types_name", IsUnique = true)]
public partial class partner_types
{
    [Key]
    public int id { get; set; }

    [StringLength(100)]
    public string name { get; set; } = null!;

    public DateTime created_at { get; set; }

    [InverseProperty("partner_type")]
    public virtual ICollection<partners> partners { get; set; } = new List<partners>();
}
