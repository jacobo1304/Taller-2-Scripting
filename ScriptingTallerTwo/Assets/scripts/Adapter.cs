using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Interfaz objetivo
public interface IAdaptadorHexadecimal
{
    int Convertir(string numeroHex);
}

// Adaptador que convierte un número hexadecimal a int
public class AdaptadorHexAInt : IAdaptadorHexadecimal
{
    public int Convertir(string numeroHex)
    {
        return int.Parse(numeroHex, System.Globalization.NumberStyles.HexNumber);
    }
}

public class Adapter : MonoBehaviour
{
    public TextMeshProUGUI textoResultado;
    public Button botonConvertir;

    private IAdaptadorHexadecimal adaptador;
    private string numeroHexadecimal = "1A"; // Número hexadecimal de ejemplo (26 en decimal)

    void Start()
    {
        adaptador = new AdaptadorHexAInt();
        botonConvertir.onClick.AddListener(ActualizarTexto);
    }

    void ActualizarTexto()
    {
        int numeroEntero = adaptador.Convertir(numeroHexadecimal);
        textoResultado.text = "Número convertido: " + numeroEntero;
        Observer.Instancia.NotificarUso("Adapter");
    }
}
