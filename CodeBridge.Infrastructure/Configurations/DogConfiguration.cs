using CodeBridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeBridge.Infrastructure.Configurations;

public class DogConfiguration : IEntityTypeConfiguration<Dog>
{
    public void Configure(EntityTypeBuilder<Dog> builder)
    {
        builder.HasKey(dog => dog.Name);
        builder.Property(dog => dog.Name).HasColumnName("name");
        builder.Property(dog => dog.Color).HasColumnName("color");
        builder.Property(dog => dog.TailLength).HasColumnName("tail_length");
        builder.Property(dog => dog.Weight).HasColumnName("weight");

        builder.HasData(new List<Dog>
        {
            new()
            {
                Name = "Neo",
                Color = "red & amber",
                TailLength = 22,
                Weight = 32
            },
            new()
            {
                Name = "Jessy",
                Color = "black & white",
                TailLength = 7,
                Weight = 14
            }
        });
    }
}