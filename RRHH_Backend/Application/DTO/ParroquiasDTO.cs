namespace RRHH_Backend.Application.DTO
{
    public record ParroquiasDTO
    {
        public int IdParroquias { get; set; }
        public int Idciudades { get; set; }
        public string Nombre { get; set; }
    }
}
