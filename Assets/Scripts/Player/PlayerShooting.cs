using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private Camera m_PlayerCamera = null;
    [SerializeField]
    private bool drawDebugRay = true;
    [SerializeField]
    private Transform gunEnd = null;
    [SerializeField]
    private float flareTimer = 0.55f;

    private LineRenderer m_LineRenderer = null;     //Temporario
    private WaitForSeconds lineTimer;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (drawDebugRay)
            Debug.DrawRay(m_PlayerCamera.transform.position, m_PlayerCamera.transform.forward, Color.blue);

        if(Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ShotEffect());
            
            Vector3 rayOrigin = m_PlayerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hit;

            m_LineRenderer.SetPosition(0, gunEnd.position);

            if (Physics.Raycast(rayOrigin, m_PlayerCamera.transform.forward, out hit, 10))
            {
                m_LineRenderer.SetPosition(1, hit.point);
            }
            else
            {
                m_LineRenderer.SetPosition(1, rayOrigin + (m_PlayerCamera.transform.forward * 10));
            }
        }
    }

    private IEnumerator ShotEffect()
    {
        m_LineRenderer.enabled = true;

        lineTimer = new WaitForSeconds(flareTimer);

        yield return lineTimer;

        m_LineRenderer.enabled = false;
    }
}