using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float Radius => m_radius;

    [SerializeField] float m_radius = 0.5f;

    private bool m_isFocussed = false;
    private bool m_hasInteracted = false;
    private Transform m_player;

    private void Update()
    {
        if(m_isFocussed && !m_hasInteracted)
        {
            float dist = Vector3.Distance(m_player.position, transform.position.NewY(0.0f));
            if(dist <= m_radius)
            {
                m_hasInteracted = true;
                Interact();
            }
        }
    }

    protected virtual void Interact()
    {
        Destroy(gameObject);
    }

    public void OnFocus(Transform _playerTransform)
    {
        m_isFocussed = true;
        m_player = _playerTransform;
        m_hasInteracted = false;
    }

    public void OnForget()
    {
        m_isFocussed = false;
        m_player = null;
        m_hasInteracted = false;
    }

    private void OnDrawGizmos()
    {
        if (m_radius > 0.0f)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, m_radius);
        }
    }
}
