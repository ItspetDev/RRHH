namespace RRHH_Backend.Application.DTO
{
    public record TiposContratosDTO
    {
        public int IdtiposContratos { get; set; }
        public string Nombre { get; set; }
        public int? Duracion { get; set; }
        public bool? EsAfiliado { get; set; }
    }
}
