using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    [Table("Yukler")]
    public class Yukler
    {
        [Key]
        public int YukId { get; set; }
        [StringLength(50)]
        [Display(Name = "Yük Türü")]
        public string YukTuru { get; set; } = null!;

        [Display(Name = "Yük Miktarı")]
        public int Agirlik { get; set; }

        [NotMapped]
        [DisplayName("Yük Resmi")]
        public IFormFile? ImageFile { get; set; }

        [DisplayName("Yük Resmi")]
        public string? UrunPhoto { get; set; }
    }
}
