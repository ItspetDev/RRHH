namespace RRHH_Backend.Application.DTO
{
    public record NacionalidadesDTO
    {
        public int IdNacionalidad { get; set; }
        public string Nacionalidad { get; set; }
        public bool? EsNinguna { get; set; }
    }
}
