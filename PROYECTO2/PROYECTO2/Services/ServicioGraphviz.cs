using System.Diagnostics;
using System.IO;
using PROYECTO2.Models;

namespace PROYECTO2.Services
{
    public class ServicioGraphviz
    {
        // =========================================================================
        // MÉTODO 1: Genera el gráfico del Sistema de Drones (Estilo Matrix Verde)
        // =========================================================================
        public string GenerarGraficoSistema(SistemaDrones sistema, string rutaCarpetaImagenes)
        {
            string nombreArchivo = $"sistema_{sistema.Nombre}.png";
            string rutaImagen = Path.Combine(rutaCarpetaImagenes, nombreArchivo);
            string rutaDot = Path.Combine(rutaCarpetaImagenes, $"sistema_{sistema.Nombre}.dot");

            if (!Directory.Exists(rutaCarpetaImagenes))
            {
                Directory.CreateDirectory(rutaCarpetaImagenes);
            }

            // =========================================================================
            // AHORA SÍ: TABLA DEL SISTEMA EN AZUL PROFESIONAL (ANTES VERDE MATRIX)
            // =========================================================================
            string dot = "digraph G {\n";
            dot += "  bgcolor=\"white\";\n";
            dot += "  node [shape=none, margin=0, fontname=\"Arial\"];\n";
            dot += "  tabla [label=<\n";
            dot += "    <table border=\"1\" color=\"#00BFFF\" cellborder=\"1\" cellspacing=\"0\" cellpadding=\"10\">\n";

            // Fila 1: Título del sistema en Azul Oscuro
            dot += $"      <tr><td colspan=\"{sistema.CantidadDrones + 1}\" bgcolor=\"#005f8a\"><font color=\"white\"><b>Sistema: {sistema.Nombre}</b></font></td></tr>\n";

            // Filas de datos (Desde AlturaMaxima bajando hasta 1)
            for (int h = sistema.AlturaMaxima; h >= 1; h--)
            {
                dot += "      <tr>\n";
                dot += $"        <td bgcolor=\"#4682B4\"><font color=\"white\"><b>{h}</b></font></td>\n"; // Columna Y (Altura) en Azul Acero

                for (int d = 0; d < sistema.Drones.Tamaño; d++)
                {
                    Dron dron = sistema.Drones.Obtener(d);
                    string letra = " ";

                    // Buscar la letra correspondiente
                    for (int a = 0; a < sistema.Alturas.Tamaño; a++)
                    {
                        AlturaLetra alt = sistema.Alturas.Obtener(a);
                        if (alt.NombreDron == dron.Nombre && alt.Valor == h)
                        {
                            letra = alt.Letra;
                            break;
                        }
                    }
                    // Celdas de letras en un azul muy clarito con texto negro
                    dot += $"        <td bgcolor=\"#F0F8FF\"><font color=\"black\">{letra}</font></td>\n";
                }
                dot += "      </tr>\n";
            }

            // Fila final: Nombres de los Drones en Azul Acero
            dot += "      <tr>\n";
            dot += "        <td bgcolor=\"#4682B4\"><font color=\"white\"><b>Altura<br/>(mts)</b></font></td>\n";
            for (int d = 0; d < sistema.Drones.Tamaño; d++)
            {
                Dron dron = sistema.Drones.Obtener(d);
                dot += $"        <td bgcolor=\"#4682B4\"><font color=\"white\"><b>{dron.Nombre}</b></font></td>\n";
            }
            dot += "      </tr>\n";

            dot += "    </table>\n";
            dot += "  >];\n";
            dot += "}\n";

            File.WriteAllText(rutaDot, dot);

            ProcessStartInfo startInfo = new ProcessStartInfo("dot.exe");
            startInfo.Arguments = $"-Tpng \"{rutaDot}\" -o \"{rutaImagen}\"";
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            try
            {
                using (Process? process = Process.Start(startInfo))
                {
                    process?.WaitForExit();
                }
            }
            catch
            {
                return "ERROR_GRAPHVIZ";
            }

            return $"/images/{nombreArchivo}";
        }

        // =========================================================================
        // MÉTODO 2: Genera el gráfico de la simulación paso a paso (Mensajes)
        // =========================================================================
        public string GenerarGraficoInstrucciones(ResultadoMensaje resultado, string rutaCarpetaImagenes)
        {
            string nombreArchivo = $"instrucciones_{resultado.NombreMensaje}.png";
            string rutaImagen = Path.Combine(rutaCarpetaImagenes, nombreArchivo);
            string rutaDot = Path.Combine(rutaCarpetaImagenes, $"instrucciones_{resultado.NombreMensaje}.dot");

            if (!Directory.Exists(rutaCarpetaImagenes))
            {
                Directory.CreateDirectory(rutaCarpetaImagenes);
            }

            if (resultado.Pasos.Tamaño == 0) return "ERROR_SIN_PASOS";

            // =========================================================================
            // INICIO DE LA ACTUALIZACIÓN: PALETA DE AZULES PROFESIONALES (NÍTIDA)
            // =========================================================================
            string dot = "digraph G {\n";
            dot += "  bgcolor=\"white\";\n"; // Fondo Blanco Puro para limpieza
            // Nodos por defecto en texto negro para legibilidad
            dot += "  node [shape=none, margin=0, fontcolor=\"black\", fontname=\"Arial\"];\n";
            dot += "  tabla [label=<\n";
            // Bordes en Azul Cielo Brillante (DeepSkyBlue)
            dot += "    <table border=\"1\" color=\"#00BFFF\" cellborder=\"1\" cellspacing=\"0\" cellpadding=\"10\">\n";

            // Fila de Título Principal: Fondo Azul Marino Oscuro, Texto Blanco
            int cantDrones = resultado.Pasos.Obtener(0).Acciones.Tamaño;
            dot += $"      <tr><td colspan=\"{cantDrones + 1}\" bgcolor=\"#005f8a\"><font color=\"white\"><b>Instrucciones Óptimas: {resultado.NombreMensaje}</b></font></td></tr>\n";

            // Fila de Encabezados (Tiempo, Drones): Fondo Azul Acero (SteelBlue), Texto Blanco
            dot += "      <tr>\n";
            dot += "        <td bgcolor=\"#4682B4\"><font color=\"white\"><b>Tiempo (seg)</b></font></td>\n";
            for (int i = 0; i < cantDrones; i++)
            {
                string nombreDron = resultado.Pasos.Obtener(0).Acciones.Obtener(i).NombreDron;
                dot += $"        <td bgcolor=\"#4682B4\"><font color=\"white\"><b>{nombreDron}</b></font></td>\n";
            }
            dot += "      </tr>\n";

            // Filas de Datos (Cada segundo)
            for (int i = 0; i < resultado.Pasos.Tamaño; i++)
            {
                PasoTiempo paso = resultado.Pasos.Obtener(i);

                // Usamos colores alternos muy sutiles para las filas para mejorar la lectura ("nitidez")
                if (i % 2 == 0) dot += "      <tr>\n";
                else dot += "      <tr bgcolor=\"#F0F8FF\">\n"; // Azul Alicia (muy claro)

                // Tiempo en texto negro por defecto
                dot += $"        <td>{paso.Tiempo}</td>\n";

                for (int j = 0; j < paso.Acciones.Tamaño; j++)
                {
                    AccionDron accion = paso.Acciones.Obtener(j);

                    // Colorear las acciones semánticamente para fondo claro
                    string colorTexto = "black"; // Por defecto negro
                    if (accion.Accion.ToLower() == "emitir luz") colorTexto = "#B8860B"; // Oro Oscuro (para que se lea en blanco)
                    else if (accion.Accion.ToLower() == "esperar") colorTexto = "#A9A9A9"; // Gris Oscuro
                    else if (accion.Accion.ToLower() == "subir") colorTexto = "#008000"; // Verde Oscuro
                    else if (accion.Accion.ToLower() == "bajar") colorTexto = "#FF8C00"; // Naranja Oscuro

                    dot += $"        <td><font color=\"{colorTexto}\">{accion.Accion}</font></td>\n";
                }
                dot += "      </tr>\n";
            }

            dot += "    </table>\n";
            dot += "  >];\n";
            dot += "}\n";
            // =========================================================================
            // FIN DE LA ACTUALIZACIÓN
            // =========================================================================

            File.WriteAllText(rutaDot, dot);

            ProcessStartInfo startInfo = new ProcessStartInfo("dot.exe");
            startInfo.Arguments = $"-Tpng \"{rutaDot}\" -o \"{rutaImagen}\"";
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            try
            {
                using (Process? process = Process.Start(startInfo))
                {
                    process?.WaitForExit();
                }
            }
            catch
            {
                return "ERROR_GRAPHVIZ";
            }

            return $"/images/{nombreArchivo}";
        }
    }
}