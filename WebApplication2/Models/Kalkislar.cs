using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models;

[Table("Kalkislar")]
public partial class Kalkislar
{
    [Key]
    public int KalkisId { get; set; }

    public int? GemiId { get; set; }
    [ForeignKey("GemiId")]
    public virtual Gemiler? Gemi { get; set; }

    public int? LimanId { get; set; }
    [ForeignKey("LimanId")]
    public virtual Limanlar? Liman { get; set; }

    [Column(TypeName = "datetime")]
    [Display(Name = "Kalkış Zamanı")]
    public DateTime KalkisZamani { get; set; }
}

