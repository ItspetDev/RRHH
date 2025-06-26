namespace RRHH_Backend.Application.DTO
{
    public record EstadoCivilDTO
    {
        public int IdestadoCivil { get; set; }
        public string Nombre { get; set; }
        public bool? RequiereConyuge { get; set; }
    }
}
