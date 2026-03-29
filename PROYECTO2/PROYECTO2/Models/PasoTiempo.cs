using PROYECTO2.Estructuras;

namespace PROYECTO2.Models
{
    // Guarda todas las acciones de todos los drones en un segundo (t)
    public class PasoTiempo
    {
        public int Tiempo { get; set; }
        public ListaDinamica<AccionDron> Acciones { get; set; }

        public PasoTiempo()
        {
            Acciones = new ListaDinamica<AccionDron>();
        }
    }
}