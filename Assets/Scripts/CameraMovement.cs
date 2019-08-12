using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform m_player;
    [SerializeField] Vector3 m_offset;

    private void Start()
    {
        transform.position = m_player.position + m_offset;
        transform.LookAt(m_player.transform);
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, m_player.position + m_offset, 0.2f);
    }
}
