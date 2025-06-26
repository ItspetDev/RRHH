namespace RRHH_Backend.Common.Core.Helpers
{
    public static class Mensaje
    {
        public static string NotFound = "No se ha encontrado el recurso especificado";
        public static string GetMessage(bool result, string accion)
        {

            string mensaje = result ? $"Se ha {accion} al recurso correctamente" : $"Error, no se ha {accion} el recurso.";
            return mensaje;
        }

        public static string AlreadyExist(string atributo)
        {
            return $"Error, no se ha podido crear el recurso porque {atributo} ya existe.";
        }
    }
}
