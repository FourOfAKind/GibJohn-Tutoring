using GibJohn.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GibJohn.Models;

namespace GibJohn.Data;

// Inherits all the features from IdentityDbContext
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
    // Adds the course, registration, and notes DB so they can be accessed through context and be pulled through pages
    public DbSet<GibJohn.Models.Course>? Course { get; set; }
    public DbSet<GibJohn.Models.Registration>? Registration { get; set; }
    public DbSet<GibJohn.Models.Note>? Note { get; set; }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // Add constraints to the custom First and Last name properties we've added
        builder.Property(u => u.FirstName).HasMaxLength(20);
        builder.Property(u => u.LastName).HasMaxLength(20);
    }
}