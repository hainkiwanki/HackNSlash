using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : Singleton<Global>
{
    private Resolution m_screenResolution;
    private Resolution m_windowResolution;

    public float ScreenMod => (float)m_windowResolution.width / (float)m_screenResolution.width;

    protected override void _OnAwake()
    {
        base._OnAwake();

        // Screen resolution (hardware)
        m_screenResolution = Screen.currentResolution;
        // Window resolution (software)
        m_windowResolution.height = Screen.height;
        m_windowResolution.width = Screen.width;

        InputManager.Init();
    }

    private void Update()
    {
        InputManager.Update();
    }
}
