using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public float gameTimer;
    public float maxTime;
    public Text gameTimerText;
    public GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.GetComponent<kakuroGameManger>().gameOver == false)
        {
            gameTimer -= Time.deltaTime;



            int seconds = (int)(gameTimer % 60);
            int minutes = (int)(gameTimer / 60) % 60;
            int hours = (int)(gameTimer / 3600) % 24;

            string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
            if(timerString.Equals("00:00"))
            {
                manager.GetComponent<kakuroGameManger>().initiateGameOver();
                //gameTimerText.text = "00:00";
            }
            gameTimerText.text = timerString;
        }
    }

    public void addTime()
    {
        if (gameTimer + 15f > maxTime)
        {
            gameTimer = maxTime;
        }
        else
        {
            gameTimer += 15f;
        }
    }
}
