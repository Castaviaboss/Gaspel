using System;
using TMPro;
using UnityEngine;

public class BaseUiObject : MonoBehaviour
{
    public LobbyController lobby;
    public TMP_Text messageText;

    protected virtual void Awake()
    {
        InitializeMenu();
    }

    public virtual void CallMessage(string text)
    {
        messageText.text = text;
    }

    public virtual void AddMessage(string text)
    {
        messageText.text += "\n" + text;
    }

    public virtual void ClearMessage()
    {
        messageText.text = "";
    }

    public virtual void InitializeMenu() { }
}
