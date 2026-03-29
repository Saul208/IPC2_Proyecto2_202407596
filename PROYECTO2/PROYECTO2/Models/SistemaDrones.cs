using PROYECTO2.Estructuras;

namespace PROYECTO2.Models
{
    public class SistemaDrones
    {
        public string Nombre { get; set; } = string.Empty;
        public int AlturaMaxima { get; set; }
        public int CantidadDrones { get; set; }
        public ListaDinamica<Dron> Drones { get; set; }
        public ListaDinamica<AlturaLetra> Alturas { get; set; }

        public SistemaDrones()
        {
            Drones = new ListaDinamica<Dron>();
            Alturas = new ListaDinamica<AlturaLetra>();
        }
    }
}