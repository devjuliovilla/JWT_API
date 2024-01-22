using JWT_API.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JWT_API.EntityMaps
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnName("NAME")
                .HasColumnType("varchar(10)")
                .IsRequired();
        }
    }
}