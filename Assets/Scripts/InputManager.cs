using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public enum MOUSEBUTTON : int
    {
        LEFT = 0,
        RIGHT = 1,
        MIDDLE = 2
    }

    private Camera m_camera;
    // Mouse buttonss
    private bool m_isLeftClick = false;
    private bool m_isRightClick = false;
    private bool m_isHoldingLeftClick = false;
    private float m_deltaYmouse = 0.0f;
    private float m_deltaXmouse = 0.0f;

    //-----------------------------------------------------------------------------------------------
    // Accessibles
    public bool LeftClick => m_isLeftClick;
    public bool RightClick => m_isRightClick;
    public bool HoldLeftClick => m_isHoldingLeftClick && !m_isLeftClick;
    public Vector3 MouseWorldPos => MouseWorldPosition();
    public Vector2 DeltaMousePos => new Vector2(m_deltaXmouse, m_deltaYmouse);

    //--
    private void Start()
    {
        m_camera = Camera.main;
    }
    //-----------------------------------------------------------------------------------------------
    private void Update()
    {
        m_isLeftClick = Input.GetMouseButtonDown((int)MOUSEBUTTON.LEFT);
        m_isRightClick = Input.GetMouseButtonDown((int)MOUSEBUTTON.RIGHT);
        m_isHoldingLeftClick = Input.GetMouseButton((int)MOUSEBUTTON.LEFT);
        m_deltaYmouse = Input.GetAxis("Mouse Y");
        m_deltaXmouse = Input.GetAxis("Mouse X");
    }

    private float m_rayDistance = 50.0f;

    private Vector3 MouseWorldPosition()
    {
        Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = 1 << 8; // Ground Layer
        if (Physics.Raycast(ray, out hit, m_rayDistance, layerMask))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
}