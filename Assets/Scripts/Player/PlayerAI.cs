using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerAI : MonoBehaviour
{
    private PlayerMotor m_playerMotor;


    public float Speed => 0.0f;

    void Awake()
    {
        m_playerMotor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        if(InputManager.ClickedLMB)
        {
            // Left-click
            m_playerMotor.MoveToPosition(InputManager.MouseWP.NewY(0.0f));
        }

        if(InputManager.ClickedRMB)
        {
            // Right-click
        }
    }

    //private void Update()
    //{
    //    m_distanceToMarker = Vector3.Distance(transform.position, m_marker.transform.position);

    //    if (InputManager.Instance.LeftClick || InputManager.Instance.HoldLeftClick)
    //    {
    //        var distanceToMouse = Vector3.Distance(transform.position.NewY(0.0f), InputManager.Instance.MouseWorldPos.NewY(0.0f));
    //        if(distanceToMouse > 0.5f)
    //        {
    //            m_marker.transform.position = InputManager.Instance.MouseWorldPos;
    //        }
        
    //        if(InputManager.Instance.HoldLeftClick && distanceToMouse <= 0.5f)
    //        {
    //            m_marker.transform.position = OffsetRaycastedPos(RayCastTowardsPlayer(InputManager.Instance.MouseWorldPos));
    //            if (distanceToMouse < 0.3f)
    //            { 
    //                m_marker.transform.position = transform.position + m_lastDirection;
    //            }
    //        }
    //    }
    //}

    //void LateUpdate()
    //{
    //    m_lastDirection = m_marker.transform.position - transform.position;
    //    if (m_playerAIagent.enabled)
    //    {
    //        if (m_distanceToMarker >= m_playerAIagent.stoppingDistance)
    //        {
    //            m_movementSpeed = 4.0f;
    //            m_playerAIagent.destination = m_marker.transform.position;
    //        }
    //        else
    //            m_movementSpeed = 0.0f;

    //        if (m_playerAIagent.enabled)
    //            m_playerAIagent.speed = m_movementSpeed;
    //    }
    //}

    //public void SetFocus(Transform _newFocus)
    //{
    //    m_marker.transform.position = _newFocus.position;
    //}

    //private Vector3 OffsetRaycastedPos(Vector3 _pos)
    //{
    //    var dir = _pos - transform.position;
    //    return (transform.position + dir.normalized).NewY(0.0f);
    //}

    //private Vector3 RayCastTowardsPlayer(Vector3 _fromPos)
    //{
    //    var dir = transform.position.NewY(0.0f) - _fromPos.NewY(0.0f);
    //    Ray ray = new Ray(_fromPos, dir);
    //    RaycastHit hitInfo;
    //    if(Physics.Raycast(ray, out hitInfo, 50.0f, 1 << 9) && InputManager.Instance.DeltaMousePos != Vector2.zero)
    //    {
    //        return hitInfo.point;
    //    }
    //    return m_marker.transform.position;
    //}
}
