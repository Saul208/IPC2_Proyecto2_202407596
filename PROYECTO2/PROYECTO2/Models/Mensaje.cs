using Proyecto2.Estructuras;
using System.Collections.Generic;

namespace PROYECTO2.Models
{
    public class Mensaje
    {
        public string Nombre { get; set; }
        public string NombreSistemaDrones { get; set; }
        public ListaEnlazada<Instruccion> Instrucciones { get; set; }

        public Mensaje()
        {
            Instrucciones = new ListaEnlazada<Instruccion>();
        }
    }
}