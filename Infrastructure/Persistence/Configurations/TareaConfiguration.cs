using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class TareaConfiguration : IEntityTypeConfiguration<Tarea>
    {
        public void Configure(EntityTypeBuilder<Tarea> builder)
        {
            builder.ToTable("tareas");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).IsRequired(true).ValueGeneratedOnAdd();
            builder.Property(s => s.Titulo).IsRequired(true).HasMaxLength(50);
            builder.Property(s => s.Descripcion).IsRequired(false).HasMaxLength(150);
            builder.Property(s => s.Completada).IsRequired(true);

            builder.HasOne(s => s.Usuario)
                .WithMany(s => s.Tareas)
                .HasForeignKey(s => s.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}