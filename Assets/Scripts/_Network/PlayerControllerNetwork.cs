using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControllerNetwork : MonoBehaviour
{
    private NavMeshAgent m_navAgent;
    [SerializeField]
    private GameObject m_camera;
    private Vector3 m_clickedMousePos;

    private void Start()
    {
        m_navAgent = GetComponent<NavMeshAgent>();
        m_clickedMousePos = transform.position;
        Instantiate(m_camera, Vector3.zero, Quaternion.identity).GetComponent<CameraController>().SetPlayer(transform);
    }

    private void FixedUpdate()
    {
        SendInputToServer();
    }

    private void SendInputToServer()
    {
        if (Input.GetMouseButton(0))
        {
            m_clickedMousePos = InputManager.MouseWP;

            m_navAgent.SetDestination(InputManager.MouseWP);
        }
        ClientSend.PlayerMovement(m_clickedMousePos);
    }
}
