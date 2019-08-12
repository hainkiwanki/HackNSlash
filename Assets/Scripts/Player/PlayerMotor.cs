using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    private NavMeshAgent m_agent;
    private Transform m_target;

    void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToPosition(Vector3 _position)
    {
        m_agent.destination = _position;
    }

    void Update()
    {
        if(m_target != null)
        {
            MoveToPosition(m_target.position);
        }
    }

    public void SetTarget(Transform _transform)
    {
        m_target = _transform;
    }

    public void RemoveTarget()
    {
        m_target = null;
    }

}
