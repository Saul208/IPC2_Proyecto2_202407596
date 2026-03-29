using PROYECTO2.Models;

namespace PROYECTO2.Estructuras
{
    public class NodoDron { public Dron Valor { get; set; } public NodoDron? Siguiente { get; set; } public NodoDron(Dron valor) { Valor = valor; Siguiente = null; } }
    public class NodoAlturaLetra { public AlturaLetra Valor { get; set; } public NodoAlturaLetra? Siguiente { get; set; } public NodoAlturaLetra(AlturaLetra valor) { Valor = valor; Siguiente = null; } }
    public class NodoSistemaDrones { public SistemaDrones Valor { get; set; } public NodoSistemaDrones? Siguiente { get; set; } public NodoSistemaDrones(SistemaDrones valor) { Valor = valor; Siguiente = null; } }
    public class NodoInstruccion { public Instruccion Valor { get; set; } public NodoInstruccion? Siguiente { get; set; } public NodoInstruccion(Instruccion valor) { Valor = valor; Siguiente = null; } }
    public class NodoMensaje { public Mensaje Valor { get; set; } public NodoMensaje? Siguiente { get; set; } public NodoMensaje(Mensaje valor) { Valor = valor; Siguiente = null; } }
    public class NodoEstadoDron { public EstadoDron Valor { get; set; } public NodoEstadoDron? Siguiente { get; set; } public NodoEstadoDron(EstadoDron valor) { Valor = valor; Siguiente = null; } }
    public class NodoAccionDron { public AccionDron Valor { get; set; } public NodoAccionDron? Siguiente { get; set; } public NodoAccionDron(AccionDron valor) { Valor = valor; Siguiente = null; } }
    public class NodoPasoTiempo { public PasoTiempo Valor { get; set; } public NodoPasoTiempo? Siguiente { get; set; } public NodoPasoTiempo(PasoTiempo valor) { Valor = valor; Siguiente = null; } }
    public class NodoResultadoMensaje { public ResultadoMensaje Valor { get; set; } public NodoResultadoMensaje? Siguiente { get; set; } public NodoResultadoMensaje(ResultadoMensaje valor) { Valor = valor; Siguiente = null; } }
}