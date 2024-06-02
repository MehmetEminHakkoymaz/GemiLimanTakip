using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models;

[Table("Limanlar")]
public partial class Limanlar
{
    [Key]
    public int LimanId { get; set; }

    [StringLength(100)]
    [Display(Name = "Liman")]
    public string LimanAdi { get; set; } = null!;

    [StringLength(50)]
    [Display(Name = "Şehir")]
    public string Sehir { get; set; } = null!;

    [StringLength(50)]
    [Display(Name = "Ülke")]
    public string Ulke { get; set; } = null!;
}
