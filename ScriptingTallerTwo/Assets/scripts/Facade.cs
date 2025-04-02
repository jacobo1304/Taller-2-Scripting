using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Clase que representa el motor del carro
public class Motor
{
    private string modeloMotor;

    public Motor(string modelo)
    {
        modeloMotor = modelo;
    }

    public string ObtenerModeloMotor()
    {
        return modeloMotor;
    }
}

// Clase que representa el color del carro
public class ColorCarro
{
    private string color;

    public ColorCarro(string color)
    {
        this.color = color;
    }

    public string ObtenerColor()
    {
        return color;
    }
}

// Clase que representa el modelo del carro
public class ModeloCarro
{
    private string modelo;

    public ModeloCarro(string modelo)
    {
        this.modelo = modelo;
    }

    public string ObtenerModelo()
    {
        return modelo;
    }
}

// Fachada para gestionar el carro
public class FachadaCarro
{
    private Motor motor;
    private ColorCarro color;
    private ModeloCarro modelo;

    public FachadaCarro(string modeloMotor, string color, string modeloCarro)
    {
        motor = new Motor(modeloMotor);
        this.color = new ColorCarro(color);
        modelo = new ModeloCarro(modeloCarro);
    }

    public string ObtenerInformacionCarro()
    {
        return $"Motor: {motor.ObtenerModeloMotor()}\nColor: {color.ObtenerColor()}\nModelo: {modelo.ObtenerModelo()}";
    }
}

// Controlador de la interfaz de usuario
public class Facade : MonoBehaviour
{
    public TextMeshProUGUI textoCarro;
    public Button botonMostrar;
    private FachadaCarro carro;

    void Start()
    {
        // Se crea un carro con sus partes
        carro = new FachadaCarro("V8 Turbo", "Rojo", "Deportivo");
        botonMostrar.onClick.AddListener(MostrarInformacionCarro);
    }

    void MostrarInformacionCarro()
    {
        textoCarro.text = carro.ObtenerInformacionCarro();
    }
}
