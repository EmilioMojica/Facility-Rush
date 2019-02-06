using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DegradationManager : MonoBehaviour
{
    [SerializeField] int aAttempted;
    [SerializeField] int aCorrect;

    public float aFill;
    public float maxFill = 15f;

    private bool KartCuro = false, assLine = false, Chutes = false, pipGyro = false;

    // Start is called before the first frame update
    void Start()
    {
        //Get gamemode that was played

        //Get the amount of questions answered in a session
        //Get the amount of quetions answered correctly in a session
    }

    // Update is called once per frame
    void Update()
    {
        if (KartCuro == true)
        {
            if(aAttempted < 5)
            {
                aFill = (aCorrect / aAttempted) / 5;
            }
            else
            {
                aFill = aCorrect / aAttempted;
            }
        }

        if(assLine == true)
        {
            if (aAttempted < 10)
            {
                aFill = (aCorrect / aAttempted) / 10;
            }
            else
            {
                aFill = aCorrect / aAttempted;
            }
        }

        if (Chutes)
        {
            if (aAttempted < 10)
            {
                aFill = (aCorrect / aAttempted) / 10;
            }
            else
            {
                aFill = aCorrect / aAttempted;
            }
        }

        if (pipGyro)
        {

        }


    }
}
