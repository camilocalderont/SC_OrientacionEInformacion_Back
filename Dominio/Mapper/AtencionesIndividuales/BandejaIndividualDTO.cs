namespace Dominio.Mapper.AtencionesIndividuales
{
    public class BandejaIndividualDTO
    {
        public long             Id { get; set; }
        public long             PersonaId { get; set; }
        public DateTime         DtFechaRegistro { get; set; }
        public long             UsuarioId { get; set; }
        public long             CanalAtencionId { get; set; }
        public string           VcTurnoSat { get; set; }
        public long             TipoSolicitudId { get; set; }
        public long             MotivoId { get; set; }
o        public long             SubMtivoId { get; set; }
        public string           TxAclaracionMotivo { get; set; }
        public string           TxGestionRealizada { get; set; }
        public string           VcRadicadoBte { get; set; }
        public long             EstadoId { get; set; }
        public int              IAnexos { get; set; }
        public long             UsuarioActualId { get; set; }

        public long             TipoDocumentoId { get; set; }
        public string           VcDocumento   { get; set; }
        public string           VcNombrecompleto { get; set; }

         public  long           GeneroId { get; set; }
        public  string          VcOtroGenero { get; set; }
        public  string          VcNombreIdentitario { get; set; }
        public  long            OrientacionSexualId { get; set; }
        public  string          VcOtraOrientacionSexual { get; set; }
        public  long            SexoId { get; set; }
        public  DateTime?       DtFechaNacimiento { get; set; }
        public  long            EnfoquePoblacionalId { get; set; }
        public  long?           HechoVictimizanteId { get; set; }
        public  long?           DepartamentoOrigenVictimaId { get; set; }
        public  long?           MunicipioOrigenVictimaId { get; set; }
        public  long            EtniaId { get; set; }
        public  long?           SubEtniaId { get; set; }
        public  long            PoblacionPrioritariaId { get; set; }
        public  long?           SubPoblacionPrioritariaId { get; set; }
        public  string          VcCorreo { get; set; }
        public  DateTime?       DtFechaActualizacion { get; set; }
        public  long?           UsuarioActualizacionId { get; set; }

        public long             RegimenId { get; set; }
        public long             AseguradoraId { get; set; }
        public long             EstadoAfiliacionId { get; set; }
        public long             NivelSisbenId { get; set; }
        public long?            InstitucionInstrumentoVinculadoId { get; set; }        


        public long             PaisId { get; set; }
        public long             DepartamentoId { get; set; }
        public long             MunicipioId { get; set; }
        public long             LocalidadId { get; set; }
        public long?            UpzId { get; set; }
        public long?            BarrioId { get; set; }
        public long             ZonaId { get; set; }
        public string           VcDireccion { get; set; }
        public string           TxDatosContactoAclaraciones { get; set; }
        public string           VcTelefono1 { get; set; }
        public string           VcTelefono2 { get; set; }    

        public IEnumerable<AtencionIndividualActorDTO> Actores {get; set; }
        public IEnumerable<AtencionIndividualSeguimientoDTO> Seguimientos {get; set; }
        public IEnumerable<AtencionIndividualReasignacionDTO> Reasignaciones {get; set; } 

    }
}
