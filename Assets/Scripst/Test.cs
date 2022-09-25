using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject m_MyObject, m_NewObject;
    Collider m_Collider, m_Collider2;

    void Start()
    {
        if (m_MyObject != null)
            m_Collider = m_MyObject.GetComponent<Collider>();

        if (m_NewObject != null)
            m_Collider2 = m_NewObject.GetComponent<Collider>();
    }

    void Update()
    {
        if (m_Collider.bounds.Intersects(m_Collider2.bounds))
        {
            Debug.Log("Bounds intersecting");
        }
    }
}
