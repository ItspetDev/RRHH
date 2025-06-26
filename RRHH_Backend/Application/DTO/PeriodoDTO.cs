namespace RRHH_Backend.Application.DTO
{
    public record PeriodoDTO
    {
        public string IdPeriodo { get; set; }
        public string Detalle { get; set; }
        public DateOnly? FechaInicial { get; set; }
        public DateOnly? FechaFinal { get; set; }
        public bool? Cerrado { get; set; }
        public DateOnly? FechaMaximaAutocierre { get; set; }
        public bool? Activo { get; set; }
        public bool? Creditos { get; set; }
        public uint? NumeroPagos { get; set; }
        public DateOnly? FechaMatruclaExtraordinaria { get; set; }
        public int? Foliop { get; set; }
        public bool? PermiteMatricula { get; set; }
        public bool? IngresoCalificaciones { get; set; }
        public bool? PermiteCalificacionesInstituto { get; set; }
        public bool? Periodoactivoinstituto { get; set; }
        public bool? VisualizaPowerBi { get; set; }
    }
}
