using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorNetwork : MonoBehaviour
{
    private PlayerControllerNetwork m_player;
    private Animator m_animator;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_player = GetComponent<PlayerControllerNetwork>();
    }

    void Update()
    {
        m_animator.SetFloat("velocity", m_player.Speed);
    }
}
