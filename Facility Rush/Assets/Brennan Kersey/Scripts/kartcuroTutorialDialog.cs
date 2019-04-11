using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kartcuroTutorialDialog : MonoBehaviour
{
    [SerializeField] private tutorialKartcuroManager manager;
    public bool isTimeToTap;
    [SerializeField] private int numberOfTimesClicked;
    
    // Start is called before the first frame update
    void Start()
    {
        numberOfTimesClicked = 0;
        isTimeToTap = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && isTimeToTap)
        {
            numberOfTimesClicked++;
            if(numberOfTimesClicked==1)
            {
                manager.pointToTimer();
            }
            else
            {
                manager.startGameOver();
            }
        }
    }
}
