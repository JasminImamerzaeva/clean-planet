using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CleanPlanet.WinForms.Data;

[Table("partner_service_history", Schema = "cp")]
[Index("partner_id", "performed_at", Name = "IX_psh_partner_date")]
[Index("service_id", Name = "IX_psh_service_id")]
public partial class partner_service_history
{
    [Key]
    public long id { get; set; }

    public int partner_id { get; set; }

    public int service_id { get; set; }

    public int qty { get; set; }

    public DateOnly performed_at { get; set; }

    public DateTime created_at { get; set; }

    [ForeignKey("partner_id")]
    [InverseProperty("partner_service_history")]
    public virtual partners partner { get; set; } = null!;

    [ForeignKey("service_id")]
    [InverseProperty("partner_service_history")]
    public virtual services service { get; set; } = null!;
}
