using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROYECTO2.Services;

namespace PROYECTO2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DatosGlobales _datos;

        public string MensajeInicializacion { get; set; } = string.Empty;

        public IndexModel(DatosGlobales datos)
        {
            _datos = datos;
        }

        public void OnGet() { }

        public void OnPostInicializar()
        {
            // Llama al método de tu almacén para limpiar todo
            _datos.InicializarSistema();
            MensajeInicializacion = "El sistema se ha inicializado correctamente. La memoria está limpia.";
        }
    }
}