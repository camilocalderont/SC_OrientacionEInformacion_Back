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
            new PersonaConfig(modelBuilder.Entity<Persona>());
            new PersonaAfiliacionConfig(modelBuilder.Entity<PersonaAfiliacion>());
            new PersonaContactoConfig(modelBuilder.Entity<PersonaContacto>());
            new AtencionIndividualConfig(modelBuilder.Entity<AtencionIndividual>());
            new AtencionIndividualActorConfig(modelBuilder.Entity<AtencionIndividualActor>());
            new AtencionIndividualAnexoConfig(modelBuilder.Entity<AtencionIndividualAnexo>());            
            new AtencionIndividualReasignacionConfig(modelBuilder.Entity<AtencionIndividualReasignacion>());
            new AtencionIndividualSeguimientoConfig(modelBuilder.Entity<AtencionIndividualSeguimiento>());
            #endregion

            #region  FLUENTCONFIG ATENCION WEB CONFIG
            new AtencionPersonaWebConfig(modelBuilder.Entity<PersonaWeb>());
            new AtencionWebConfig(modelBuilder.Entity<AtencionWeb>());
            new AtencionWebAnexoConfig(modelBuilder.Entity<AtencionWebAnexo>());
            new AtencionWebReasignacionConfig(modelBuilder.Entity<AtencionWebReasignacion>());
            new AtencionIndividualSeguimientoConfig(modelBuilder.Entity<AtencionIndividualSeguimiento>());
            #endregion

            #region  FLUENTCONFIG ATENCION GRUPAL CONFIG
            new AtencionGrupalConfig(modelBuilder.Entity<AtencionGrupal>());
            new AtencionGrupalAnexoConfig(modelBuilder.Entity<AtencionGrupalAnexo>());    
            #endregion

        }

        #region ATENCIONES INDIVIDUALES DOMINIO
        public DbSet<AtencionIndividualActor> AtencionIndividualActor { get; set; }
        public DbSet<AtencionIndividualAnexo> AtencionIndividualAnexo { get; set; }
        public DbSet<AtencionIndividual> AtencionIndividual { get; set; }
        public DbSet<AtencionIndividualReasignacion> AtencionIndividualReasignacion { get; set; }
        public DbSet<AtencionIndividualSeguimiento> AtencionIndividualSeguimiento { get; set; }
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