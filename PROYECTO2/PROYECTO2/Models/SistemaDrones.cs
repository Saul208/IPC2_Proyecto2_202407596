using Proyecto2.Estructuras;
using System.Collections.Generic;

namespace PROYECTO2.Models
{
    public class SistemaDrones
    {
        public string Nombre { get; set; }
        public int AlturaMaxima { get; set; }
        public int CantidadDrones { get; set; }
        public ListaEnlazada<Dron> Drones { get; set; }
        public ListaEnlazada<AlturaLetra> Alturas { get; set; }

        public SistemaDrones()
        {
            Drones = new ListaEnlazada<Dron>();
            Alturas = new ListaEnlazada<AlturaLetra>();
        }
    }
}