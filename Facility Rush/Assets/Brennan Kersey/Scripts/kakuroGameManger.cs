using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Scripts Goals: Generation of correct board based on level selection, perform end of level when game is finished, keep track of player score, and pass data back to general manager
 */

public class kakuroGameManger : MonoBehaviour
{
    private int gradeLevel;
    public GameObject boardGenerator;
    public GameObject gameBoard;
    public bool gameOver;

    public GameObject[] firstRowThreeByThree;
    public GameObject[] secondRowThreeByThree;

    public GameObject[] firstRowTwoByTwo;
    public GameObject[] secondRowTwoByTwo;

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

    private int x;
    private int numberCorrectSoFar;
    private int numberCorrectTotal;

    public GameObject timer;

    

    public int getTotalNumberedPanels()
    {
        return totalNumberedPanels;
    }

    // Use this for initialization


    void Start ()
    {
        gameOver = false;
        x = 0;
        numberCorrectSoFar = 0;
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
    public void setOriginalPositions()
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

   

	public void makeNewBoard()
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
	void Update ()
    {
		
	}

    public void checkBoard()
    {
        if (gameOver != false)
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
                if (numberCorrectSoFar == (x + 3))
                {
                    timer.GetComponent<timer>().addTime();
                    x++;
                    numberCorrectSoFar = 0;
                }
            }

            else
            {
                feedback.text = "Incorrect Try again";
            }
        }
    }

    public void initiateGameOver()
    {
        gameOver = true;
        feedback.text = "GameOver";
    }

    public void updateSum()
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
