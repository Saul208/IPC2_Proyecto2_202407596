using System;

namespace PROYECTO2.Estructuras
{
    public class ListaDinamica<T>
    {
        public Nodo<T>? Cabeza { get; private set; }
        public int Tamaño { get; private set; }

        public ListaDinamica()
        {
            Cabeza = null;
            Tamaño = 0;
        }

        public void Agregar(T valor)
        {
            Nodo<T> nuevoNodo = new Nodo<T>(valor);
            if (Cabeza == null)
            {
                Cabeza = nuevoNodo;
            }
            else
            {
                Nodo<T> actual = Cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevoNodo;
            }
            Tamaño++;
        }

        public T Obtener(int indice)
        {
            if (indice < 0 || indice >= Tamaño)
                throw new IndexOutOfRangeException("Índice fuera de rango.");

            Nodo<T> actual = Cabeza!;
            for (int i = 0; i < indice; i++)
            {
                actual = actual.Siguiente!;
            }
            return actual.Valor;
        }
    }
}