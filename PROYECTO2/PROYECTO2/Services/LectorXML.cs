using System;
using System.Xml;
using PROYECTO2.Estructuras;
using PROYECTO2.Models;

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
            DronesGlobales = new ListaDinamica<Dron>();
            SistemasGlobales = new ListaDinamica<SistemaDrones>();
            MensajesGlobales = new ListaDinamica<Mensaje>();
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

                    // Drones del sistema
                    XmlNodeList? dronesSistema = nodoSistema.SelectNodes("contenido/dron");
                    if (dronesSistema != null)
                    {
                        foreach (XmlNode d in dronesSistema)
                        {
                            nuevoSistema.Drones.Agregar(new Dron { Nombre = d.InnerText.Trim() });
                        }
                    }

                    // Alturas del sistema
                    XmlNodeList? alturasSistema = nodoSistema.SelectNodes("contenido/alturas/altura");
                    if (alturasSistema != null)
                    {
                        foreach (XmlNode a in alturasSistema)
                        {
                            if (a.Attributes?["valor"] != null)
                            {
                                nuevoSistema.Alturas.Agregar(new AlturaLetra
                                {
                                    Valor = int.Parse(a.Attributes["valor"]!.Value),
                                    Letra = a.InnerText.Trim()
                                });
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
    }
}