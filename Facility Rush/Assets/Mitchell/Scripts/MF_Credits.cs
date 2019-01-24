using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MF_Credits : MonoBehaviour
{
    public GameObject credits;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClickCredits()
    {
        if (credits.activeInHierarchy == false)
        {
            credits.SetActive(true);
        }
    }

    public void OnClickCloseCredits()
    {
        if (credits.activeInHierarchy == true)
        {
            credits.SetActive(false);
        }
    }
}
