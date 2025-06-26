namespace RRHH_Backend.Application.DTO
{
    public record CampoDetalladoUnescoDTO
    {
        public int IdCampoDetalladoUnesco { get; set; }
        public int IdCampospecificoUnesco { get; set; }
        public string NombreDetallado { get; set; }
        public string CodigoDetallado { get; set; }
        public bool? Activo { get; set; }

    }
}
