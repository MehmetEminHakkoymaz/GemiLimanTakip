using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models;

[Table("Varislar")]
public partial class Varislar
{
    [Key]
    public int VarisId { get; set; }

    public int? GemiId { get; set; }
    [ForeignKey("GemiId")]
    public virtual Gemiler? Gemi { get; set; }

    public int? LimanId { get; set; }
    [ForeignKey("LimanId")]
    public virtual Limanlar? Liman { get; set; }

    [Column(TypeName = "datetime")]
    [Display(Name = "Varış Zamanı")]
    public DateTime VarisZamani { get; set; }
}
