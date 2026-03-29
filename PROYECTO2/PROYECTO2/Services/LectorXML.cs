using Proyecto2.Estructuras;
using Proyecto2.Models;
using PROYECTO2.Models;
using System;
using System.Xml;

namespace Proyecto2.Services
{
    public class LectorXML
    {
        // Listas globales que simulan tu "Base de Datos" temporal
        public ListaEnlazada<Dron> DronesGlobales { get; private set; }
        public ListaEnlazada<SistemaDrones> SistemasGlobales { get; private set; }
        public ListaEnlazada<Mensaje> MensajesGlobales { get; private set; }

        public LectorXML()
        {
            DronesGlobales = new ListaEnlazada<Dron>();
            SistemasGlobales = new ListaEnlazada<SistemaDrones>();
            MensajesGlobales = new ListaEnlazada<Mensaje>();
        }

        public void CargarDesdeArchivo(string rutaArchivo)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(rutaArchivo); // Carga el XML desde la ruta física

            // 1. Leer Drones
            XmlNodeList nodosDron = doc.SelectNodes("//listaDrones/dron");
            if (nodosDron != null)
            {
                foreach (XmlNode nodo in nodosDron)
                {
                    DronesGlobales.Agregar(new Dron { Nombre = nodo.InnerText.Trim() });
                }
            }

            // 2. Leer Sistemas de Drones
            XmlNodeList nodosSistemas = doc.SelectNodes("//listaSistemasDrones/sistemaDrones");
            if (nodosSistemas != null)
            {
                foreach (XmlNode nodoSistema in nodosSistemas)
                {
                    SistemaDrones nuevoSistema = new SistemaDrones();
                    nuevoSistema.Nombre = nodoSistema.Attributes["nombre"].Value;
                    nuevoSistema.AlturaMaxima = int.Parse(nodoSistema["alturaMaxima"].InnerText);
                    nuevoSistema.CantidadDrones = int.Parse(nodoSistema["cantidadDrones"].InnerText);

                    // Drones del sistema
                    XmlNodeList dronesSistema = nodoSistema.SelectNodes("contenido/dron");
                    foreach (XmlNode d in dronesSistema)
                    {
                        nuevoSistema.Drones.Agregar(new Dron { Nombre = d.InnerText.Trim() });
                    }

                    // Alturas del sistema
                    XmlNodeList alturasSistema = nodoSistema.SelectNodes("contenido/alturas/altura");
                    foreach (XmlNode a in alturasSistema)
                    {
                        nuevoSistema.Alturas.Agregar(new AlturaLetra
                        {
                            Valor = int.Parse(a.Attributes["valor"].Value),
                            Letra = a.InnerText.Trim()
                        });
                    }

                    SistemasGlobales.Agregar(nuevoSistema);
                }
            }

            // 3. Leer Mensajes
            XmlNodeList nodosMensajes = doc.SelectNodes("//listaMensajes/Mensaje");
            if (nodosMensajes != null)
            {
                foreach (XmlNode nodoMensaje in nodosMensajes)
                {
                    Mensaje nuevoMensaje = new Mensaje();
                    nuevoMensaje.Nombre = nodoMensaje.Attributes["nombre"].Value;
                    nuevoMensaje.NombreSistemaDrones = nodoMensaje["sistemaDrones"].InnerText.Trim();

                    XmlNodeList instrucciones = nodoMensaje.SelectNodes("instrucciones/instruccion");
                    foreach (XmlNode inst in instrucciones)
                    {
                        nuevoMensaje.Instrucciones.Agregar(new Instruccion
                        {
                            NombreDron = inst.Attributes["dron"].Value,
                            ValorAltura = int.Parse(inst.InnerText.Trim())
                        });
                    }

                    MensajesGlobales.Agregar(nuevoMensaje);
                }
            }
        }
    }
}