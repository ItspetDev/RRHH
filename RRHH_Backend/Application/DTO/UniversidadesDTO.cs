namespace RRHH_Backend.Application.DTO
{
    public record UniversidadesDTO
    {
        public int IdUniversidad { get; set; }
        public int Idpaises { get; set; }
        public string Nombre { get; set; }
        public string CodigoSiees { get; set; }
    }
}
