using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Renderer m_rend;

    Color m_originalColor;

    void Start()
    {
        m_originalColor = m_rend.material.color;
    }

    public void SelectEnemy()
    {
        m_rend.material.color = Color.green;
    }

    public void ResetEnemy()
    {
        m_rend.material.color = m_originalColor;
    }
}
