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
``` C#
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

// Decoradores concretos
public class MilkDecorator : CoffeeDecorator
{
    public MilkDecorator(ICoffee coffee) : base(coffee) { }
    public override string GetDescription() => _coffee.GetDescription() + ", con leche";
    public override double GetCost() => _coffee.GetCost() + 1.5;
}

public class SugarDecorator : CoffeeDecorator
{
    public SugarDecorator(ICoffee coffee) : base(coffee) { }
    public override string GetDescription() => _coffee.GetDescription() + ", con azúcar";
    public override double GetCost() => _coffee.GetCost() + 0.5;
}

// Clase para el patrón Facade
public class CoffeeOrderFacade
{
    public void OrderCoffee()
    {
        ICoffee coffee = new SimpleCoffee();
        coffee = new MilkDecorator(coffee);
        coffee = new SugarDecorator(coffee);
        Console.WriteLine($"Pedido: {coffee.GetDescription()} - Precio: ${coffee.GetCost()}");
    }
}

// Programa principal
class Program
{
    static void Main()
    {
        CoffeeOrderFacade facade = new CoffeeOrderFacade();
        facade.OrderCoffee();
    }
}
```
## ejemplos de antipatron

### ejemplo 1 - godObject (una clase que maneja demasiadas tareas diferentes)
``` C#
using System;
using System.Collections.Generic;

class GodObject
{
    // Lista de productos disponibles en la tienda
    private List<string> Products = new List<string>();

    // Lista de clientes registrados
    private List<string> Customers = new List<string>();

    // Método para agregar productos a la tienda
    public void AddProduct(string product)
    {
        Products.Add(product);
        Console.WriteLine($"Producto agregado: {product}");
    }

    // Método para mostrar productos disponibles
    public void ShowProducts()
    {
        Console.WriteLine("Productos en la tienda:");
        Products.ForEach(Console.WriteLine);
    }

    // Método para registrar clientes
    public void RegisterCustomer(string customer)
    {
        Customers.Add(customer);
        Console.WriteLine($"Cliente registrado: {customer}");
    }

    // Método para mostrar clientes registrados
    public void ShowCustomers()
    {
        Console.WriteLine("Clientes registrados:");
        Customers.ForEach(Console.WriteLine);
    }

    // Método para procesar una compra
    public void ProcessPurchase(string customer, string product)
    {
        if (Customers.Contains(customer) && Products.Contains(product))
        {
            Console.WriteLine($"Compra realizada: {customer} compró {product}");
        }
        else
        {
            Console.WriteLine("Error: Cliente no registrado o producto no disponible.");
        }
    }

    // Método para simular el envío del producto
    public void ShipOrder(string customer, string product)
    {
        Console.WriteLine($"Enviando {product} a {customer}...");
    }
}

class Program
{
    static void Main()
    {
        GodObject store = new GodObject();

        // Agregar productos
        store.AddProduct("Laptop");
        store.AddProduct("Teléfono");

        // Registrar clientes
        store.RegisterCustomer("Carlos");
        store.RegisterCustomer("María");

        // Mostrar productos y clientes
        store.ShowProducts();
        store.ShowCustomers();

        // Procesar una compra
        store.ProcessPurchase("Carlos", "Laptop");

        // Enviar el producto
        store.ShipOrder("Carlos", "Laptop");
    }
}
```
### ejemplo 2 - codigo espagueti (codigo mal estrucutrado, dificil de leer y entender)
```
using System;
using System.Collections.Generic;

class Program
{
    static List<string> clientes = new List<string>();
    static List<string> reservas = new List<string>();
    
    static void Main()
    {
        Console.WriteLine("Bienvenido al sistema de reservas");

        while (true)
        {
            Console.WriteLine("1. Agregar cliente");
            Console.WriteLine("2. Hacer reserva");
            Console.WriteLine("3. Ver reservas");
            Console.WriteLine("4. Salir");

            string opcion = Console.ReadLine();

            if (opcion == "1")
            {
                Console.WriteLine("Ingrese nombre del cliente:");
                string nombre = Console.ReadLine();
                clientes.Add(nombre);
                Console.WriteLine("Cliente agregado.");
            }
            else if (opcion == "2")
            {
                Console.WriteLine("Ingrese nombre del cliente para la reserva:");
                string cliente = Console.ReadLine();
                bool encontrado = false;
                for (int i = 0; i < clientes.Count; i++)
                {
                    if (clientes[i] == cliente)
                    {
                        encontrado = true;
                        break;
                    }
                }
                if (encontrado)
                {
                    Console.WriteLine("Ingrese la fecha de la reserva:");
                    string fecha = Console.ReadLine();
                    reservas.Add(cliente + " - " + fecha);
                    Console.WriteLine("Reserva realizada.");
                }
                else
                {
                    Console.WriteLine("Cliente no registrado.");
                }
            }
            else if (opcion == "3")
            {
                Console.WriteLine("Reservas:");
                for (int i = 0; i < reservas.Count; i++)
                {
                    Console.WriteLine(reservas[i]);
                }
            }
            else if (opcion == "4")
            {
                break;
            }
            else
            {
                Console.WriteLine("Opción inválida.");
            }
        }
    }
}
```

