using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipesManager : MonoBehaviour
{
    public Text textOne;
    public Text textTwo;
    public Text textThree;
    public Text textFour;
    private int [] listOne = new int[10];
    private int[] listTwo = new int[10];
    private int[] listThree = new int[10];
    private int[] listFour = new int[10];

    public bool gameOver;
    public float gameTimer;
    public float maxTime;
    public Text gameTimerText;

    private int currentProblem;

    private int showList;
    // Start is called before the first frame update
    void Start()
    {
        showList = 0;
        //for (int i = 0; i < 10; i++)
        //{
        //listOne.Add(Random.Range(0,10));
        //listTwo.Add(Random.Range(0, 10));
        //listThree.Add(Random.Range(0, 10));
        //listFour.Add(Random.Range(0, 10));
        //}
        generateProblem();
        ShowText();
    }


    void Update()
    {
        if (gameOver == false)
        {
            gameTimer -= Time.deltaTime;



            int seconds = (int)(gameTimer % 60);
            int minutes = (int)(gameTimer / 60) % 60;
            int hours = (int)(gameTimer / 3600) % 24;

            string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
            if (timerString.Equals("00:00"))
            {
                initiateGameOver();
                //gameTimerText.text = "00:00";
            }
            gameTimerText.text = timerString;
        }
    }

    public void generateProblem()
    {

    }

    public void initiateGameOver()
    {
        gameOver = true;
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

    private void ShowText()
    {
        textOne.text = listOne[showList].ToString();
        textTwo.text = listTwo[showList].ToString();
        textThree.text = listThree[showList].ToString();
        textFour.text = listFour[showList].ToString();
    }

    public void NextNumbers()//test function
    {
        showList++;
        ShowText();
    }


}
