using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerNetwork : MonoBehaviour
{
    [SerializeField] Transform m_player;

    private Camera m_cam;
    private Vector3 m_playerPos;
    private float m_offset = 0.0f;

    private void Awake()
    {
        m_cam = Camera.main;
        m_cam.orthographic = true;
    }

    private void Update()
    {
        if(m_cam == null)
        {
            m_cam = Camera.main;
        }
        m_cam.orthographicSize = 7.5f * Global.Inst.ScreenMod;

        if (m_player != null)
        {
            var destination = GetCameraPos(m_player.position);
            transform.position = Vector3.Lerp(transform.position, destination, 1f);
        }
    }

    private Vector3 GetCameraPos(Vector3 _positionToFocus)
    {
        return _positionToFocus + transform.forward * m_offset;
    }

    public void SetPlayer(Transform _player)
    {
        m_offset = -50.0f;
        m_player = _player;
        m_playerPos = m_player.position + Vector3.up * 0.5f;
        transform.rotation = Quaternion.Euler(30.0f, 40.0f, 0.0f);
        transform.position = m_playerPos + transform.forward * m_offset;
        transform.LookAt(m_player);
    }
}
