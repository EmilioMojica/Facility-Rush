using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Scripts Goals: Generation of correct board based on level selection, perform end of level when game is finished, keep track of player score, and pass data back to general manager
 */

public class kakuroGameManger : MonoBehaviour
{
    public int gradeLevel;                     // variable for determining grade level of player
    public GameObject boardGenerator;           // variable for game object that contains the board generation scipt
    public GameObject gameBoard;                // empty variable
    public bool gameOver;                       // variable for game over state

    public GameObject[] firstRowThreeByThree; // array for checking first row sum for 3x3
    public GameObject[] secondRowThreeByThree;// array for checking second row sum for 3x3

    public GameObject[] firstRowTwoByTwo;     // array for checking first row sum for 2x2
    public GameObject[] secondRowTwoByTwo;    // array for checking second row sum for 2x2

    private int currentSum1;
    private int currentSum2;

    private int sumOneAnswer;
    private int sumTwoAnswer;

    private Kakuro sumGenerator;
    public Text feedback;

    public RectTransform [] numberSlots;

    public RectTransform[] answerPanels;

    public RectTransform[] numberSlotsTwoByTwo;
    public RectTransform[] answerPanelsTwoByTwo;

    public RectTransform positionOne;
    public RectTransform positionTwo;
    public int score;

    public Text scoreText;
    public int totalNumberedPanels;

    public GameObject inventoryHolder;

    [SerializeField] private GameObject UILayoutThreeByThree;
    [SerializeField] private GameObject UILayoutTwoByTwo;

    private int x;                      // limit for how many need to be correct to get time bonus
    private int numberCorrectSoFar;     // how many problems the player has correct so far
    private int numberCorrectTotal;     // how many problems the player has gotten correct over all
    private int numberOfProblemsAttempted; // a variable representing the total number of problems the player has attempted to do.
    public GameObject timer;


    public float gameTimer;
    public float maxTime;
    public Text gameTimerText;

    [SerializeField]private GameObject degradationBuddy;
    private DegradationManager dylan;

    public int getTotalNumberedPanels()
    {
        return totalNumberedPanels;
    }

    // Use this for initialization


    void Start ()
    {
        dylan = degradationBuddy.GetComponent<DegradationManager>();
        gameOver = false;
        x = 0;
        numberCorrectSoFar = 0;
        numberCorrectTotal = 0;
        numberOfProblemsAttempted = 0;
        gradeLevel = PlayerPrefs.GetInt("grade");
        feedback.text = "";
        score = 0;
        scoreText.text = "Score: " + score;
        sumGenerator = boardGenerator.GetComponent<Kakuro>();
        if(gradeLevel==0||gradeLevel==1)
        {
            UILayoutTwoByTwo.SetActive(true);
        }
        else
        {
            UILayoutThreeByThree.SetActive(true);
        }
        makeNewBoard();
       // if(gradeLevel== 0 || gradeLevel==1)
        //{
            //sumGenerator.generateBoardKindergartenThroughFirst();
            //sumOneAnswer = sumGenerator.getFirstSum();
            //sumTwoAnswer = sumGenerator.getSecondSum();
        //}
        //else
        //{
            //sumGenerator.calculateSumsSecondThroughFifth();
            //sumOneAnswer = sumGenerator.getFirstSum();
            //sumTwoAnswer = sumGenerator.getSecondSum();
        //}
	}
    public void setOriginal()
    {
        //trial = numberSlots[0];
    }
    public void setOriginalPositions() // resets the original postions of all the numbers for the board.
    {
        //print("Yay");
        if (gradeLevel == 0 || gradeLevel == 1)
        {
            for(int i=0;i<4;i++)
            {
                int childCount = answerPanelsTwoByTwo[i].childCount;
                if (childCount > 0)
               {
                   answerPanelsTwoByTwo[i].GetChild(0).transform.parent = numberSlotsTwoByTwo[int.Parse(answerPanelsTwoByTwo[i].GetChild(0).gameObject.name) - 1].transform;
               }
            }
        }
        else
        {
            for (int i = 0; i < 9; i++)
            {
                answerPanels[i].GetChild(0).transform.parent = numberSlots[int.Parse(answerPanels[i].GetChild(0).gameObject.name) - 1].transform;
            }
        }
    }

   

	public void makeNewBoard() // generates the board
    {
        if (gradeLevel == 0 || gradeLevel == 1)
        {
           
            //print("Total Numbered Panels in makeBoard() is "+ totalNumberedPanels);
            sumGenerator.generateBoardKindergartenThroughFirst();
            sumOneAnswer = sumGenerator.getFirstSum();
            sumTwoAnswer = sumGenerator.getSecondSum();
        }
        else
        {
           
            sumGenerator.calculateSumsSecondThroughFifth();
            sumOneAnswer = sumGenerator.getFirstSum();
            sumTwoAnswer = sumGenerator.getSecondSum();

            //print("First sum to find is " + sumOneAnswer);
            //print("Second sum to find is " + sumTwoAnswer);
        }
    }
    // Update is called once per frame
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

    public void checkBoard() // checks for correct answer for the board
    {
        if (gameOver != true)
        {
            if (currentSum1 == sumOneAnswer && currentSum2 == sumTwoAnswer)
            {
                feedback.text = "Correct";
                makeNewBoard();
                setOriginalPositions();
                score += 100;
                PlayerPrefs.SetInt("recentKakuroHighScore", score);
                scoreText.text = "Score: " + score;
                numberCorrectSoFar++;
                numberCorrectTotal++;
                numberOfProblemsAttempted++;
                if (numberCorrectSoFar == (x + 3))
                {
                    addTime();
                    x++;
                    numberCorrectSoFar = 0;
                }
            }

            else
            {
                feedback.text = "Incorrect Try again";
                numberOfProblemsAttempted++;
            }
        }
    }

    public void Happiness() //logic for calculating the happiness level based on correct answers/attempted questions
    {
        float happiness;
        float maxHappiness = .15f;

        if (numberOfProblemsAttempted >= 5)
        {
            happiness = ((numberCorrectTotal / numberOfProblemsAttempted) * maxHappiness);
            happiness = Mathf.Round(happiness * 100f) / 100f; //testing this to see if it rounds properly
        }

        else if (numberOfProblemsAttempted <5 && numberOfProblemsAttempted >=1)
        {
            happiness = (((numberCorrectTotal / numberOfProblemsAttempted) / 5) * maxHappiness) * 100;
        }
        //PlayerPrefs.SetFloat("")
    }

    public void initiateGameOver() // initiates game over state
    {
        gameOver = true;
        feedback.text = "GameOver";
        //dylan.cartkuroCalculation();
    }

    public void updateSum() // updates the sum for the first and second row while player inserts numbers in slots
    {
        currentSum1 = 0;
        currentSum2 = 0;
        if (gradeLevel>1)
        {
            for (int i = 0; i < 3; i++)
            {
                if (firstRowThreeByThree[i].transform.childCount > 0)
                {
                    //print("The name of the child in "+firstRow[i].gameObject.name+ " is " +firstRow[i].transform.GetChild(0).gameObject.name);
                    currentSum1 += int.Parse(firstRowThreeByThree[i].transform.GetChild(0).gameObject.name);
                }

                if (secondRowThreeByThree[i].transform.childCount > 0)
                {
                    currentSum2 += int.Parse(secondRowThreeByThree[i].transform.GetChild(0).gameObject.name);
                }
            }
        }
        else
        {
            for(int i=0;i<2;i++)
            {
                if (firstRowTwoByTwo[i].transform.childCount > 0)
                {
                    //print("The name of the child in "+firstRow[i].gameObject.name+ " is " +firstRow[i].transform.GetChild(0).gameObject.name);
                    currentSum1 += int.Parse(firstRowTwoByTwo[i].transform.GetChild(0).gameObject.name);
                }

                if (secondRowTwoByTwo[i].transform.childCount > 0)
                {
                    currentSum2 += int.Parse(secondRowTwoByTwo[i].transform.GetChild(0).gameObject.name);
                }
            }
        }
        print("Sum for row 1 is " + currentSum1);
        print("Sum for row 2 is " + currentSum2);
    }

}
