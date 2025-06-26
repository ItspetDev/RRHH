namespace RRHH_Backend.Application.DTO
{
    public record TitulosEnCursoDTO
    {
        public int IdTitulosProfesorCurso { get; set; }
        public string IdProfesor { get; set; }
        public string Titulo { get; set; }
        public int IdUniversidad { get; set; }
        public int IdGradoAcademico { get; set; }
        public int IdCampoDetalladoUnesco { get; set; }
        public DateOnly? FechaInicio { get; set; }
        public bool? TieneBeca { get; set; }
        public int? PorcentajeBeca { get; set; }
        public int IdTipoBeca { get; set; }
        public decimal? MontoBeca { get; set; }
        public int IdFinanciamiento { get; set; }
        public string NombreOtro { get; set; }
    }
}
