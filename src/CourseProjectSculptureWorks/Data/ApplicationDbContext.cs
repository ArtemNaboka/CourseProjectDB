using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CourseProjectSculptureWorks.Models;
using CourseProjectSculptureWorks.Models.Entities;

namespace CourseProjectSculptureWorks.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Style> Styles { get; set; }
        public DbSet<Sculpture> Sculptures { get; set; }
        public DbSet<Sculptor> Sculptors { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ExcursionType> ExcursionTypes { get; set; }
        public DbSet<Excursion> Excursion { get; set; }
        public DbSet<Composition> Compositions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Composition>()
                .HasKey(c => new { c.LocationId, c.ExcursionId });

            builder.Entity<Composition>()
                .HasOne(c => c.Location)
                .WithMany(l => l.Compositions)
                .HasForeignKey(c => c.LocationId);

            builder.Entity<Composition>()
                .HasOne(c => c.Excursion)
                .WithMany(e => e.Compositions)
                .HasForeignKey(c => c.ExcursionId);

            base.OnModelCreating(builder);
        }
    }
}
