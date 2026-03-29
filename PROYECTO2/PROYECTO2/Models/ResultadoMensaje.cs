using PROYECTO2.Estructuras;

namespace PROYECTO2.Models
{
    public class ResultadoMensaje
    {
        public string NombreMensaje { get; set; } = string.Empty;
        public string SistemaDrones { get; set; } = string.Empty;
        public int TiempoOptimo { get; set; }
        public string MensajeRecibido { get; set; } = string.Empty;
        public ListaPasoTiempo Pasos { get; set; } = new ListaPasoTiempo();
    }
}