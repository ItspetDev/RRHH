namespace RRHH_Backend.Application.DTO
{
    public record CursosProfesoresDTO
    {
        public int IdCursoProfesor { get; set; }
        public string IdProfesor { get; set; }
        public string NombreCurso { get; set; }
        public string Institucion { get; set; }
        public DateOnly? FechaInicio { get; set; }
        public DateOnly? FechaFinalizacion { get; set; }
        public int? NumeroHoras { get; set; }
        public bool? EsValido { get; set; }
        public string ArchivoCurso { get; set; }
    }
}
