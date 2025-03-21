# Taller-2-Scripting

[preguntas teoricas con repsuesta](https://docs.google.com/document/d/1EWxG7sqi7-Ndyjf9g3W44JX9ZXT4TCCKOv9kYSJyqRU/edit?usp=sharing)

### Ejemplo Observer
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
