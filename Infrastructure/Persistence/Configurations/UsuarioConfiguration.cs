using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuarios");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).IsRequired(true).ValueGeneratedOnAdd();
            builder.Property(s => s.Nombre).IsRequired(true).HasMaxLength(50);
            builder.Property(s => s.Email).IsRequired(true).HasMaxLength(150);
        }
    }
}
