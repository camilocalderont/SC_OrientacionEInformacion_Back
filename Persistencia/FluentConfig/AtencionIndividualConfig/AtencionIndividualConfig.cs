using Dominio.Models.AtencionesIndividuales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.FluentConfig.AtencionIndividualConfig
{
    public class AtencionIndividualConfig
    {
        public AtencionIndividualConfig(EntityTypeBuilder<AtencionIndividual> entity)
        {
           entity.ToTable("AtencionIndividual");
           entity.HasKey(x => x.Id);

           
            entity.HasMany(p => p.AtencionReasignaciones) // tiene muchos
          .WithOne(p => p.AtencionIndividual)           
          .HasForeignKey(p => p.AtencionIndividualId)   // Código del caso asociado
          .HasConstraintName("FK_CasoI_A_CasoIR")
          .OnDelete(DeleteBehavior.Restrict);


           entity.HasMany(p => p.AtencionSeguimientos)
          .WithOne(p => p.AtencionIndividual)
          .HasForeignKey(p => p.AtencionIndividualId)   // Código del caso asociado
          .HasConstraintName("FK_CasoI_A_CasoIS")
          .OnDelete(DeleteBehavior.Restrict);

          entity.HasMany(p => p.AtencionAnexos)
          .WithOne(p => p.AtencionIndividual)
          .HasForeignKey(p => p.AtencionIndividualId)   // Código del caso asociado
          .HasConstraintName("FK_CasoI_A_CasoIAn")
          .OnDelete(DeleteBehavior.Restrict);


          entity.HasMany(p => p.AtencionActores)
          .WithOne(p => p.AtencionIndividual)
          .HasForeignKey(p => p.AtencionIndividualId)   // Código del caso asociado
          .HasConstraintName("FK_CasoI_A_CasoIAc")
          .OnDelete(DeleteBehavior.Restrict);

          // entity.HasOne(p => p.Personas)                 // tiene uno
          //.WithMany(p => p.AtencionIndividual);          // con muchos


           entity.Property(p => p.PersonaId).IsRequired();
           entity.Property(p => p.DtFechaRegistro).IsRequired();
           entity.Property(p => p.UsuarioId).IsRequired();
           entity.Property(p => p.CanalAtencionId).IsRequired();
           entity.Property(p => p.VcTurnoSat).IsRequired(false).HasMaxLength(50);
           entity.Property(p => p.TipoSolicitudId).IsRequired();
           entity.Property(p => p.MotivoId).IsRequired();
           entity.Property(p => p.SubMotivoId).IsRequired();
           entity.Property(p => p.TxAclaracionMotivo).IsRequired();
           entity.Property(p => p.TxGestionRealizada).IsRequired(false);
           entity.Property(p => p.VcRadicadoBte).IsRequired(false).HasMaxLength(50);
           entity.Property(p => p.EstadoId).IsRequired();
        }
    }
}
