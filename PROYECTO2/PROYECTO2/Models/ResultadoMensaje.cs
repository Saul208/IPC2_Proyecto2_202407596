using PROYECTO2.Estructuras;

namespace PROYECTO2.Models
{
    // El resultado final de un mensaje procesado
    public class ResultadoMensaje
    {
        public string NombreMensaje { get; set; } = string.Empty;
        public string SistemaDrones { get; set; } = string.Empty;
        public int TiempoOptimo { get; set; }
        public string MensajeRecibido { get; set; } = string.Empty;
        public ListaDinamica<PasoTiempo> Pasos { get; set; }

        public ResultadoMensaje()
        {
            Pasos = new ListaDinamica<PasoTiempo>();
        }
    }
}
