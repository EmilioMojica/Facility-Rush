using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MF_Credits : MonoBehaviour
{
    public GameObject credits;
    public GameObject options;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnClickCredits()
    {
        //if (credits.activeInHierarchy == false)
        //{
        //    credits.SetActive(true);
        //}
        options.SetActive(false);
        anim.SetTrigger("OpenCredits");
    }

    public void OnClickCloseCredits()
    {
        //if (credits.activeInHierarchy == true)
        //{
        //    credits.SetActive(false);
        //}
        anim.SetTrigger("CloseCredits");
    }
}
