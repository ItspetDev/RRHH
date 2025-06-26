namespace RRHH_Backend.Application.DTO
{
    public record TitulosProfesoresDTO
    {
        public int IdTitulosProfesor { get; set; }
        public string IdProfesor { get; set; }
        public string Titulo { get; set; }
        public int IdUniversidad { get; set; }
        public int IdGradoAcademico { get; set; }
        public string CodigoSenescyt { get; set; }
        public DateOnly? FechaObtencion { get; set; }
        public DateOnly? FechaRegistro { get; set; }
        public string Tituloscol { get; set; }
        public int IdCampoDetalladoUnesco { get; set; }
        public string ArchivoTitulo { get; set; }
    }
}
