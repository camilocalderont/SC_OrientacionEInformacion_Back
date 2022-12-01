using Dominio.Models.AtencionesGrupales;
using Dominio.Models.AtencionesIndividuales;
using Dominio.Models.AtencionesWeb;
using Microsoft.EntityFrameworkCore;
using Persistencia.FluentConfig.AtencionGrupalConfig;
using Persistencia.FluentConfig.AtencionIndividualConfig;
using Persistencia.FluentConfig.AtencionWebConfig;

namespace Persistencia.Context
{
    public class OrientacionDbContext : DbContext
    {
        public OrientacionDbContext(DbContextOptions options): base(options){}
        public OrientacionDbContext(){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region  FLUENTCONFIG ATENCION INDIVIDUAL CONFIG
            new AtencionActorConfig(modelBuilder.Entity<AtencionActor>());
            new AtencionAnexoConfig(modelBuilder.Entity<AtencionAnexo>());
            new AtencionIndividualConfig(modelBuilder.Entity<AtencionIndividual>());
            new AtencionReasignacionConfig(modelBuilder.Entity<AtencionReasignacion>());
            new AtencionSeguimientoConfig(modelBuilder.Entity<AtencionSeguimiento>());
            new PersonaConfig(modelBuilder.Entity<Persona>());
            new PersonaAfiliacionConfig(modelBuilder.Entity<PersonaAfiliacion>());
            new PersonaContactoConfig(modelBuilder.Entity<PersonaContacto>());
            #endregion

            #region  FLUENTCONFIG ATENCION WEB CONFIG
            new AtencionWebConfig(modelBuilder.Entity<AtencionWeb>());
            new AtencionWebAnexoConfig(modelBuilder.Entity<AtencionWebAnexo>());
            new AtencionWebReasignacionConfig(modelBuilder.Entity<AtencionWebReasignacion>());
            new AtencionSeguimientoConfig(modelBuilder.Entity<AtencionSeguimiento>());
            new AtencionPersonaWebConfig(modelBuilder.Entity<PersonaWeb>());
            #endregion

            #region  FLUENTCONFIG ATENCION GRUPAL CONFIG
            new AtencionGrupalConfig(modelBuilder.Entity<AtencionGrupal>());
            new AtencionGrupalAnexoConfig(modelBuilder.Entity<AtencionGrupalAnexo>());    
            #endregion

        }

        #region ATENCIONES INDIVIDUALES DOMINIO
        public DbSet<AtencionActor> AtencionActor { get; set; }
        public DbSet<AtencionAnexo> AtencionAnexo { get; set; }
        public DbSet<AtencionIndividual> AtencionIndividual { get; set; }
        public DbSet<AtencionReasignacion> AtencionReasignacion { get; set; }
        public DbSet<AtencionSeguimiento> AtencionSeguimiento { get; set; }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<PersonaAfiliacion> PersonaAfiliacion { get; set; }
        public DbSet<PersonaContacto> PersonaContacto { get; set; }

        #endregion

        #region ATENCIONES WEB DOMINIO
        public DbSet<AtencionWeb> AtencionWeb { get; set; }
        public DbSet<AtencionWebAnexo> AtencionWebAnexo { get; set; }
        public DbSet<AtencionWebReasignacion> AtencionWebReasignacion { get; set; }
        public DbSet<AtencionWebSeguimiento> AtencionWebSeguimiento { get; set; }
        public DbSet<PersonaWeb> PersonaWeb { get; set; }
        #endregion


        #region ATENCIONES GRUPALES DOMINIO
        public DbSet<AtencionGrupal> AtencionGrupal { get; set; }
        public DbSet<AtencionWebAnexo> AtencionGrupalAnexo { get; set; }
        #endregion


    }
}