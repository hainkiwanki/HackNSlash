using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float m_zoomSpeed = 350.0f;
    [SerializeField] float m_minZoom = 7.0f;
    [SerializeField] float m_maxZoom = 11.0f;
    [SerializeField] Transform m_player;
    [SerializeField] Vector3 m_offset;
    
    private Vector3 m_destination;

    private void Start()
    {
        transform.position = m_player.position + m_offset;
        transform.RotateAround(m_player.position, Vector3.up, 45.0f);
        transform.LookAt(m_player.transform.position + Vector3.up * 0.5f);
    }

    private void LateUpdate()
    {
        if(InputManager.Zoom != 0.0f)
        {
            var deltaMove = transform.forward * InputManager.Zoom * Time.deltaTime * m_zoomSpeed;
            var tempPostion = transform.position + deltaMove;

            var dist = Vector3.Distance(tempPostion, m_player.position);
            if (dist > m_minZoom && dist < m_maxZoom)
            {
                m_offset += deltaMove;
            }
        }
        var dest = m_player.position + m_offset;
        transform.position = Vector3.Lerp(transform.position, dest, 0.2f);
    }

    public void SetDestination(Vector3 _position)
    {
        m_destination = _position;
    }
}
