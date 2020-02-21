using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControllerNetwork : MonoBehaviour
{
    [SerializeField]
    private GameObject m_camera;
    private Vector3 m_clickedMousePos;

    private void Start()
    {
        m_clickedMousePos = transform.position;
        Instantiate(m_camera, Vector3.zero, Quaternion.identity).GetComponent<CameraControllerNetwork>().SetPlayer(transform);
    }

    private void FixedUpdate()
    {
        SendMouseInput();
    }

    private void SendMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            m_clickedMousePos = InputManager.MouseWP;
        }
        ClientSend.PlayerMovement(m_clickedMousePos);
    }
}
