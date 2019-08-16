using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : Singleton<Global>
{
    private Resolution m_screenResolution;
    private Resolution m_windowResolution;

    protected override void _OnAwake()
    {
        base._OnAwake();

        InputManager.Init();
    }

    private void Update()
    {
        InputManager.Update();
    }
}
