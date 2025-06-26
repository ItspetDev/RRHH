namespace RRHH_Backend.Application.DTO
{
    public record CampoEspecificoUnescoDTO
    {
        public int IdCampospecificoUnesco { get; set; }
        public int IdCampoAmplioUnesco { get; set; }
        public string NombreEspecifico { get; set; }
        public string CodigoEspecifico { get; set; }
        public bool? Activo { get; set; }
    }
}
