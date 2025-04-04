using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    // Observador concreto
    class Observador
    {
        private string nombre;

        public Observador(string nombre)
        {
            this.nombre = nombre;
        }

        public void Actualizar(string nombreDelPatron)
        {
            Debug.Log($"{nombre}: se usó el patrón {nombreDelPatron}");
        }
    }

    // Sujeto que notifica a los observadores
    class Notificador
    {
        private List<Observador> observadores = new List<Observador>();

        public void AgregarObservador(Observador observador)
        {
            observadores.Add(observador);
        }

        public void Notificar(string nombreDelPatron)
        {
            foreach (var observador in observadores)
            {
                observador.Actualizar(nombreDelPatron);
            }
        }
    }

    // Singleton accesible desde otros scripts
    public static Observer Instancia;

    private Notificador notificador;

    void Awake()
    {
        Instancia = this;
        notificador = new Notificador();

        notificador.AgregarObservador(new Observador("Sujeto 1"));
        notificador.AgregarObservador(new Observador("Sujeto 2"));
        notificador.AgregarObservador(new Observador("Sujeto 3"));
    }

    public void NotificarUso(string nombreDelPatron)
    {
        notificador.Notificar(nombreDelPatron);
    }
}
