using PROYECTO2.Estructuras;
using PROYECTO2.Models;
using System;
using System.Diagnostics;
using System.Xml;

namespace PROYECTO2.Services
{
    public class LectorXML
    {
        // Listas globales que simulan tu "Base de Datos" temporal
        public ListaDron DronesGlobales { get; private set; } = new ListaDron();
        public ListaSistemaDrones SistemasGlobales { get; private set; } = new ListaSistemaDrones();
        public ListaMensaje MensajesGlobales { get; private set; } = new ListaMensaje();

        public LectorXML()
        {
            DronesGlobales = new ListaDron();
            SistemasGlobales = new ListaSistemaDrones();
            MensajesGlobales = new ListaMensaje();
        }

        public void CargarDesdeArchivo(string rutaArchivo)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(rutaArchivo); // Carga el XML desde la ruta física

            // 1. Leer Drones
            XmlNodeList? nodosDron = doc.SelectNodes("//listaDrones/dron");
            if (nodosDron != null)
            {
                foreach (XmlNode nodo in nodosDron)
                {
                    DronesGlobales.Agregar(new Dron { Nombre = nodo.InnerText.Trim() });
                }
            }

            // 2. Leer Sistemas de Drones
            XmlNodeList? nodosSistemas = doc.SelectNodes("//listaSistemasDrones/sistemaDrones");
            if (nodosSistemas != null)
            {
                foreach (XmlNode nodoSistema in nodosSistemas)
                {
                    SistemaDrones nuevoSistema = new SistemaDrones();

                    if (nodoSistema.Attributes?["nombre"] != null)
                        nuevoSistema.Nombre = nodoSistema.Attributes["nombre"]!.Value;

                    if (nodoSistema["alturaMaxima"] != null)
                        nuevoSistema.AlturaMaxima = int.Parse(nodoSistema["alturaMaxima"]!.InnerText);

                    if (nodoSistema["cantidadDrones"] != null)
                        nuevoSistema.CantidadDrones = int.Parse(nodoSistema["cantidadDrones"]!.InnerText);

                    // --- NUEVA LÓGICA DE LECTURA POR BLOQUES ---
                    XmlNodeList? nodosContenido = nodoSistema.SelectSingleNode("contenido")?.ChildNodes;
                    string dronActual = "";

                    if (nodosContenido != null)
                    {
                        foreach (XmlNode nodo in nodosContenido)
                        {
                            if (nodo.Name == "dron")
                            {
                                dronActual = nodo.InnerText.Trim();
                                nuevoSistema.Drones.Agregar(new Dron { Nombre = dronActual });
                            }
                            else if (nodo.Name == "alturas")
                            {
                                foreach (XmlNode a in nodo.ChildNodes)
                                {
                                    if (a.Name == "altura" && a.Attributes?["valor"] != null)
                                    {
                                        nuevoSistema.Alturas.Agregar(new AlturaLetra
                                        {
                                            NombreDron = dronActual, // Asignamos el dueño
                                            Valor = int.Parse(a.Attributes["valor"]!.Value),
                                            Letra = a.InnerText.Trim()
                                        });
                                    }
                                }
                            }
                        }
                    }
                    SistemasGlobales.Agregar(nuevoSistema);
                }
            }

            // 3. Leer Mensajes
            XmlNodeList? nodosMensajes = doc.SelectNodes("//listaMensajes/Mensaje");
            if (nodosMensajes != null)
            {
                foreach (XmlNode nodoMensaje in nodosMensajes)
                {
                    Mensaje nuevoMensaje = new Mensaje();

                    if (nodoMensaje.Attributes?["nombre"] != null)
                        nuevoMensaje.Nombre = nodoMensaje.Attributes["nombre"]!.Value;

                    if (nodoMensaje["sistemaDrones"] != null)
                        nuevoMensaje.NombreSistemaDrones = nodoMensaje["sistemaDrones"]!.InnerText.Trim();

                    XmlNodeList? instrucciones = nodoMensaje.SelectNodes("instrucciones/instruccion");
                    if (instrucciones != null)
                    {
                        foreach (XmlNode inst in instrucciones)
                        {
                            if (inst.Attributes?["dron"] != null)
                            {
                                nuevoMensaje.Instrucciones.Agregar(new Instruccion
                                {
                                    NombreDron = inst.Attributes["dron"]!.Value,
                                    ValorAltura = int.Parse(inst.InnerText.Trim())
                                });
                            }
                        }
                    }

                    MensajesGlobales.Agregar(nuevoMensaje);
                }
            }
        }

        public string GenerarGraficoInstrucciones(ResultadoMensaje resultado, string rutaCarpetaImagenes)
        {
            string nombreArchivo = $"instrucciones_{resultado.NombreMensaje}.png";
            string rutaImagen = Path.Combine(rutaCarpetaImagenes, nombreArchivo);
            string rutaDot = Path.Combine(rutaCarpetaImagenes, $"instrucciones_{resultado.NombreMensaje}.dot");

            if (!Directory.Exists(rutaCarpetaImagenes))
            {
                Directory.CreateDirectory(rutaCarpetaImagenes);
            }

            // Validar que haya pasos para graficar
            if (resultado.Pasos.Tamaño == 0) return "ERROR_SIN_PASOS";

            string dot = "digraph G {\n";
            dot += "  bgcolor=\"#1e1e1e\";\n"; // Fondo oscuro
            dot += "  node [shape=none, margin=0];\n";
            dot += "  tabla [label=<\n";
            dot += "    <table border=\"1\" color=\"#00BFFF\" cellborder=\"1\" cellspacing=\"0\" cellpadding=\"10\">\n";

            // Título
            int cantDrones = resultado.Pasos.Obtener(0).Acciones.Tamaño;
            dot += $"      <tr><td colspan=\"{cantDrones + 1}\" bgcolor=\"#005f8a\"><font color=\"white\"><b>Instrucciones: {resultado.NombreMensaje}</b></font></td></tr>\n";

            // Encabezados de Columna (Tiempo + Nombres de Drones)
            dot += "      <tr>\n";
            dot += "        <td bgcolor=\"#008CBA\"><font color=\"white\"><b>Tiempo (seg)</b></font></td>\n";
            for (int i = 0; i < cantDrones; i++)
            {
                string nombreDron = resultado.Pasos.Obtener(0).Acciones.Obtener(i).NombreDron;
                dot += $"        <td bgcolor=\"#008CBA\"><font color=\"white\"><b>{nombreDron}</b></font></td>\n";
            }
            dot += "      </tr>\n";

            // Filas de Datos (Cada segundo)
            for (int i = 0; i < resultado.Pasos.Tamaño; i++)
            {
                PasoTiempo paso = resultado.Pasos.Obtener(i);
                dot += "      <tr>\n";
                dot += $"        <td><font color=\"white\">{paso.Tiempo}</font></td>\n";

                for (int j = 0; j < paso.Acciones.Tamaño; j++)
                {
                    AccionDron accion = paso.Acciones.Obtener(j);
                    // Colorear dependiendo de la acción
                    string colorTexto = "white";
                    if (accion.Accion.ToLower() == "emitir luz") colorTexto = "#FFFF00"; // Amarillo
                    else if (accion.Accion.ToLower() == "esperar") colorTexto = "#A9A9A9"; // Gris
                    else if (accion.Accion.ToLower() == "subir") colorTexto = "#00FF00"; // Verde
                    else if (accion.Accion.ToLower() == "bajar") colorTexto = "#FF4500"; // Naranja

                    dot += $"        <td><font color=\"{colorTexto}\">{accion.Accion}</font></td>\n";
                }
                dot += "      </tr>\n";
            }

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
    }
}