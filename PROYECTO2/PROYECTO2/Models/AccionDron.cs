namespace PROYECTO2.Models
{
    // Guarda lo que hace un dron en un segundo específico
    public class AccionDron
    {
        public string NombreDron { get; set; } = string.Empty;
        public string Accion { get; set; } = string.Empty; // "Subir", "Bajar", "Esperar", "Emitir luz"
    }
}