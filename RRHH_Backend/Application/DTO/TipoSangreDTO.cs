namespace RRHH_Backend.Application.DTO
{
    public record TipoSangreDTO
    {
        public string CodigoTipoSangre { get; set; }
        public string Grupo { get; set; }
        public bool? SitemaRh { get; set; }
    }
}
