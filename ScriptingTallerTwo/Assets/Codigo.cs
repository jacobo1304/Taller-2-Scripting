using UnityEngine;
using UnityEngine.UI;
using System;

// Interfaz para el patrón Strategy
public interface IPatternStrategy
{
    void Execute(Text uiText);
}

// 🎨 PATRÓN DECORATOR: Agrega texto adicional a un mensaje base
public class BaseMessage : IPatternStrategy
{
    public virtual void Execute(Text uiText) 
    {
        uiText.text = "Mensaje Base";
        PatternObserver.Notify("Decorator");
    }
}

public class BoldDecorator : BaseMessage
{
    public override void Execute(Text uiText) 
    {
        base.Execute(uiText);
        uiText.text = "<b>" + uiText.text + "</b>";
    }
}

// 🏠 PATRÓN FACADE: Llama varias funciones internas para mostrar el mensaje
public class FacadeMessage : IPatternStrategy
{
    private string FormatMessage() => "Mensaje con Facade";

    public void Execute(Text uiText)
    {
        uiText.text = FormatMessage();
        PatternObserver.Notify("Facade");
    }
}

// ⚡ PATRÓN STRATEGY: Permite cambiar el mensaje dinámicamente
public class StrategyMessage : IPatternStrategy
{
    public void Execute(Text uiText)
    {
        uiText.text = "Mensaje con Strategy";
        PatternObserver.Notify("Strategy");
    }
}

// 🔌 PATRÓN ADAPTER: Adapta una clase externa para que funcione en nuestro sistema
public class ExternalMessageSystem
{
    public string GetExternalMessage() => "Mensaje Adaptado";
}

public class AdapterMessage : IPatternStrategy
{
    private ExternalMessageSystem _externalSystem = new ExternalMessageSystem();

    public void Execute(Text uiText)
    {
        uiText.text = _externalSystem.GetExternalMessage();
        PatternObserver.Notify("Adapter");
    }
}

// 👀 OBSERVER: Notifica qué patrón se usó
public static class PatternObserver
{
    public static Action<string> OnPatternUsed;

    public static void Notify(string pattern)
    {
        OnPatternUsed?.Invoke(pattern);
    }
}

// 🎛️ MANAGER PRINCIPAL: Controla la UI y ejecuta los patrones
public class UIManager : MonoBehaviour
{
    public Text displayText;
    public Text observerText; // Muestra qué patrón se usó

    private IPatternStrategy currentPattern;

    private void Start()
    {
        // Suscribirse al Observer
        PatternObserver.OnPatternUsed += UpdateObserverText;
    }

    public void UseDecorator()
    {
        currentPattern = new BoldDecorator();
        currentPattern.Execute(displayText);
    }

    public void UseFacade()
    {
        currentPattern = new FacadeMessage();
        currentPattern.Execute(displayText);
    }

    public void UseStrategy()
    {
        currentPattern = new StrategyMessage();
        currentPattern.Execute(displayText);
    }

    public void UseAdapter()
    {
        currentPattern = new AdapterMessage();
        currentPattern.Execute(displayText);
    }

    private void UpdateObserverText(string patternUsed)
    {
        observerText.text = "Patrón usado: " + patternUsed;
    }
}
