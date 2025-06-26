namespace RRHH_Backend.Application.DTO
{
    public record EtniasDTO
    {
        public int IdEtnia { get; set; }
        public string Etnia { get; set; }
        public bool? EsIndigena { get; set; }
        public bool? NoRegistra { get; set; }
    }
}
