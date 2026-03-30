using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;
using PROYECTO2.Services;
using PROYECTO2.Models;

namespace PROYECTO2.Pages
{
    public class CargarXMLModel : PageModel
    {
        private readonly DatosGlobales _datos;

        public CargarXMLModel(DatosGlobales datos)
        {
            _datos = datos;
        }

        // Propiedad para recibir el archivo desde el formulario HTML
        [BindProperty]
        public IFormFile ArchivoXML { get; set; } = null!;

        public string Mensaje { get; set; } = string.Empty;

        public void OnGet()
        {
            // Se ejecuta al cargar la página por primera vez
        }

        public IActionResult OnPostCargar()
        {
            if (ArchivoXML != null && ArchivoXML.Length > 0)
            {
                // 1. Guardar el archivo temporalmente en el servidor
                string rutaTemp = Path.GetTempFileName();
                using (var stream = new FileStream(rutaTemp, FileMode.Create))
                {
                    ArchivoXML.CopyTo(stream);
                }

                // 2. Usar tu clase LectorXML para analizar el archivo
                LectorXML lector = new LectorXML();
                try
                {
                    lector.CargarDesdeArchivo(rutaTemp);

                    // 3. Pasar los datos leídos al almacén global (Carga Incremental)
                    for (int i = 0; i < lector.DronesGlobales.Tamaño; i++)
                    {
                        _datos.Drones.Agregar(lector.DronesGlobales.Obtener(i));
                    }

                    for (int i = 0; i < lector.SistemasGlobales.Tamaño; i++)
                    {
                        _datos.Sistemas.Agregar(lector.SistemasGlobales.Obtener(i));
                    }

                    for (int i = 0; i < lector.MensajesGlobales.Tamaño; i++)
                    {
                        _datos.Mensajes.Agregar(lector.MensajesGlobales.Obtener(i));
                    }

                    Mensaje = "¡Archivo XML cargado y procesado exitosamente! Los datos han sido agregados al sistema.";
                }
                catch (System.Exception ex)
                {
                    Mensaje = "Error al procesar el XML: " + ex.Message;
                }
                finally
                {
                    // 4. Limpiar eliminando el archivo temporal
                    if (System.IO.File.Exists(rutaTemp))
                    {
                        System.IO.File.Delete(rutaTemp);
                    }
                }
            }
            else
            {
                Mensaje = "Por favor, seleccione un archivo .xml válido.";
            }

            return Page();
        }
    }
}