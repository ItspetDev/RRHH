namespace RRHH_Backend.Application.DTO
{
    public record DiscapacidadesDTO
    {
        public int IdDiscapacidad { get; set; }
        public string Discapacidad { get; set; }
        public bool? EsDefecto { get; set; }
    }
}
