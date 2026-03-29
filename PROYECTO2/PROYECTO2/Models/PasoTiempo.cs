using PROYECTO2.Estructuras;

namespace PROYECTO2.Models
{
    // Guarda todas las acciones de todos los drones en un segundo (t)
    public class PasoTiempo
    {
        public int Tiempo { get; set; }
        public ListaAccionDron Acciones { get; set; } = new ListaAccionDron();
    }
}