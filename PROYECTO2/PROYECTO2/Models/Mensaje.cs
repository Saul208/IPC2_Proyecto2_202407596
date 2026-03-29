using PROYECTO2.Estructuras;

namespace PROYECTO2.Models
{
    public class Mensaje
    {
        public string Nombre { get; set; } = string.Empty;
        public string NombreSistemaDrones { get; set; } = string.Empty;
        public ListaDinamica<Instruccion> Instrucciones { get; set; }

        public Mensaje()
        {
            Instrucciones = new ListaDinamica<Instruccion>();
        }
    }
}