using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator m_animator;
    [SerializeField] PlayerAI m_player;

    void Update()
    {
        m_animator.SetFloat("Speed", m_player.Speed);
    }
}
