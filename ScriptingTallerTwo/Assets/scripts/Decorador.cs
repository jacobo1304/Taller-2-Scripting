using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Clase base del decorador
public abstract class DecoradorMensaje
{
    protected string mensaje;

    public DecoradorMensaje(string mensajeBase)
    {
        this.mensaje = mensajeBase;
    }

    public abstract string ObtenerMensaje();
}

// Decorador de Negrita
public class DecoradorNegrita : DecoradorMensaje
{
    public DecoradorNegrita(string mensajeBase) : base(mensajeBase) {}

    public override string ObtenerMensaje()
    {
        return "<b>" + mensaje + "</b>";
    }
}

// Decorador de Itálica
public class DecoradorItalica : DecoradorMensaje
{
    public DecoradorItalica(string mensajeBase) : base(mensajeBase) {}

    public override string ObtenerMensaje()
    {
        return "<i>" + mensaje + "</i>";
    }
}

public class Decorador : MonoBehaviour
{
    public TextMeshProUGUI textoMostrar;
    public Button botonNegrita;
    public Button botonItalica;

    private string mensajeBase = "Hola, esto es un mensaje.";

    void Start()
    {
        botonNegrita.onClick.AddListener(DecorarNegrita);
        botonItalica.onClick.AddListener(DecorarItalica);
        textoMostrar.text = mensajeBase;
    }

    void DecorarNegrita()
    {
        DecoradorMensaje mensajeDecorado = new DecoradorNegrita(mensajeBase);
        textoMostrar.text = mensajeDecorado.ObtenerMensaje();
        Observer.Instancia.NotificarUso("Decorador");
    }

    void DecorarItalica()
    {
        DecoradorMensaje mensajeDecorado = new DecoradorItalica(mensajeBase);
        textoMostrar.text = mensajeDecorado.ObtenerMensaje();
        Observer.Instancia.NotificarUso("Decorador");
    }
}