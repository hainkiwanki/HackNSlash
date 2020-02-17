using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    public GameObject startMenu;
    public InputField usernameField;

    protected override void _OnAwake()
    {
        base._OnAwake();
    }

    public void ConnectToServer()
    {
        startMenu.SetActive(false);
        usernameField.interactable = false;
        Client.Inst.ConnectToServer();
    }
}
