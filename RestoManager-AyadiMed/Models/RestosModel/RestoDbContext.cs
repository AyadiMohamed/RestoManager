using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RestoManager_AyadiMed.Models.RestosModel
{
    public class RestoDbContext : DbContext
    {
        public RestoDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Proprietaires> proprietaires { get; set; } = null!;
        public DbSet<Restaurant> restaurants { get; set; } = null!;

        public DbSet<Avis> avis { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<Proprietaires> PropBuilder = modelBuilder.Entity<Proprietaires>();
            PropBuilder.ToTable("TProprietair", "resto");
            PropBuilder.HasKey(p => p.Numero);
            PropBuilder.Property(p => p.Nom).HasColumnName("NomProp").HasMaxLength(20).IsRequired();
            PropBuilder.Property(p => p.Email).HasColumnName("Email").HasMaxLength(50).IsRequired();
            PropBuilder.Property(p => p.Gsm).HasColumnName("GsmProp").HasMaxLength(8).IsRequired();

            EntityTypeBuilder<Restaurant> RestoBuilder= modelBuilder.Entity<Restaurant>();
            RestoBuilder.ToTable("TRestaurant", "resto");
            RestoBuilder.HasKey(r => r.CodeResto);
            RestoBuilder.Property(r => r.NomResto).HasColumnName("SpecResto").HasMaxLength(20).IsRequired();
            RestoBuilder.Property(r => r.Specialite).HasColumnName("Email").HasMaxLength(20).IsRequired().HasDefaultValue("Tunisienne");
            RestoBuilder.Property(r => r.Ville).HasColumnName("VilleResto").HasMaxLength(20).IsRequired();
            RestoBuilder.Property(r => r.Tel).HasColumnName("TelResto").HasMaxLength(8).IsRequired();

            PropBuilder.HasMany(p => p.LesRestos).WithOne(r => r.LePropio).HasForeignKey(r => r.NumProp).HasConstraintName("Relation_Propio_Resto").IsRequired();

            EntityTypeBuilder<Avis> AvisBuilder = modelBuilder.Entity<Avis>();
            AvisBuilder.ToTable("TAvis", "admin");
            AvisBuilder.HasKey(a => a.CodeAvis);
            AvisBuilder.Property(a => a.nomPersonne).HasColumnName("nomPersonne").HasMaxLength(30).IsRequired();
            AvisBuilder.Property(a => a.Note).HasColumnName("Note").IsRequired();
            AvisBuilder.Property(a => a.Commentaire).HasColumnName("Commentaire").HasMaxLength(255);

            RestoBuilder.HasMany(r => r.LesAvis).WithOne(a => a.leResto).HasForeignKey(a => a.NumResto).HasConstraintName("Relation_Resto_Avis").IsRequired();

        }
    }

}
