using System;
using PROYECTO2.Models;

namespace PROYECTO2.Estructuras
{
    public class ListaDron
    {
        public NodoDron? Cabeza { get; private set; }
        public int Tamaño { get; private set; }
        public void Agregar(Dron valor) { NodoDron nuevo = new NodoDron(valor); if (Cabeza == null) Cabeza = nuevo; else { NodoDron actual = Cabeza; while (actual.Siguiente != null) actual = actual.Siguiente; actual.Siguiente = nuevo; } Tamaño++; }
        public Dron Obtener(int indice) { NodoDron actual = Cabeza!; for (int i = 0; i < indice; i++) actual = actual.Siguiente!; return actual.Valor; }
    }

    public class ListaAlturaLetra
    {
        public NodoAlturaLetra? Cabeza { get; private set; }
        public int Tamaño { get; private set; }
        public void Agregar(AlturaLetra valor) { NodoAlturaLetra nuevo = new NodoAlturaLetra(valor); if (Cabeza == null) Cabeza = nuevo; else { NodoAlturaLetra actual = Cabeza; while (actual.Siguiente != null) actual = actual.Siguiente; actual.Siguiente = nuevo; } Tamaño++; }
        public AlturaLetra Obtener(int indice) { NodoAlturaLetra actual = Cabeza!; for (int i = 0; i < indice; i++) actual = actual.Siguiente!; return actual.Valor; }
    }

    public class ListaSistemaDrones
    {
        public NodoSistemaDrones? Cabeza { get; private set; }
        public int Tamaño { get; private set; }
        public void Agregar(SistemaDrones valor) { NodoSistemaDrones nuevo = new NodoSistemaDrones(valor); if (Cabeza == null) Cabeza = nuevo; else { NodoSistemaDrones actual = Cabeza; while (actual.Siguiente != null) actual = actual.Siguiente; actual.Siguiente = nuevo; } Tamaño++; }
        public SistemaDrones Obtener(int indice) { NodoSistemaDrones actual = Cabeza!; for (int i = 0; i < indice; i++) actual = actual.Siguiente!; return actual.Valor; }
    }

    public class ListaInstruccion
    {
        public NodoInstruccion? Cabeza { get; private set; }
        public int Tamaño { get; private set; }
        public void Agregar(Instruccion valor) { NodoInstruccion nuevo = new NodoInstruccion(valor); if (Cabeza == null) Cabeza = nuevo; else { NodoInstruccion actual = Cabeza; while (actual.Siguiente != null) actual = actual.Siguiente; actual.Siguiente = nuevo; } Tamaño++; }
        public Instruccion Obtener(int indice) { NodoInstruccion actual = Cabeza!; for (int i = 0; i < indice; i++) actual = actual.Siguiente!; return actual.Valor; }
    }

    public class ListaMensaje
    {
        public NodoMensaje? Cabeza { get; private set; }
        public int Tamaño { get; private set; }
        public void Agregar(Mensaje valor) { NodoMensaje nuevo = new NodoMensaje(valor); if (Cabeza == null) Cabeza = nuevo; else { NodoMensaje actual = Cabeza; while (actual.Siguiente != null) actual = actual.Siguiente; actual.Siguiente = nuevo; } Tamaño++; }
        public Mensaje Obtener(int indice) { NodoMensaje actual = Cabeza!; for (int i = 0; i < indice; i++) actual = actual.Siguiente!; return actual.Valor; }
    }

    public class ListaEstadoDron
    {
        public NodoEstadoDron? Cabeza { get; private set; }
        public int Tamaño { get; private set; }
        public void Agregar(EstadoDron valor) { NodoEstadoDron nuevo = new NodoEstadoDron(valor); if (Cabeza == null) Cabeza = nuevo; else { NodoEstadoDron actual = Cabeza; while (actual.Siguiente != null) actual = actual.Siguiente; actual.Siguiente = nuevo; } Tamaño++; }
        public EstadoDron Obtener(int indice) { NodoEstadoDron actual = Cabeza!; for (int i = 0; i < indice; i++) actual = actual.Siguiente!; return actual.Valor; }
    }

    public class ListaAccionDron
    {
        public NodoAccionDron? Cabeza { get; private set; }
        public int Tamaño { get; private set; }
        public void Agregar(AccionDron valor) { NodoAccionDron nuevo = new NodoAccionDron(valor); if (Cabeza == null) Cabeza = nuevo; else { NodoAccionDron actual = Cabeza; while (actual.Siguiente != null) actual = actual.Siguiente; actual.Siguiente = nuevo; } Tamaño++; }
        public AccionDron Obtener(int indice) { NodoAccionDron actual = Cabeza!; for (int i = 0; i < indice; i++) actual = actual.Siguiente!; return actual.Valor; }
    }

    public class ListaPasoTiempo
    {
        public NodoPasoTiempo? Cabeza { get; private set; }
        public int Tamaño { get; private set; }
        public void Agregar(PasoTiempo valor) { NodoPasoTiempo nuevo = new NodoPasoTiempo(valor); if (Cabeza == null) Cabeza = nuevo; else { NodoPasoTiempo actual = Cabeza; while (actual.Siguiente != null) actual = actual.Siguiente; actual.Siguiente = nuevo; } Tamaño++; }
        public PasoTiempo Obtener(int indice) { NodoPasoTiempo actual = Cabeza!; for (int i = 0; i < indice; i++) actual = actual.Siguiente!; return actual.Valor; }
    }

    public class ListaResultadoMensaje
    {
        public NodoResultadoMensaje? Cabeza { get; private set; }
        public int Tamaño { get; private set; }
        public void Agregar(ResultadoMensaje valor) { NodoResultadoMensaje nuevo = new NodoResultadoMensaje(valor); if (Cabeza == null) Cabeza = nuevo; else { NodoResultadoMensaje actual = Cabeza; while (actual.Siguiente != null) actual = actual.Siguiente; actual.Siguiente = nuevo; } Tamaño++; }
        public ResultadoMensaje Obtener(int indice) { NodoResultadoMensaje actual = Cabeza!; for (int i = 0; i < indice; i++) actual = actual.Siguiente!; return actual.Valor; }
    }
}