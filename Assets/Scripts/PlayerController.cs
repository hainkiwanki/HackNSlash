using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public float Speed => 0.0f;

    [SerializeField] Interactable m_marker;
    [SerializeField] GameObject m_inventory;

    private PlayerMotor m_playerMotor;
    private float m_ignoreRadius = 0.3f;
    private Interactable m_visualTarget;
    private Interactable m_target;

    public delegate void OnFocusChanged(Interactable _newTarget);
    public OnFocusChanged onFocusChangeCallback;

    void Awake()
    {
        m_playerMotor = GetComponent<PlayerMotor>();
        m_visualTarget = Instantiate(m_marker, transform.position, Quaternion.identity);
    }

    void Update()
    {
        UpdateLMB();

        if (InputManager.ClickedRMB)
        {
            // Right-click
        }

        if (InputManager.HasOpenedInventory)
            if (m_inventory != null)
                m_inventory.SetActive(!m_inventory.activeSelf);
    }

    void UpdateLMB()
    {
        if (InputManager.ClickedLMB)
        {
            var mouseWP = InputManager.MouseWP;
            var interactable = InputManager.GetInteractableClickedOn();
            if (interactable != null)
            {
                interactable.OnFocus(transform);
                SetTargetFocus(interactable);
            }
            else
            {
                if (mouseWP != Vector3.zero)
                {
                    m_playerMotor.MoveToPosition(mouseWP);
                    m_visualTarget.transform.position = mouseWP;
                }

                m_playerMotor.MoveToPosition(m_visualTarget.transform.position);
            }
        }

        if (InputManager.HoldingLMB)
        {
            var mouseWP = InputManager.MouseWP;
            if (mouseWP != Vector3.zero)
            {
                var distance = Vector3.Distance(transform.position.NewY(0.0f), mouseWP);
                if (distance > m_ignoreRadius)
                    m_visualTarget.transform.position = mouseWP;
                else
                    m_visualTarget.transform.position = transform.position + (transform.forward * m_ignoreRadius * 2.0f);
            }

            SetTargetFocus(m_visualTarget);
        }

        if (InputManager.ReleasedLMB && m_target == null)
        {
            SetTargetFocus(null);
        }
    }

    private void SetTargetFocus(Interactable _newTarget)
    {
        if (onFocusChangeCallback != null)
            onFocusChangeCallback.Invoke(_newTarget);

        // Forget about previous target before setting new one
        if(m_target != _newTarget && m_target != null)
        {
            m_target.OnForget();
        }

        m_target = _newTarget;

        if(m_target != null)
        {
            m_target.OnFocus(transform);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_ignoreRadius);
    }
}
