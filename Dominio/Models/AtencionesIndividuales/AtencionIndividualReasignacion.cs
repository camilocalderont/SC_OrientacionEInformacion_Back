namespace Dominio.Models.AtencionesIndividuales
{
    public class AtencionIndividualReasignacion
    {
        public long     Id { get; set; }
        public long     AtencionIndividualId { get; set; }
        public string   VcDescripcion { get; set; }
        public DateTime DtFechaAsignacion { get; set; }
        public long     UsuarioAsignaId { get; set; }
        public DateTime DtFechaReAsignacion { get; set; }
        public long     UsuarioActualId { get; set; }

        public AtencionIndividual AtencionIndividual { get; set; }

    }
}
