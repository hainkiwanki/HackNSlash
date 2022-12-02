using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorNetwork : MonoBehaviour
{
    private PlayerManager m_player;
    [SerializeField]
    private Animator m_animator;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_player = GetComponent<PlayerManager>();
    }

    void Update()
    {
        m_animator.SetFloat("velocity", m_player.velocity);
    }
}
