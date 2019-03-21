using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseParticle : MonoBehaviour
{
    public ParticleSystem gS;

    private void Start()
    {
        gS.Stop();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            gS.emissionRate = 150.0f;
        }

        else
        {
            gS.emissionRate = 0f;
        }
    }
}
