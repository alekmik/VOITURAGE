using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Voiturage.Models;

namespace Voiturage.Data
{
    public partial class voiturageContext : DbContext
    {
        public voiturageContext()
        {
        }

        public voiturageContext(DbContextOptions<voiturageContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Avis> Avis { get; set; } = null!;
        public virtual DbSet<Trajet> Trajets { get; set; } = null!;
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; } = null!;
        public virtual DbSet<Ville> Villes { get; set; } = null!;
        public virtual DbSet<Voiture> Voitures { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=voiturage;User Id=sa;Password=reallyStrongPwd123;Trusted_Connection=false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Avis>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Commentaire)
                    .HasColumnType("text")
                    .HasColumnName("commentaire");

                entity.Property(e => e.IdNote).HasColumnName("id_2_Utilisateur");

                entity.Property(e => e.IdTrajet).HasColumnName("id_Trajet");

                entity.Property(e => e.IdNotant).HasColumnName("id_Utilisateur");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.HasOne(d => d.UtilisateurNote)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.IdNote)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Avis_Utilisateur2_FK");

                entity.HasOne(d => d.Trajet)
                    .WithMany(p => p.Avis)
                    .HasForeignKey(d => d.IdTrajet)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Avis_Trajet0_FK");

                entity.HasOne(d => d.UtilisateurNotant)
                    .WithMany(p => p.NotesDonnees)
                    .HasForeignKey(d => d.IdNotant)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Avis_Utilisateur_FK");
            });

            modelBuilder.Entity<Trajet>(entity =>
            {
                entity.ToTable("Trajet");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HeureArrivee)
                    .HasColumnType("datetime")
                    .HasColumnName("heureArrivee");

                entity.Property(e => e.HeureDepart)
                    .HasColumnType("datetime")
                    .HasColumnName("heureDepart");

                entity.Property(e => e.IdVilleArrivee).HasColumnName("id_2_Ville");

                entity.Property(e => e.IdChauffeur).HasColumnName("id_Utilisateur");

                entity.Property(e => e.IdVilleDepart).HasColumnName("id_Ville");

                entity.Property(e => e.Prix).HasColumnName("prix");

                entity.Property(e => e.PlaceMax).HasColumnName("PlaceMax");

                entity.HasOne(d => d.VilleArrivee)
                    .WithMany(p => p.TrajetALarrivee)
                    .HasForeignKey(d => d.IdVilleArrivee)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Trajet_Ville2_FK");

                entity.HasOne(d => d.Chauffeur)
                    .WithMany(p => p.TrajetsChauffeur)
                    .HasForeignKey(d => d.IdChauffeur)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Trajet_Utilisateur0_FK");

                entity.HasOne(d => d.VilleDepart)
                    .WithMany(p => p.TrajetAuDepart)
                    .HasForeignKey(d => d.IdVilleDepart)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Trajet_Ville_FK");
            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.ToTable("Utilisateur");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Admin).HasColumnName("admin");

                entity.Property(e => e.IdVoiture).HasColumnName("id_Voiture");

                entity.Property(e => e.Mail)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("mail");

                entity.Property(e => e.Nom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nom");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Photo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("photo");

                entity.Property(e => e.Prenom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("prenom");

                entity.Property(e => e.Salt)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("salt");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.Voiture)
                    .WithMany(p => p.Utilisateurs)
                    .HasForeignKey(d => d.IdVoiture)
                    .HasConstraintName("Utilisateur_Voiture_FK");

                entity.HasMany(d => d.TrajetsPassager)
                    .WithMany(p => p.Passagers)
                    .UsingEntity<Dictionary<string, object>>(
                        "Passager",
                        l => l.HasOne<Trajet>().WithMany().HasForeignKey("IdTrajet").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("passagers_Trajet0_FK"),
                        r => r.HasOne<Utilisateur>().WithMany().HasForeignKey("Id").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("passagers_Utilisateur_FK"),
                        j =>
                        {
                            j.HasKey("Id", "IdTrajet").HasName("passagers_PK");

                            j.ToTable("passagers");

                            j.IndexerProperty<int>("Id").HasColumnName("id");

                            j.IndexerProperty<int>("IdTrajet").HasColumnName("id_Trajet");
                        });
            });

            modelBuilder.Entity<Ville>(entity =>
            {
                entity.ToTable("Ville");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Voiture>(entity =>
            {
                entity.ToTable("Voiture");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Marque)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("marque");

                entity.Property(e => e.Modele)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("modele");
            });

            OnModelCreatingPartial(modelBuilder);
            
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
