using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cinemas.Models
{
    public partial class CinemasContext : IdentityDbContext
    {
        public CinemasContext()
        {
        }

        public CinemasContext(DbContextOptions<CinemasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pelicula> Peliculas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-BKNU1NK\\SQLEXPRESS;Initial Catalog=Cinemas;integrated security=True; TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pelicula>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Calificacion).HasColumnName("calificacion");

                entity.Property(e => e.Director)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("director");

                entity.Property(e => e.Genero)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("genero");

                entity.Property(e => e.Poster)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("poster");

                entity.Property(e => e.Sipnopsis)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("sipnopsis");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("titulo");

                base.OnModelCreating(modelBuilder);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
