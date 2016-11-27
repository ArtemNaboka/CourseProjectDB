using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CourseProjectSculptureWorks.Data;

namespace CourseProjectSculptureWorks.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CourseProjectSculptureWorks.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CourseProjectSculptureWorks.Models.Entities.Composition", b =>
                {
                    b.Property<int>("LocationId");

                    b.Property<int>("ExcursionId");

                    b.Property<int>("SerialNumber");

                    b.HasKey("LocationId", "ExcursionId");

                    b.HasIndex("ExcursionId");

                    b.HasIndex("LocationId");

                    b.ToTable("Compositions");
                });

            modelBuilder.Entity("CourseProjectSculptureWorks.Models.Entities.Excursion", b =>
                {
                    b.Property<int>("ExcursionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOfExcursion");

                    b.Property<int?>("ExcursionTypeId");

                    b.Property<int>("NumberOfPeople");

                    b.Property<decimal>("PriceOfExcursion");

                    b.Property<string>("Subjects");

                    b.HasKey("ExcursionId");

                    b.HasIndex("ExcursionTypeId");

                    b.ToTable("Excursions");
                });

            modelBuilder.Entity("CourseProjectSculptureWorks.Models.Entities.ExcursionType", b =>
                {
                    b.Property<int>("ExcursionTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Discount");

                    b.Property<int>("MaxNumberOfPeople");

                    b.Property<int>("MinNumberOfPeople");

                    b.Property<string>("NameOfType")
                        .IsRequired();

                    b.HasKey("ExcursionTypeId");

                    b.ToTable("ExcursionTypes");
                });

            modelBuilder.Entity("CourseProjectSculptureWorks.Models.Entities.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 30);

                    b.Property<int>("DurationOfExcursion");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("LocationType")
                        .IsRequired();

                    b.Property<decimal>("PriceForPerson");

                    b.HasKey("LocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("CourseProjectSculptureWorks.Models.Entities.Sculptor", b =>
                {
                    b.Property<int>("SculptorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 25);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 25);

                    b.Property<int>("YearOfBirth");

                    b.Property<int?>("YearOfDeath");

                    b.HasKey("SculptorId");

                    b.ToTable("Sculptors");
                });

            modelBuilder.Entity("CourseProjectSculptureWorks.Models.Entities.Sculpture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Height");

                    b.Property<int?>("LocationId");

                    b.Property<string>("Material")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 25);

                    b.Property<int?>("SculptorId");

                    b.Property<double>("Square");

                    b.Property<int?>("StyleId");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("SculptorId");

                    b.HasIndex("StyleId");

                    b.ToTable("Sculptures");
                });

            modelBuilder.Entity("CourseProjectSculptureWorks.Models.Entities.Style", b =>
                {
                    b.Property<int>("StyleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("Era")
                        .IsRequired();

                    b.Property<string>("StyleName");

                    b.HasKey("StyleId");

                    b.ToTable("Styles");
                });

            modelBuilder.Entity("CourseProjectSculptureWorks.Models.Entities.Transfer", b =>
                {
                    b.Property<int>("StartLocationId");

                    b.Property<int>("FinishLocationId");

                    b.Property<int>("Duration");

                    b.HasKey("StartLocationId", "FinishLocationId");

                    b.HasIndex("FinishLocationId");

                    b.HasIndex("StartLocationId");

                    b.ToTable("Transfers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CourseProjectSculptureWorks.Models.Entities.Composition", b =>
                {
                    b.HasOne("CourseProjectSculptureWorks.Models.Entities.Excursion", "Excursion")
                        .WithMany("Compositions")
                        .HasForeignKey("ExcursionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CourseProjectSculptureWorks.Models.Entities.Location", "Location")
                        .WithMany("Compositions")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CourseProjectSculptureWorks.Models.Entities.Excursion", b =>
                {
                    b.HasOne("CourseProjectSculptureWorks.Models.Entities.ExcursionType", "ExcursionType")
                        .WithMany("Excursions")
                        .HasForeignKey("ExcursionTypeId");
                });

            modelBuilder.Entity("CourseProjectSculptureWorks.Models.Entities.Sculpture", b =>
                {
                    b.HasOne("CourseProjectSculptureWorks.Models.Entities.Location", "Location")
                        .WithMany("Sculptures")
                        .HasForeignKey("LocationId");

                    b.HasOne("CourseProjectSculptureWorks.Models.Entities.Sculptor", "Sculptor")
                        .WithMany("Sculptures")
                        .HasForeignKey("SculptorId");

                    b.HasOne("CourseProjectSculptureWorks.Models.Entities.Style", "Style")
                        .WithMany("Sculptures")
                        .HasForeignKey("StyleId");
                });

            modelBuilder.Entity("CourseProjectSculptureWorks.Models.Entities.Transfer", b =>
                {
                    b.HasOne("CourseProjectSculptureWorks.Models.Entities.Location", "FinishLocation")
                        .WithMany("FinishTransfers")
                        .HasForeignKey("FinishLocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CourseProjectSculptureWorks.Models.Entities.Location", "StartLocation")
                        .WithMany("StartTransfers")
                        .HasForeignKey("StartLocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CourseProjectSculptureWorks.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CourseProjectSculptureWorks.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CourseProjectSculptureWorks.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
