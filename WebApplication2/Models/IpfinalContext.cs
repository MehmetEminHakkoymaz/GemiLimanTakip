using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using Microsoft.AspNetCore.Identity;

namespace WebApplication2.Models;

public partial class IpfinalContext : IdentityDbContext<IdentityUser>
{
    public IpfinalContext()
    {
    }

    public IpfinalContext(DbContextOptions<IpfinalContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Varislar> Varislars { get; set; }
    public virtual DbSet<Yukler> Yuklers { get; set; }
    public virtual DbSet<Limanlar> Limanlars { get; set; }
    public virtual DbSet<Gemiler> Gemilers { get; set; }
    public virtual DbSet<Kalkislar> Kalkislars { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public DbSet<Kullanıcılar> Kullanıcılar { get; set; }
    public DbSet<Kalkislar> Kalkis { get; set; } 



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Add this line

        modelBuilder.Entity<Varislar>(entity =>
        {
            entity.HasKey(e => e.VarisId).HasName("PK_Varislar_4A8D300558D4B744");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<WebApplication2.Models.Personel> Personel { get; set; } = default!;

}