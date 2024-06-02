using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models;

[Table("Gemiler")]
public partial class Gemiler
{
    [Key]
    public int GemiId { get; set; }

    [StringLength(100)]
    [Display(Name = "Gemi")]
    public string GemiAdi { get; set; } = null!;

    [StringLength(50)]
    [Display(Name = "Ülke")]
    public string Bayrak { get; set; } = null!;

    [Display(Name = "Yük Sınırı")]
    public int Tonaj { get; set; }

    public int? LimanId { get; set; }
}
