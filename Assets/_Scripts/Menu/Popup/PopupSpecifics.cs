using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PopupSpecifics
{
    public string title;
    public string description;
    public ButtonsInfo[] buttons;

    public PopupSpecifics(string title, string description, ButtonsInfo[] buttons)
    {
        this.title = title;
        this.description = description;
        this.buttons = buttons;
    }
}

[Serializable]
public class ButtonsInfo
{
    public string buttonText;
    public UnityEvent buttonEvent;
    public Action buttonActionEvent;

    public ButtonsInfo(string buttonText)
    {
        this.buttonText = buttonText;
    }
    
    public ButtonsInfo(string buttonText, UnityEvent buttonEvent)
    {
        this.buttonText = buttonText;
        this.buttonEvent = buttonEvent;
    }
    
    public ButtonsInfo(string buttonText, Action buttonActionEvent)
    {
        this.buttonText = buttonText;
        this.buttonActionEvent = buttonActionEvent;
    }

    public ButtonsInfo(string buttonText, UnityEvent buttonEvent, Action buttonActionEvent)
    {
        this.buttonText = buttonText;
        this.buttonEvent = buttonEvent;
        this.buttonActionEvent = buttonActionEvent;
    }
}