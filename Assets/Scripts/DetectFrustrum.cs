using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectFrustrum : MonoBehaviour
{

    Camera m_cam;
    Renderer m_renderer;

    // Start is called before the first frame update
    void Start()
    {
        m_cam = Camera.main;
        m_renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_renderer != null)
        {
            var planes = GeometryUtility.CalculateFrustumPlanes(m_cam);
            if (!GeometryUtility.TestPlanesAABB(planes, m_renderer.bounds))
            {
                Debug.Log(gameObject.name + " is not fully in view frustrum");
            }
        }
    }
}
