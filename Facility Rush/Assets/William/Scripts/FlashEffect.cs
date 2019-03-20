using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffect : MonoBehaviour
{
    public Animator anim;
    public AssemblyTutorial AT;
    public RectTransform myRecTransform;
    public RectTransform[] flasingPos;

    // Start is called before the first frame update
    void Start()
    {
        myRecTransform = GetComponent<RectTransform>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AT.index == 1)
        {
            myRecTransform.localPosition = flasingPos[0].localPosition;
            anim.SetBool("IsFlashing", true);

        }

        if (AT.index == 2)
        {
            myRecTransform.localPosition = flasingPos[1].localPosition;
        }

        if (AT.index == 3)
        {
            myRecTransform.localPosition = flasingPos[2].localPosition;
        }

        if (AT.index == 4)
        {
            myRecTransform.localPosition = flasingPos[3].localPosition;
        }

        if (AT.index == 5)
        {
            myRecTransform.localPosition = new Vector3(500, 100, 0);
            anim.SetBool("IsFlashing", false);

        }
    }
}
