using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float m_strength;
    private float m_vitality;

    public float STR { get { return m_strength; } set { m_strength = value; } }
    public float VIT { get { return m_vitality; } set { m_vitality = value; } }

    public float Damage()
    {
        return 0.0f;
    }
}
