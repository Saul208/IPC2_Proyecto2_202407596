using PROYECTO2.Estructuras;
using PROYECTO2.Models;

namespace PROYECTO2.Services
{
    public class Simulador
    {
        public ResultadoMensaje ProcesarMensaje(Mensaje mensaje, SistemaDrones sistema)
        {
            ResultadoMensaje resultado = new ResultadoMensaje
            {
                NombreMensaje = mensaje.Nombre,
                SistemaDrones = sistema.Nombre,
                TiempoOptimo = 0
            };

            ListaDinamica<EstadoDron> estados = new ListaDinamica<EstadoDron>();
            for (int i = 0; i < sistema.Drones.Tamaño; i++)
            {
                estados.Agregar(new EstadoDron
                {
                    Nombre = sistema.Drones.Obtener(i).Nombre,
                    AlturaActual = 0
                });
            }

            int tiempoActual = 1;

            for (int i = 0; i < mensaje.Instrucciones.Tamaño; i++)
            {
                Instruccion instActual = mensaje.Instrucciones.Obtener(i);
                bool instruccionCompletada = false;

                while (!instruccionCompletada)
                {
                    PasoTiempo paso = new PasoTiempo { Tiempo = tiempoActual };

                    for (int j = 0; j < estados.Tamaño; j++)
                    {
                        EstadoDron estado = estados.Obtener(j);
                        AccionDron accion = new AccionDron { NombreDron = estado.Nombre };

                        if (estado.Nombre == instActual.NombreDron)
                        {
                            if (estado.AlturaActual < instActual.ValorAltura)
                            {
                                accion.Accion = "Subir";
                                estado.AlturaActual++;
                            }
                            else if (estado.AlturaActual > instActual.ValorAltura)
                            {
                                accion.Accion = "Bajar";
                                estado.AlturaActual--;
                            }
                            else
                            {
                                accion.Accion = "Emitir luz";
                                instruccionCompletada = true;
                            }
                        }
                        else
                        {
                            int siguienteAltura = BuscarSiguienteAltura(mensaje.Instrucciones, i + 1, estado.Nombre);

                            if (siguienteAltura == -1 || estado.AlturaActual == siguienteAltura)
                            {
                                accion.Accion = "Esperar";
                            }
                            else if (estado.AlturaActual < siguienteAltura)
                            {
                                accion.Accion = "Subir";
                                estado.AlturaActual++;
                            }
                            else if (estado.AlturaActual > siguienteAltura)
                            {
                                accion.Accion = "Bajar";
                                estado.AlturaActual--;
                            }
                        }

                        paso.Acciones.Agregar(accion);
                    }

                    resultado.Pasos.Agregar(paso);
                    tiempoActual++;
                }
            }

            resultado.TiempoOptimo = tiempoActual - 1;
            resultado.MensajeRecibido = DecodificarMensaje(mensaje.Instrucciones, sistema);

            return resultado;
        }

        private int BuscarSiguienteAltura(ListaDinamica<Instruccion> instrucciones, int indexInicio, string nombreDron)
        {
            for (int i = indexInicio; i < instrucciones.Tamaño; i++)
            {
                if (instrucciones.Obtener(i).NombreDron == nombreDron)
                {
                    return instrucciones.Obtener(i).ValorAltura;
                }
            }
            return -1;
        }

        private string DecodificarMensaje(ListaDinamica<Instruccion> instrucciones, SistemaDrones sistema)
        {
            string mensajeFinal = "";

            for (int i = 0; i < instrucciones.Tamaño; i++)
            {
                Instruccion inst = instrucciones.Obtener(i);
                bool letraEncontrada = false;

                for (int j = 0; j < sistema.Alturas.Tamaño; j++)
                {
                    AlturaLetra al = sistema.Alturas.Obtener(j);

                    if (al.Valor == inst.ValorAltura)
                    {
                        mensajeFinal += al.Letra;
                        letraEncontrada = true;
                        break;
                    }
                }

                if (!letraEncontrada)
                {
                    mensajeFinal += " ";
                }
            }

            return mensajeFinal;
        }
    }
}