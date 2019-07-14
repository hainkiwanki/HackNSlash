using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHolder : MonoBehaviour
{
    public List<Enemy> m_enemies = new List<Enemy>();

    public void ResetEnemies()
    {
        foreach(var enemy in m_enemies)
        {
            enemy.ResetEnemy();
        }
    }
}
