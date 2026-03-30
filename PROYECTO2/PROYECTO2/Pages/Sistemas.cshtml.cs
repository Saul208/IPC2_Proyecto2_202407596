using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROYECTO2.Models;
using PROYECTO2.Services;
using System.Collections.Generic;
using System.IO;

namespace PROYECTO2.Pages
{
    // Clase de apoyo para enviar a la UI el sistema y la ruta de su imagen
    public class SistemaUI
    {
        public SistemaDrones Sistema { get; set; } = new SistemaDrones();
        public string RutaImagen { get; set; } = string.Empty;
    }

    public class SistemasModel : PageModel
    {
        private readonly DatosGlobales _datos;
        private readonly ServicioGraphviz _graphviz;
        private readonly IWebHostEnvironment _env; // Para obtener la ruta física de wwwroot

        public SistemasModel(DatosGlobales datos, ServicioGraphviz graphviz, IWebHostEnvironment env)
        {
            _datos = datos;
            _graphviz = graphviz;
            _env = env;
        }

        // Lista nativa puente para la UI
        public List<SistemaUI> ListaSistemasUI { get; set; } = new List<SistemaUI>();

        public void OnGet()
        {
            string rutaImagenes = Path.Combine(_env.WebRootPath, "images");

            // Recorremos tu Lista Tipada (TDA)
            for (int i = 0; i < _datos.Sistemas.Tamaño; i++)
            {
                SistemaDrones sistemaActual = _datos.Sistemas.Obtener(i);

                // Generamos la imagen
                string rutaRelativa = _graphviz.GenerarGraficoSistema(sistemaActual, rutaImagenes);

                // Lo agregamos a la lista de la UI
                ListaSistemasUI.Add(new SistemaUI
                {
                    Sistema = sistemaActual,
                    RutaImagen = rutaRelativa
                });
            }
        }
    }
}