using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Scripts Goals: Generation of correct board based on level selection, perform end of level when game is finished, keep track of player score, and pass data back to general manager
   Authors: Brennan Kersey
 */

public class kakuroGameManger : MonoBehaviour
{
    [SerializeField] private GameObject twoBytwoCartPrefab;
    [SerializeField] private GameObject threeBythreeCartPrefab;
    [SerializeField] private GameObject gameOverPanel;

    public int gradeLevel;                     // variable for determining grade level of player
    public GameObject boardGenerator;           // variable for game object that contains the board generation scipt
    public GameObject gameBoard;                // empty variable
    public bool gameOver;                       // variable for game over state

    public GameObject[] firstRowThreeByThree; // array for checking first row sum for 3x3
    public GameObject[] secondRowThreeByThree;// array for checking second row sum for 3x3

    public GameObject[] firstRowTwoByTwo;     // array for checking first row sum for 2x2
    public GameObject[] secondRowTwoByTwo;    // array for checking second row sum for 2x2

    private int currentSum1;                // variable that represents the player's current sum of the numbers of the first row in the game
    private int currentSum2;                // variable that represent the player's current sum of the numbers of the second row in the game

    private int sumOneAnswer;               // a variable representing the summed answer that the player must achieve for the first row
    private int sumTwoAnswer;               // a variable representing the summed answer that the player must achieve for the second row

    private Kakuro sumGenerator;            // a variable representing the sum generator for creating the sums for the game board
    public Text feedback;                   // a text box representing feedback for the player(Correct or Incorrect)

    public RectTransform [] numberSlots;    // An array of transforms that represents the 3x3 slots with the numbers that the players will drag

    public RectTransform[] answerPanels;    // An array of transforms that represents the 3x3 panels that the player that the player will drag the answers into 

    public RectTransform[] numberSlotsTwoByTwo;
    public RectTransform[] answerPanelsTwoByTwo;

    public GameObject threeByThreeFirstRowTriangle;
    public GameObject threeByThreeSecondRowTriangle;

    public GameObject twoByTwoFirstRowTriangle;
    public GameObject twoByTwoSecondRowTriangle;

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

    [SerializeField] private Animator animator2x2;
    [SerializeField] private Animator animator3x3;

    private float kartMoveForward2x2;
    private float kartMoveBackward2x2;

    private float kartMoveForward3x3;
    private float kartMoveBackward3x3;

    [SerializeField] private float kindergartenTime;
    [SerializeField] private float firstGradeTime;
    [SerializeField] private float secondGradeTime;
    [SerializeField] private float thirdGradeTime;
    [SerializeField] private float fourthGradeTime;
    [SerializeField] private float fifthGradeTime;

    [SerializeField] private DragHandler[] twoByTwoBoxes;
    [SerializeField] private DragHandler[] threeByThreeBoxes;

    public int getTotalNumberedPanels()
    {
        return totalNumberedPanels;
    }

    // Use this for initialization

    public void setGameTimer()
    {
        int seconds;
        int minutes;
        int hours;

        string timerString;
        switch (gradeLevel)
        {
            case 0:
                gameTimer = kindergartenTime;
                seconds = (int)(gameTimer % 60);
                minutes = (int)(gameTimer / 60) % 60;
                hours = (int)(gameTimer / 3600) % 24;
                timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
                gameTimerText.text = timerString;
                break;

            case 1:
                gameTimer = firstGradeTime;
                seconds = (int)(gameTimer % 60);
                minutes = (int)(gameTimer / 60) % 60;
                hours = (int)(gameTimer / 3600) % 24;
                timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
                gameTimerText.text = timerString;
                break;

            case 2:
                gameTimer = secondGradeTime;
                seconds = (int)(gameTimer % 60);
                minutes = (int)(gameTimer / 60) % 60;
                hours = (int)(gameTimer / 3600) % 24;
                timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
                gameTimerText.text = timerString;
                break;

            case 3:
                gameTimer = thirdGradeTime;
                seconds = (int)(gameTimer % 60);
                minutes = (int)(gameTimer / 60) % 60;
                hours = (int)(gameTimer / 3600) % 24;
                timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
                gameTimerText.text = timerString;
                break;

            case 4:
                gameTimer = fourthGradeTime;
                seconds = (int)(gameTimer % 60);
                minutes = (int)(gameTimer / 60) % 60;
                hours = (int)(gameTimer / 3600) % 24;
                timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
                gameTimerText.text = timerString;
                break;

            case 5:
                gameTimer = fifthGradeTime;
                seconds = (int)(gameTimer % 60);
                minutes = (int)(gameTimer / 60) % 60;
                hours = (int)(gameTimer / 3600) % 24;
                timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
                gameTimerText.text = timerString;
                break;

        }
    }

    public void deactivateDrags()
    {
        if(gradeLevel<=1)
        {
            for(int i=0;i<twoByTwoBoxes.Length;i++)
            {
                twoByTwoBoxes[i].enabled = false;
            }
        }
        else
        {
            for(int i=0;i<threeByThreeBoxes.Length;i++)
            {
                threeByThreeBoxes[i].enabled = false;
            }
        }
    }

    public void activateDrags()
    {
        if (gradeLevel <= 1)
        {
            for (int i = 0; i < twoByTwoBoxes.Length; i++)
            {
                twoByTwoBoxes[i].enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < threeByThreeBoxes.Length; i++)
            {
                threeByThreeBoxes[i].enabled = true;
            }
        }
    }
    void Start ()
    {

        kartMoveForward2x2 = animator2x2.runtimeAnimatorController.animationClips[0].length;
        kartMoveBackward2x2 = animator2x2.runtimeAnimatorController.animationClips[1].length;

        kartMoveForward3x3= animator3x3.runtimeAnimatorController.animationClips[0].length;
        kartMoveBackward3x3= animator3x3.runtimeAnimatorController.animationClips[1].length;

        print("Current KartCuro score that is called from kartcuro manager: " + (PlayerPrefs.GetInt("kakuroHighScore")));
        dylan = degradationBuddy.GetComponent<DegradationManager>();
        gameTimerText.text = "3:00";
        gameOver = true;
        x = 0;
        numberCorrectSoFar = 0;
        numberCorrectTotal = 0;
        numberOfProblemsAttempted = 0;
        gradeLevel = PlayerPrefs.GetInt("grade");
        deactivateDrags();
        setGameTimer();
        feedback.text = "";
        score = 0;
        scoreText.text = "" + score;
        sumGenerator = boardGenerator.GetComponent<Kakuro>();
        if(gradeLevel==0||gradeLevel==1)
        {
            UILayoutTwoByTwo.SetActive(true);
            twoBytwoCartPrefab.SetActive(true);
        }
        else
        {
            UILayoutThreeByThree.SetActive(true);
            threeBythreeCartPrefab.SetActive(true);
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
                //setOriginalPositions();
                score += 100;
                PlayerPrefs.SetInt("recentKakuroHighScore", score);
                scoreText.text = "" + score;
                if(gradeLevel==0 || gradeLevel==1)
                {
                    print("start animating 2x2");
                    StartCoroutine(kartAnimation2x2());
                }
                else
                {
                    StartCoroutine(kartAnimation3x3());
                }
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
                setOriginalPositions();
                numberOfProblemsAttempted++;
            }
        }
    }
    public void checkForNewHighScore()
    {
        int currentHighScore = PlayerPrefs.GetInt("kakuroHighScore");
       // print("This is the current kakuroHighScore: " + currentHighScore);
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("kakuroHighScore", score);
            PlayerPrefs.Save();
        }
    }
    public void calculateRestoration()
    {
        DegradationManager degredationManager = GameObject.FindGameObjectWithTag("degredationManager").GetComponent<DegradationManager>();
        degredationManager.aAttempted = numberOfProblemsAttempted;
        degredationManager.aCorrect = numberCorrectTotal;
        degredationManager.assemblyCalulate();
        degredationManager.gameHasBeenPlayed(3);
        checkForNewHighScore();
       // degredationManager.setScoreOfRecentPlayedGame(score);
    }

    public void initiateGameOver() // initiates game over state
    {
        gameOver = true;
        feedback.text = "GameOver";
        gameOverPanel.GetComponent<gameOverPanel>().crossCheckScores(1, score);
        gameOverPanel.SetActive(true);
        calculateRestoration();
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
    IEnumerator kartAnimation2x2()
    {
        twoByTwoFirstRowTriangle.SetActive(false);
        twoByTwoSecondRowTriangle.SetActive(false);
        animator2x2.SetInteger("nextTransition2x2", 0);
        yield return new WaitForSeconds(kartMoveForward2x2);
        twoBytwoCartPrefab.SetActive(false);
        setOriginalPositions();
        animator2x2.SetInteger("nextTransition2x2", 1);
        yield return new WaitForSeconds(.5f);
        twoBytwoCartPrefab.SetActive(true);
        yield return new WaitForSeconds(kartMoveBackward2x2);
        twoByTwoFirstRowTriangle.SetActive(true);
        twoByTwoSecondRowTriangle.SetActive(true);
        animator2x2.SetInteger("nextTransition2x2", -1);
        yield return new WaitForSeconds(kartMoveBackward2x2);
        yield return null;
    }
    
    IEnumerator kartAnimation3x3()
    {
        threeByThreeFirstRowTriangle.SetActive(false);
        threeByThreeSecondRowTriangle.SetActive(false);
        animator3x3.SetInteger("nextTransition3x3",0);
        yield return new WaitForSeconds(kartMoveForward3x3);
        //threeBythreeCartPrefab.SetActive(false);
        threeBythreeCartPrefab.GetComponent<Image>().enabled = false;
        answerPanels[0].parent.gameObject.SetActive(false);
        setOriginalPositions();
        animator3x3.SetInteger("nextTransition3x3",1);
        yield return new WaitForSeconds(kartMoveBackward3x3/2);
        threeBythreeCartPrefab.GetComponent<Image>().enabled = true;
        answerPanels[0].parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(kartMoveBackward3x3);
        threeByThreeFirstRowTriangle.SetActive(true);
        threeByThreeSecondRowTriangle.SetActive(true);
        animator3x3.SetInteger("nextTransition3x3", -1);
        yield return new WaitForSeconds(kartMoveBackward3x3);
        yield return null;
    }
}
