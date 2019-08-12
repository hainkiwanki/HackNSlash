using UnityEngine;

public enum MOUSEBUTTON : int
{
    LEFT = 0, RIGHT = 1, MIDDLE = 2
}

public static class InputManager
{
    private static Camera m_cam;

    public static bool IsInitialized = false;
    public static bool ClickedLMB => Input.GetMouseButtonDown((int)MOUSEBUTTON.LEFT);
    public static bool ClickedRMB => Input.GetMouseButtonDown((int)MOUSEBUTTON.RIGHT);
    public static bool HoldingLMB => Input.GetMouseButton((int)MOUSEBUTTON.LEFT);
    public static bool HoldingRMB => Input.GetMouseButton((int)MOUSEBUTTON.RIGHT);

    /// <summary>
    /// Returns the mouse world position
    /// </summary>
    public static Vector3 MouseWP => MouseWorldPosition();

    public static void Init()
    {
        m_cam = Camera.main;
        IsInitialized = true;
    }

    private static Vector3 MouseWorldPosition()
    {
        if (m_cam == null)
            return Vector3.zero;

        Ray ray = m_cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = 1 << 8; // Ground Layer
        if (Physics.Raycast(ray, out hit, 50.0f, layerMask))
        {
            return hit.point;
        }
    
        return Vector3.zero;
    }
}