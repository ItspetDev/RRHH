namespace RRHH_Backend.Application.DTO
{
    public record GradosAcademicosDTO
    {
        public int IdGradoAcademico { get; set; }
        public int IdNivelAcademico { get; set; }
        public string Nombre { get; set; }
    }
}
