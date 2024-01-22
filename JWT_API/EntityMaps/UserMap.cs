using JWT_API.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JWT_API.EntityMaps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Username)
                .HasColumnName("USNAME")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.Password)
                .HasColumnName("PASS")
                .HasColumnType("varchar(MAX)")
                .IsRequired();

            builder.Property(x => x.RoleId)
                   .HasColumnName("ROLEID")
                   .HasColumnType("int")
                   .IsRequired();

            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(fk => fk.RoleId);
        }
    }
}