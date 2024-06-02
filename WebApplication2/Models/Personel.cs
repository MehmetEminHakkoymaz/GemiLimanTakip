using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models;

[Table("Personel")]
public partial class Personel
{
    [Key]
    public int PersonelId { get; set; }

    [StringLength(50)]
    [Display(Name = "Adı")]
    public string? Adi { get; set; }

    [StringLength(50)]
    [Display(Name = "Soyadı")]
    public string? Soyadi { get; set; }

    [StringLength(50)]
    [Display(Name = "Görevi")]
    public string? Gorev { get; set; }

    public int? GemiId { get; set; }

    // GemiId'ye karşılık gelen Gemi nesnesini tanımlayın
    [ForeignKey("GemiId")]
    public Gemiler? Gemi { get; set; }
}

