namespace RRHH_Backend.Application.DTO
{
    public record CampoAmplioUnescoDTO
    {
        public int IdCampoAmplioUnesco { get; set; }
        public string Nombre { get; set; }
        public string CodigoAmplio { get; set; }
        public bool? Activo { get; set; }
    }
}
