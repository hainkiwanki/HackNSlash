using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECameraMode
{
    NONE = 0,
    PERSPECTIVE = 1,
    ISOMETRIC = 2,
}

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform m_player;

    private Camera m_cam;
    private Transform m_destination = null;
    private Vector3 m_playerPos;
    private float m_offset = 0.0f;

    private void Awake()
    {
        m_cam = Camera.main;
        m_playerPos = m_player.position + Vector3.up * 0.5f;

        var screens = Screen.resolutions;
        var currentScreen = Screen.currentResolution;
        for (int i = 0; i < screens.Length; i++)
        {
            if (screens[i].height == currentScreen.height && screens[i].width == currentScreen.width)
                Debug.Log("This screen resolution is: " + screens[i]);
        }

        // transform.rotation = Quaternion.Euler(70.0f, 45.0f, 0.0f);
        m_cam.orthographic = true;
        transform.rotation = Quaternion.Euler(30.0f, 40.0f, 0.0f);
        m_offset = -50.0f;
        transform.position = m_playerPos + transform.forward * m_offset;
        transform.LookAt(m_player);
    }

    private void Update()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        screenRatio /= (16.0f / 9.0f);
        m_cam.orthographicSize = 7.5f * screenRatio;

        if (m_destination != null)
        {
            // Do something
            Debug.Log("Must be doing something");
        }
        else
        {
            var destination = GetCameraPos(m_player.position);
            transform.position = Vector3.Lerp(transform.position, destination, 1f);
        }
    }

    private Vector3 GetCameraPos(Vector3 _positionToFocus)
    {
        return _positionToFocus + transform.forward * m_offset;
    }

    public void SetDestination(Transform _obj)
    {
        m_destination = _obj;
    }
}
