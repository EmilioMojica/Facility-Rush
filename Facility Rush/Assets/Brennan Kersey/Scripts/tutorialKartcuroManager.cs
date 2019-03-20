using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialKartcuroManager : MonoBehaviour
{
    [SerializeField] private GameObject twoBytwoCartPrefab;         // Game Object variable that represents green cart prefab for the 2x2 game board
    [SerializeField] private GameObject threeBythreeCartPrefab;     // Game Object variable that represents green cart prefab for the 3x3 game board
    [SerializeField] private GameObject gameOverPanel;              // Game Object variable that represents the Game Over Panel. 

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

    public RectTransform[] numberSlots;    // An array of transforms that represents the 3x3 slots with the numbers that the players will drag

    public RectTransform[] answerPanels;    // An array of transforms that represents the 3x3 panels that the player that the player will drag the answers into 

    public RectTransform[] numberSlotsTwoByTwo; // An array of transforms for the boxes that will be dragged for the 2x2 GameBoard
    public RectTransform[] answerPanelsTwoByTwo; // An array of transforms for the spaces where the boxes will be dragged

    // public RectTransform positionOne;
    // public RectTransform positionTwo;

    [SerializeField] private Text firstRowSum;  // A text variable for the first row that the player must drag the boxes to sum to
    [SerializeField] private Text secondRowSum; // A text variable for the second row that the player must drag the boxes to sum to
    public int score;                           // A integer variable used to keep track of the player's score

    public Text scoreText;                      // A text variable used to indicate the score on screen
    public int totalNumberedPanels;             // An integer that represents the total number of answer panels that a answer grid inside of the Cart gameobject, this is used for answer checking

    //public GameObject inventoryHolder;

    [SerializeField] private GameObject UILayoutThreeByThree; // A variable representing the Gameobject for the 3x3 Game board that can be initiated on start
    [SerializeField] private GameObject UILayoutTwoByTwo;     // A variable representing the Gameobject for the 2x2 Game bOard that can be initiated on start 

    
    //public GameObject timer;


    //public float gameTimer;
    //public float maxTime;
    //public Text gameTimerText;

   // [SerializeField] private GameObject degradationBuddy;
   // private DegradationManager dylan;

    public int getTotalNumberedPanels()
    {
        return totalNumberedPanels;
    }

    // Use this for initialization


    void Start()
    {
        gameOver = false;
        gradeLevel = 0;
        feedback.text = "";
        score = 0;
        scoreText.text = "" + score;
        sumGenerator = boardGenerator.GetComponent<Kakuro>();
        if (gradeLevel == 0 || gradeLevel == 1)
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
            for (int i = 0; i < 4; i++)
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
            sumOneAnswer = 4;
            sumTwoAnswer = 6;

            firstRowSum.text = sumOneAnswer.ToString();
            secondRowSum.text = sumTwoAnswer.ToString();
        }
        else
        {

            sumGenerator.calculateSumsSecondThroughFifth();
            sumOneAnswer = 24;
            sumTwoAnswer = 15;
        }
    }
    // Update is called once per frame
    void Update()
    {
     
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
                scoreText.text = "" + score;              
            }

            else
            {
                feedback.text = "Incorrect Try again";
                setOriginalPositions();
            }
        }
    }
    public void checkForNewHighScore()
    {
        int currentHighScore = PlayerPrefs.GetInt("kakuroHighScore");
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("kakuroHighScore", score);
            PlayerPrefs.Save();
        }
    }

    public void initiateGameOver() // initiates game over state
    {
        gameOver = true;
        feedback.text = "GameOver";
        gameOverPanel.SetActive(true);
    }

    public void updateSum() // updates the sum for the first and second row while player inserts numbers in slots
    {
        currentSum1 = 0;
        currentSum2 = 0;
        if (gradeLevel > 1)
        {
            for (int i = 0; i < 3; i++)
            {
                if (firstRowThreeByThree[i].transform.childCount > 0)
                {
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
            for (int i = 0; i < 2; i++)
            {
                if (firstRowTwoByTwo[i].transform.childCount > 0)
                {
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
