﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestoManager_AyadiMed.Models.RestosModel;

#nullable disable

namespace RestoManager_AyadiMed.Migrations
{
    [DbContext(typeof(RestoDbContext))]
    partial class RestoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RestoManager_AyadiMed.Models.RestosModel.Avis", b =>
                {
                    b.Property<int>("CodeAvis")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodeAvis"));

                    b.Property<string>("Commentaire")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Commentaire");

                    b.Property<int>("Note")
                        .HasColumnType("int")
                        .HasColumnName("Note");

                    b.Property<int>("NumResto")
                        .HasColumnType("int");

                    b.Property<string>("nomPersonne")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("nomPersonne");

                    b.HasKey("CodeAvis");

                    b.HasIndex("NumResto");

                    b.ToTable("TAvis", "admin");
                });

            modelBuilder.Entity("RestoManager_AyadiMed.Models.RestosModel.Proprietaires", b =>
                {
                    b.Property<int>("Numero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Numero"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Email");

                    b.Property<string>("Gsm")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("GsmProp");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("NomProp");

                    b.HasKey("Numero");

                    b.ToTable("TProprietair", "resto");
                });

            modelBuilder.Entity("RestoManager_AyadiMed.Models.RestosModel.Restaurant", b =>
                {
                    b.Property<int>("CodeResto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodeResto"));

                    b.Property<string>("NomResto")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("SpecResto");

                    b.Property<int>("NumProp")
                        .HasColumnType("int");

                    b.Property<string>("Specialite")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasDefaultValue("Tunisienne")
                        .HasColumnName("Email");

                    b.Property<string>("Tel")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("TelResto");

                    b.Property<string>("Ville")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("VilleResto");

                    b.HasKey("CodeResto");

                    b.HasIndex("NumProp");

                    b.ToTable("TRestaurant", "resto");
                });

            modelBuilder.Entity("RestoManager_AyadiMed.Models.RestosModel.Avis", b =>
                {
                    b.HasOne("RestoManager_AyadiMed.Models.RestosModel.Restaurant", "leResto")
                        .WithMany("LesAvis")
                        .HasForeignKey("NumResto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Relation_Resto_Avis");

                    b.Navigation("leResto");
                });

            modelBuilder.Entity("RestoManager_AyadiMed.Models.RestosModel.Restaurant", b =>
                {
                    b.HasOne("RestoManager_AyadiMed.Models.RestosModel.Proprietaires", "LePropio")
                        .WithMany("LesRestos")
                        .HasForeignKey("NumProp")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Relation_Propio_Resto");

                    b.Navigation("LePropio");
                });

            modelBuilder.Entity("RestoManager_AyadiMed.Models.RestosModel.Proprietaires", b =>
                {
                    b.Navigation("LesRestos");
                });

            modelBuilder.Entity("RestoManager_AyadiMed.Models.RestosModel.Restaurant", b =>
                {
                    b.Navigation("LesAvis");
                });
#pragma warning restore 612, 618
        }
    }
}
