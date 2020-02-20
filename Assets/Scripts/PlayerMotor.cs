using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    public float m_speed => m_agent.velocity.sqrMagnitude;
    private NavMeshAgent m_agent;
    private Interactable m_target;

    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        GetComponent<PlayerController>().onFocusChangeCallback += OnTargetChanged;
    }

    public void MoveToPosition(Vector3 _position)
    {
        m_agent.destination = _position;
    }

    private void Update()
    {
        if(m_target != null)
        {
            MoveToPosition(m_target.transform.position);
            Rotate();
        }
    }

    private void OnTargetChanged(Interactable _newTarget)
    {
        if(_newTarget != null)
        {
            m_agent.stoppingDistance = _newTarget.Radius * 0.8f;
            m_agent.updateRotation = false;
            m_target = _newTarget;
        }
        else
        {
            m_agent.stoppingDistance = 0.0f;
            m_agent.updateRotation = true;
            m_target = null;
        }
    }

    private void Rotate()
    {
        float dist = Vector3.Distance(transform.position, m_target.transform.position);
        Vector3 dir = m_target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir.NewY(0.0f));
        if (dist <= m_target.Radius)
            transform.rotation = lookRotation;
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.2f);
    }
}
