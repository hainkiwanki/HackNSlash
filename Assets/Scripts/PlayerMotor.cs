using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{

    [SerializeField] float m_minRadiusDistance = 1.0f;

    private NavMeshAgent m_agent;
    private Transform m_target;

    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        GetComponent<PlayerAI>().onFocusChangeCallback += OnTargetChanged;
    }

    public void MoveToPosition(Vector3 _position)
    {
        m_agent.destination = _position;
    }

    private void Update()
    {
        if(m_target != null)
        {
            MoveToPosition(m_target.position);
            Rotate();
        }
    }

    private void OnTargetChanged(Interactable _newTarget)
    {
        if(_newTarget != null)
        {
            m_agent.stoppingDistance = _newTarget.Radius * 0.8f;
            m_agent.updateRotation = false;
            m_target = _newTarget.transform;
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
        float dist = Vector3.Distance(transform.position, m_target.position);
        Vector3 dir = m_target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir.NewY(0.0f));
        if (dist <= m_minRadiusDistance)
            transform.rotation = lookRotation;
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.2f);
    }
}
