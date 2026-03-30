using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROYECTO2.Models;
using PROYECTO2.Services;
using System.Collections.Generic;
using System.Linq;

namespace PROYECTO2.Pages
{
    public class DronesModel : PageModel
    {
        private readonly DatosGlobales _datos;

        public DronesModel(DatosGlobales datos)
        {
            _datos = datos;
        }

        public List<Dron> ListaDronesUI { get; set; } = new List<Dron>();

        [BindProperty]
        public string NuevoDron { get; set; } = string.Empty;

        // Propiedad para mostrar mensajes de error/éxito en la interfaz
        public string AlertaUI { get; set; } = string.Empty;

        public void OnGet()
        {
            CargarDronesParaUI();
        }

        public IActionResult OnPostAgregar()
        {
            if (!string.IsNullOrEmpty(NuevoDron))
            {
                bool existe = false;

                // 1. Verificar si el nombre ya existe usando tu estructura TDA
                for (int i = 0; i < _datos.Drones.Tamaño; i++)
                {
                    if (_datos.Drones.Obtener(i).Nombre.ToUpper() == NuevoDron.ToUpper())
                    {
                        existe = true;
                        break;
                    }
                }

                // 2. Si es un nombre único, lo agregamos a tu TDA
                if (!existe)
                {
                    _datos.Drones.Agregar(new Dron { Nombre = NuevoDron });
                    AlertaUI = string.Empty; // Limpiamos la alerta si todo salió bien
                }
                else
                {
                    // Si ya existe, disparamos la alerta hacia la interfaz
                    AlertaUI = $"Error: El dron '{NuevoDron}' ya está registrado. Debes usar un nombre único.";
                }
            }

            CargarDronesParaUI();
            return Page();
        }

        private void CargarDronesParaUI()
        {
            ListaDronesUI = new List<Dron>();

            // Pasar del TDA a la lista nativa para que Bootstrap la pueda dibujar
            for (int i = 0; i < _datos.Drones.Tamaño; i++)
            {
                ListaDronesUI.Add(_datos.Drones.Obtener(i));
            }

            // El enunciado pide orden alfabético
            ListaDronesUI = ListaDronesUI.OrderBy(d => d.Nombre).ToList();
        }
    }
}