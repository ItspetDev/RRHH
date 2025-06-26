namespace RRHH_Backend.Application.DTO
{
    public record ContratosDTO
    {
        public int IdContratro { get; set; }
        public string NumeroContrato { get; set; }
        public string IdProfesor { get; set; }
        public int TipoFuncionario { get; set; }
        public int IdDedicacion { get; set; }
        public int TipoContrato { get; set; }
        public int IdCategoria { get; set; }
        public int IdRelacionIes { get; set; }
        public bool? EsAdendum { get; set; }
        public string ContratoVinculado { get; set; }
        public decimal? Sueldo { get; set; }
        public DateOnly? FechaRegistro { get; set; }
        public DateOnly? FechaInicio { get; set; }
        public DateOnly? FechaFinal { get; set; }
        public bool? EsActivo { get; set; }
        public string ArchivoContrato { get; set; }
        public string ArchivoLegalizado { get; set; }
        public string ArchivoFiniquito { get; set; }
        public string ArchivoLegalizadoSalida { get; set; }
        public bool? IngresoConcurso { get; set; }
    }
}
