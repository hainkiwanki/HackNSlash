using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator m_animator;
    [SerializeField] PlayerMotor m_player;

    void Update()
    {
        m_animator.SetFloat("velocity", m_player.m_speed);
    }
}
