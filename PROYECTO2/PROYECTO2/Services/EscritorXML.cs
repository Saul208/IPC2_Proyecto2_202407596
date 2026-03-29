using System.Xml;
using PROYECTO2.Estructuras;
using PROYECTO2.Models;

namespace PROYECTO2.Services
{
    public class EscritorXML
    {
        public void GenerarSalida(ListaDinamica<ResultadoMensaje> resultados, string rutaDestino)
        {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(xmlDeclaration);

            XmlElement root = doc.CreateElement("respuesta");
            doc.AppendChild(root);

            XmlElement listaMensajes = doc.CreateElement("listaMensajes");
            root.AppendChild(listaMensajes);

            for (int i = 0; i < resultados.Tamaño; i++)
            {
                ResultadoMensaje res = resultados.Obtener(i);

                XmlElement mensaje = doc.CreateElement("mensaje");
                mensaje.SetAttribute("nombre", res.NombreMensaje);

                XmlElement sisDrones = doc.CreateElement("sistemaDrones");
                sisDrones.InnerText = res.SistemaDrones;
                mensaje.AppendChild(sisDrones);

                XmlElement tiempoOptimo = doc.CreateElement("tiempoOptimo");
                tiempoOptimo.InnerText = res.TiempoOptimo.ToString();
                mensaje.AppendChild(tiempoOptimo);

                XmlElement mensajeRecibido = doc.CreateElement("mensajeRecibido");
                mensajeRecibido.InnerText = res.MensajeRecibido;
                mensaje.AppendChild(mensajeRecibido);

                XmlElement instrucciones = doc.CreateElement("instrucciones");

                for (int j = 0; j < res.Pasos.Tamaño; j++)
                {
                    PasoTiempo paso = res.Pasos.Obtener(j);

                    XmlElement tiempo = doc.CreateElement("tiempo");
                    tiempo.SetAttribute("valor", paso.Tiempo.ToString());

                    XmlElement acciones = doc.CreateElement("acciones");

                    for (int k = 0; k < paso.Acciones.Tamaño; k++)
                    {
                        AccionDron accion = paso.Acciones.Obtener(k);

                        XmlElement dron = doc.CreateElement("dron");
                        dron.SetAttribute("nombre", accion.NombreDron);
                        dron.InnerText = accion.Accion;
                        acciones.AppendChild(dron);
                    }

                    tiempo.AppendChild(acciones);
                    instrucciones.AppendChild(tiempo);
                }

                mensaje.AppendChild(instrucciones);
                listaMensajes.AppendChild(mensaje);
            }

            doc.Save(rutaDestino);
        }
    }
}