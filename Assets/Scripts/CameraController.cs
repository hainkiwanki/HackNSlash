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
    [SerializeField] ECameraMode m_mode;

    private Camera m_cam;
    private Transform m_destination = null;
    private Vector3 m_playerPos;
    private float m_offset = 0.0f;

    private void Awake()
    {
        m_cam = Camera.main;
        m_playerPos = m_player.position + Vector3.up * 0.5f;
        ChangePerspective(m_mode, true);
    }

    private void ChangePerspective(ECameraMode _mode, bool _force = false)
    {
        if (m_mode != _mode || _force)
        {
            m_mode = _mode;
            switch (m_mode)
            {
                case ECameraMode.PERSPECTIVE:
                    m_cam.orthographic = false;
                    transform.rotation = Quaternion.Euler(70.0f, 45.0f, 0.0f);
                    m_offset = -9.0f;
                    break;
                case ECameraMode.ISOMETRIC:
                    m_cam.orthographic = true;
                    transform.rotation = Quaternion.Euler(30.0f, 40.0f, 0.0f);
                    m_offset = -10.0f;
                    break;
            }
            transform.position = m_playerPos + transform.forward * m_offset;
            transform.LookAt(m_player);
        }
    }

    private void Update()
    {
        if(m_destination != null)
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
