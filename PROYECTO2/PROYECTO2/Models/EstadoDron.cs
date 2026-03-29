namespace PROYECTO2.Models
{
    // Sirve para llevar el control de dónde está el dron en la simulación
    public class EstadoDron
    {
        public string Nombre { get; set; } = string.Empty;
        public int AlturaActual { get; set; }
    }
}