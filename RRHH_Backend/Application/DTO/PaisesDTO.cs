namespace RRHH_Backend.Application.DTO
{
    public record PaisesDTO
    {
        public int Idpaises { get; set; }
        public string Nombre { get; set; }
        public string Nacionalidad { get; set; }
        public bool? EsEcuador { get; set; }
    }
}
