namespace RRHH_Backend.Application.DTO
{
    public record ProfesoresDTO
    {
        public string IdProfesor { get; set; }
        public string Tipodocumento { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public int EstadoCivil { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public int IdEtnia { get; set; }
        public int IdNacionalidad { get; set; }
        public int IdParroquiaNacimiento { get; set; }
        public int IdParroquiaResidencia { get; set; }
        public string TipoSangre { get; set; }
        public string CodigoPostal { get; set; }
        public int IdDiscapacidad { get; set; }
        public int? PorcentajeDiscapacidad { get; set; }
        public string NumeroConadis { get; set; }
        public string Foto { get; set; }
        public string Clave { get; set; }
        public bool? Practicas { get; set; }
        public string Tipo { get; set; }
        public string EmailInstitucional { get; set; }
        public DateOnly? FechaIngreso { get; set; }
        public DateOnly? FechaIngresoIess { get; set; }
        public DateOnly? FechaRetiro { get; set; }
        public string Abreviatura { get; set; }
        public string AbreviaturaPost { get; set; }
        public bool? Activo { get; set; }
    }
}
