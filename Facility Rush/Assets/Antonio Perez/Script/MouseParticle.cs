using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseParticle : MonoBehaviour
{
    private int distance = 8;

    private void Start()
    {
        //GetComponent<ParticleSystem>().Stop();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            transform.position = r.GetPoint(distance);
        }

        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<ParticleSystem>().Play();
        }
    }
}