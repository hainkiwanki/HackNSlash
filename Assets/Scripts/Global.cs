using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : Singleton<Global>
{
    protected override void _OnAwake()
    {
        base._OnAwake();

        InputManager.Init();
    }
}
