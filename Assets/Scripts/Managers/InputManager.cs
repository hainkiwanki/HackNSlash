using UnityEngine;

public enum MOUSEBUTTON : int
{
    LEFT = 0, RIGHT = 1, MIDDLE = 2
}

public static class InputManager
{
    private const int HOLD_FRAMES_TRIGGER_AMOUNT = 50;

    private static Camera m_cam;

    private static uint m_LMBCounter = 0;
    private static uint m_RMBCounter = 0;

    public static bool IsInitialized = false;
    public static bool ClickedLMB => Input.GetMouseButtonDown((int)MOUSEBUTTON.LEFT);
    public static bool ReleasedLMB => Input.GetMouseButtonUp((int)MOUSEBUTTON.LEFT);
    public static bool ClickedRMB => Input.GetMouseButtonDown((int)MOUSEBUTTON.RIGHT);
    public static bool ReleasedRMB => Input.GetMouseButtonUp((int)MOUSEBUTTON.RIGHT);
    public static bool HoldingLMB => m_LMBCounter >= HOLD_FRAMES_TRIGGER_AMOUNT;
    public static bool HoldingRMB => m_RMBCounter >= HOLD_FRAMES_TRIGGER_AMOUNT;
    public static Vector2 DeltaMousePos => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    public static float Zoom => Input.GetAxisRaw("Mouse ScrollWheel");
    public static bool HasOpenedInventory => Input.GetKeyDown(KeyCode.I);

    /// <summary>
    /// Returns the mouse world position
    /// </summary>
    public static Vector3 MouseWP => MouseWorldPosition();

    public static void Update()
    {
        if (Input.GetMouseButton((int)MOUSEBUTTON.LEFT) && !ClickedLMB && !ReleasedLMB)
            m_LMBCounter++;

        if (ReleasedLMB)
            m_LMBCounter = 0;

        if (Input.GetMouseButton((int)MOUSEBUTTON.RIGHT) && !ClickedRMB && !ReleasedRMB)
            m_RMBCounter++;

        if(ReleasedRMB)
            m_RMBCounter = 0;
    }

    public static void Init()
    {
        m_cam = Camera.main;
        IsInitialized = true;
    }

    private static Vector3 MouseWorldPosition(int _layerMask = (1 << 8))
    {
        if (m_cam == null)
        {
            Init();
            return Vector3.zero;
        }

        Ray ray = m_cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 999.0f, _layerMask))
        {
            return hit.point;
        }
    
        return Vector3.zero;
    }

    public static Interactable GetInteractableClickedOn()
    {
        if (m_cam == null)
            return null;

        Ray ray = m_cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 999.0f, (1 << 10)))
        {
            return hit.collider.gameObject.GetComponent<Interactable>();
        }

        return null;
    }
}