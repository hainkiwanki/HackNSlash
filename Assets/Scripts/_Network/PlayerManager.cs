using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public float velocity;

    public float updateTime = 1.0f / 30.0f;
    private float updateInterval = 1.0f / 30.0f;

    public List<Vector3> snapShotPositions;

    private Vector3 m_currPos;
    private Vector3 m_nextPos;

    private void Awake()
    {
        snapShotPositions = new List<Vector3>();
        m_currPos = transform.position;
        snapShotPositions.Add(m_currPos);
        m_nextPos = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, m_nextPos, 0.5f);
        updateTime -= Time.deltaTime;
        if(updateTime <= 0.0f)
        {
            updateTime = updateInterval;
            if (snapShotPositions.Count > 0)
            {
                m_nextPos = snapShotPositions[0];
                snapShotPositions.RemoveAt(0);
            }
        }
        /*if (transform.position != m_nextPos)
        {
            updateTime -= Time.deltaTime;
            transform.position = Vector3.Lerp(m_currPos, m_nextPos, (updateInterval - updateTime) / updateInterval);
        }
        if (updateTime <= 0.0f) // By now, client must have received something from server (unless packet is lost)
        {
            updateTime = updateInterval;
            m_currPos = transform.position;
            if (snapShotPositions.Count > 1)
            {
                m_nextPos = snapShotPositions[1];
                snapShotPositions.Remove(m_nextPos);
            }
        }*/

    }
}
