# Taller-2-Scripting
## Preguntas teóricas
[preguntas teoricas con repsuesta](https://docs.google.com/document/d/1EWxG7sqi7-Ndyjf9g3W44JX9ZXT4TCCKOv9kYSJyqRU/edit?usp=sharing)

### Ejemplo Práctico Singleton
``` C#
using System;
namespace Taller2EjercicioPractico1;
public class Personaje
{
    // Instancia única del personaje
    private static Personaje _instancia;

    // Propiedades del personaje
    public string Nombre { get; set; }
    public int Salud { get; set; }
    public int Nivel { get; set; }

    // Constructor privado para evitar que se cree una instancia desde fuera
    private Personaje()
    {
        Nombre = "Héroe";
        Salud = 100;
        Nivel = 1;
    }

    // Método estático para obtener la instancia única del personaje
    public static Personaje ObtenerInstancia()
    {
        // Si no existe la instancia, la crea
        if (_instancia == null)
        {
            _instancia = new Personaje();
        }
        return _instancia;
    }

    // Método para mostrar el estado del personaje
    public void MostrarEstado()
    {
        Console.WriteLine($"Nombre: {Nombre}, Salud: {Salud}, Nivel: {Nivel}");
    }

    // Método para aumentar el nivel del personaje
    public void SubirNivel()
    {
        Nivel++;
        Salud = 100; // Al subir de nivel, se restaura la salud
    }

    // Método para recibir daño
    public void RecibirDanio(int dano)
    {
        Salud -= dano;
        if (Salud < 0) Salud = 0;
    }
}
```
### Y aquí está la clase program, donde se hacen ciertas comprobaciones por consola 
```C#
using System;
namespace Taller2EjercicioPractico1;

class Program
{
    static void Main()
    {
        // Obtener la única instancia del personaje
        Personaje personaje1 = Personaje.ObtenerInstancia();

        // Mostrar el estado inicial del personaje
        Console.WriteLine("Estado inicial:");
        personaje1.MostrarEstado();

        // Simulamos que el personaje sube de nivel
        personaje1.SubirNivel();
        Console.WriteLine("\nDespués de subir de nivel:");
        personaje1.MostrarEstado();

        // Simulamos que el personaje recibe daño
        personaje1.RecibirDanio(40);
        Console.WriteLine("\nDespués de recibir daño:");
        personaje1.MostrarEstado();

        // Intentamos obtener una nueva instancia y comprobar si es la misma
        Personaje personaje2 = Personaje.ObtenerInstancia();
        Console.WriteLine("\nVerificando si ambas instancias son la misma:");
        Console.WriteLine(personaje1 == personaje2 ? "Ambas instancias son la misma." : "Las instancias son diferentes.");
    }
}
```

### Ejemplo Práctico Observer
``` C#
namespace NombreDelProyecto;

class Program
{
    static void Main(string[] args)
    {
        Faro faroPrincipal = new Faro();
        Barco barco1 = new Barco("BarcoA", 1);
        Barco barco2 = new Barco("BarcoB", 2);
        Barco barco3 = new Barco("BarcoC", 3);

        faroPrincipal.barco1 = barco1;
        faroPrincipal.barco2 = barco2;
        faroPrincipal.barco3 = barco3;

        Console.WriteLine("Presiona cualquier tecla para activar el faro y notificar a los barcos. Presiona 'q' para salir.");

         while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Q)
                {
                    break;
                }

                faroPrincipal.NotifyObservers();
            }
    }
}

class Faro{
    public Barco barco1;
    public Barco barco2;
    public Barco barco3;

    public void NotifyObservers()
    {
        barco1?.Update();
        barco2?.Update();
        barco3?.Update();
    }

}

class Barco {
    private string nombre;
    private int num;

    public Barco(string nombre, int num)
    {
        this.nombre = nombre;
        this.num = num;
    }

     public void Update()
     {
        Console.WriteLine($"{nombre} ha recibido la señal del faro.");
    }
}

```

### Ejemplo observer con delegados

```C#
using System;

namespace NombreDelProyecto
{
    
    delegate void NotifyDelegate();

    class Program
    {
        static void Main(string[] args)
        {
            Faro faroPrincipal = new Faro();
            Barco barco1 = new Barco("BarcoA");
            Barco barco2 = new Barco("BarcoB");
            Barco barco3 = new Barco("BarcoC");

           
            faroPrincipal.OnNotify += barco1.Update;
            faroPrincipal.OnNotify += barco2.Update;
            faroPrincipal.OnNotify += barco3.Update;

            Console.WriteLine("Presiona cualquier tecla para activar el faro y notificar a los barcos. Presiona 'q' para salir.");

            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Q)
                {
                    break;
                }

                faroPrincipal.NotifyObservers();
            }
        }
    }

    class Faro
    {
        
        public event NotifyDelegate OnNotify;

       
        public void NotifyObservers()
        {
            OnNotify?.Invoke();  
        }
    }

    class Barco
    {
        private string nombre;

        public Barco(string nombre)
        {
            this.nombre = nombre;
        }

      
        public void Update()
        {
            Console.WriteLine($"{nombre} ha recibido la señal del faro.");
        }
    }
}

```
### ejemplo patron decorador y facade
```
using System;

// Interfaz base para el patrón Decorador
public interface ICoffee
{
    string GetDescription();
    double GetCost();
}

// Clase concreta
public class SimpleCoffee : ICoffee
{
    public string GetDescription() => "Café simple";
    public double GetCost() => 5.0;
}

// Decorador base
public class CoffeeDecorator : ICoffee
{
    protected ICoffee _coffee;
    public CoffeeDecorator(ICoffee coffee) { _coffee = coffee; }
    public virtual string GetDescription() => _coffee.GetDescription();
    public virtual double GetCost() => _coffee.GetCost();
}
```
