using PROYECTO2.Estructuras;
using PROYECTO2.Models;

namespace PROYECTO2.Services
{
    public class DatosGlobales
    {
        public ListaDron Drones { get; set; } = new ListaDron();
        public ListaSistemaDrones Sistemas { get; set; } = new ListaSistemaDrones();
        public ListaMensaje Mensajes { get; set; } = new ListaMensaje();

        public void InicializarSistema()
        {
            Drones = new ListaDron();
            Sistemas = new ListaSistemaDrones();
            Mensajes = new ListaMensaje();
        }
    }
}