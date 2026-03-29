using PROYECTO2.Estructuras;

namespace PROYECTO2.Models
{
    public class SistemaDrones
    {
        public string Nombre { get; set; } = string.Empty;
        public int AlturaMaxima { get; set; }
        public int CantidadDrones { get; set; }
        public ListaDron Drones { get; set; } = new ListaDron();
        public ListaAlturaLetra Alturas { get; set; } = new ListaAlturaLetra();
    }
}