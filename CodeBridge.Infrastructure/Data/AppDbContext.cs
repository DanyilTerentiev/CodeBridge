using System.Reflection;
using CodeBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeBridge.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Dog> Dogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppDbContext))!);
        base.OnModelCreating(modelBuilder);
    }
}