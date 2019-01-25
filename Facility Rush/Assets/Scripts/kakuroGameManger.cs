using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Scripts Goals: Generation of correct board based on level selection, perform end of level when game is finished, keep track of player score, and pass data back to general manager
 */

public class kakuroGameManger : MonoBehaviour
{
    public int gradeLevel;
    public GameObject boardGenerator;
    public GameObject gameBoard;
    public bool gameOver;
    public GameObject[] firstRow;
    public GameObject[] secondRow;

    private int currentSum1;
    private int currentSum2;

    private int sumOneAnswer;
    private int sumTwoAnswer;

    private Kakuro sumGenerator;
    public Text feedback;

    public RectTransform [] numberSlots;

    public RectTransform[] answerPanels;

    public RectTransform positionOne;
    public RectTransform positionTwo;
    public int score;

    public Text scoreText;

    // Use this for initialization
    void Start ()
    {
        feedback.text = "";
        score = 0;
        scoreText.text = "Score: " + score;
        sumGenerator = boardGenerator.GetComponent<Kakuro>();
        //if(firstRow[0].GetC)
        //gradeLevel = PlayerPrefs.GetInt("grade");
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
       for(int i=0;i<9;i++)
        {
            answerPanels[i].GetChild(0).transform.parent = numberSlots[int.Parse(answerPanels[i].GetChild(0).gameObject.name)-1].transform;
        }
    }

   

	public void makeNewBoard()
    {
        if (gradeLevel == 0 || gradeLevel == 1)
        {
            sumGenerator.generateBoardKindergartenThroughFirst();
            sumOneAnswer = sumGenerator.getFirstSum();
            sumTwoAnswer = sumGenerator.getSecondSum();
        }
        else
        {
            sumGenerator.calculateSumsSecondThroughFifth();
            sumOneAnswer = sumGenerator.getFirstSum();
            sumTwoAnswer = sumGenerator.getSecondSum();

            print("First sum to find is " + sumOneAnswer);
            print("Second sum to find is " + sumTwoAnswer);
        }
    }
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void checkBoard()
    {
        if(currentSum1==sumOneAnswer && currentSum2==sumTwoAnswer)
        {
            feedback.text = "Correct";
            makeNewBoard();
            setOriginalPositions();
            score += 100;
            PlayerPrefs.SetInt("recentKakuroHighScore", score);
            scoreText.text = "Score: " + score;
        }

        else
        {
            feedback.text = "Incorrect Try again";
        }
    }

    public void initiateGameOver()
    {

    }

    public void updateSum()
    {
        currentSum1 = 0;
        currentSum2 = 0;
        for(int i=0;i<3;i++)
        {
            if(firstRow[i].transform.childCount>0)
            {
                //print("The name of the child in "+firstRow[i].gameObject.name+ " is " +firstRow[i].transform.GetChild(0).gameObject.name);
                currentSum1 += int.Parse(firstRow[i].transform.GetChild(0).gameObject.name);
            }

            if(secondRow[i].transform.childCount>0)
            {
                currentSum2 += int.Parse(secondRow[i].transform.GetChild(0).gameObject.name);
            }
        }
        print("Sum for row 1 is " + currentSum1);
        print("Sum for row 2 is " + currentSum2);
    }
}
