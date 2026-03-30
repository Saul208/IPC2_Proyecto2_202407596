using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROYECTO2.Models;
using PROYECTO2.Services;
using PROYECTO2.Estructuras;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace PROYECTO2.Pages
{
    public class MensajesModel : PageModel
    {
        private readonly DatosGlobales _datos;
        private readonly ServicioGraphviz _graphviz;
        private readonly IWebHostEnvironment _env;

        public MensajesModel(DatosGlobales datos, ServicioGraphviz graphviz, IWebHostEnvironment env)
        {
            _datos = datos;
            _graphviz = graphviz;
            _env = env;
        }

        public List<Mensaje> ListaMensajesUI { get; set; } = new List<Mensaje>();
        public ResultadoMensaje? ResultadoActual { get; set; }
        public string RutaImagenInstrucciones { get; set; } = string.Empty;

        public void OnGet(string? nombreMensajeSeleccionado)
        {
            // Cargar la lista lateral ordenada
            for (int i = 0; i < _datos.Mensajes.Tamaño; i++)
            {
                ListaMensajesUI.Add(_datos.Mensajes.Obtener(i));
            }
            ListaMensajesUI = ListaMensajesUI.OrderBy(m => m.Nombre).ToList();

            // Simular si el usuario hizo clic
            if (!string.IsNullOrEmpty(nombreMensajeSeleccionado))
            {
                Mensaje? mensajeEncontrado = null;
                SistemaDrones? sistemaEncontrado = null;

                for (int i = 0; i < _datos.Mensajes.Tamaño; i++)
                {
                    if (_datos.Mensajes.Obtener(i).Nombre == nombreMensajeSeleccionado)
                    {
                        mensajeEncontrado = _datos.Mensajes.Obtener(i);
                        break;
                    }
                }

                if (mensajeEncontrado != null)
                {
                    for (int i = 0; i < _datos.Sistemas.Tamaño; i++)
                    {
                        if (_datos.Sistemas.Obtener(i).Nombre == mensajeEncontrado.NombreSistemaDrones)
                        {
                            sistemaEncontrado = _datos.Sistemas.Obtener(i);
                            break;
                        }
                    }
                }

                if (mensajeEncontrado != null && sistemaEncontrado != null)
                {
                    Simulador sim = new Simulador();
                    ResultadoActual = sim.ProcesarMensaje(mensajeEncontrado, sistemaEncontrado);

                    string rutaImagenes = Path.Combine(_env.WebRootPath, "images");
                    RutaImagenInstrucciones = _graphviz.GenerarGraficoInstrucciones(ResultadoActual, rutaImagenes);
                }
            }
        }

        public IActionResult OnPostDescargarXML()
        {
            Simulador sim = new Simulador();
            ListaResultadoMensaje listaResultadosTDA = new ListaResultadoMensaje();

            for (int i = 0; i < _datos.Mensajes.Tamaño; i++)
            {
                Mensaje msj = _datos.Mensajes.Obtener(i);
                SistemaDrones? sis = null;

                for (int j = 0; j < _datos.Sistemas.Tamaño; j++)
                {
                    if (_datos.Sistemas.Obtener(j).Nombre == msj.NombreSistemaDrones)
                    {
                        sis = _datos.Sistemas.Obtener(j);
                        break;
                    }
                }

                if (sis != null)
                {
                    listaResultadosTDA.Agregar(sim.ProcesarMensaje(msj, sis));
                }
            }

            string rutaTemp = Path.GetTempFileName();
            EscritorXML escritor = new EscritorXML();
            escritor.GenerarSalida(listaResultadosTDA, rutaTemp);

            byte[] fileBytes = System.IO.File.ReadAllBytes(rutaTemp);
            System.IO.File.Delete(rutaTemp);

            return File(fileBytes, "application/xml", "Salida.xml");
        }
    }
}