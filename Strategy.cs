using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Interfaz para definir el comportamiento del medio de transporte
public interface IMedioTransporte
{
    string ObtenerDescripcion();
}

// Estrategias concretas
public class Coche : IMedioTransporte
{
    public string ObtenerDescripcion() => "Transporte: Coche - Rápido y cómodo";
}

public class Bicicleta : IMedioTransporte
{
    public string ObtenerDescripcion() => "Transporte: Bicicleta - Ecológico y saludable";
}

public class Avion : IMedioTransporte
{
    public string ObtenerDescripcion() => "Transporte: Avión - Ideal para largas distancias";
}

// Contexto que usa una estrategia
public class Strategy : MonoBehaviour
{
    private IMedioTransporte medioActual;
    public TextMeshProUGUI textoTransporte;
    public Button botonCambiar;

    private void Start()
    {
        medioActual = new Coche(); // Medio inicial
        MostrarTransporte();
        botonCambiar.onClick.AddListener(CambiarTransporte);
    }

    private void MostrarTransporte()
    {
        textoTransporte.text = medioActual.ObtenerDescripcion();
    }

    private void CambiarTransporte()
    {
        if (medioActual is Coche)
            medioActual = new Bicicleta();
        else if (medioActual is Bicicleta)
            medioActual = new Avion();
        else
            medioActual = new Coche();


        MostrarTransporte();
        Observer.Instancia.NotificarUso("Strategy");
    }
}
