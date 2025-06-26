namespace RRHH_Backend.Application.DTO
{
    public record TipoFuncionarioDTO
    {
        public int IdtipoFuncionario { get; set; }
        public string Nombre { get; set; }
        public byte? EsDocente { get; set; }
    }
}
