﻿using System.Collections;
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


    [SerializeField] private Text firstRowSum;  // A text variable for the first row that the player must drag the boxes to sum to
    [SerializeField] private Text secondRowSum; // A text variable for the second row that the player must drag the boxes to sum to
    public int score;                           // A integer variable used to keep track of the player's score

    public Text scoreText;                      // A text variable used to indicate the score on screen
    public int totalNumberedPanels;             // An integer that represents the total number of answer panels that a answer grid inside of the Cart gameobject, this is used for answer checking

    [SerializeField] private Text tutorialDialog;
    [SerializeField] private kartcuroTutorialDialog dialogManager;


    [SerializeField] private GameObject UILayoutThreeByThree; // A variable representing the Gameobject for the 3x3 Game board that can be initiated on start
    [SerializeField] private GameObject UILayoutTwoByTwo;     // A variable representing the Gameobject for the 2x2 Game bOard that can be initiated on start 

    [SerializeField] private Animator tutorialAnimator;
    [SerializeField] private Animator cartAnimator;
    [SerializeField] private bool isTwoBoxMoved;
    [SerializeField] private bool isOneBoxMoved;
    [SerializeField] private bool isThreeBoxMoved;
    [SerializeField] private bool isFourthBoxMoved;
    [SerializeField] private bool tutorialFinished;

    [SerializeField] private GameObject dragDirectionsBox;
    [SerializeField] private GameObject scoreDirectionsBox;
    [SerializeField] private GameObject timerDirectionsBox;
    [SerializeField] private GameObject animatedHand;

    private float kartMoveForward;
    private float kartMoveBackward;

    [SerializeField] private GameObject twoByTwoTopTriangle;
    [SerializeField] private GameObject twoByTwoBottomTriangle;

    [SerializeField] private GameObject congratsPanel;

    [SerializeField] private DragHandler twoBox;
    [SerializeField] private DragHandler oneBox;
    [SerializeField] private DragHandler threeBox;
    [SerializeField] private DragHandler fourBox;


    public int getTotalNumberedPanels()
    {
        return totalNumberedPanels;
    }

    // Use this for initialization
    void Start()
    {
        kartMoveForward=cartAnimator.runtimeAnimatorController.animationClips[0].length;
        kartMoveBackward=cartAnimator.runtimeAnimatorController.animationClips[1].length;
        StartCoroutine(animateDirections(0));
        numberSlotsTwoByTwo[0].gameObject.transform.GetChild(0).GetComponent<DragHandler>().enabled = false;
        numberSlotsTwoByTwo[2].gameObject.transform.GetChild(0).GetComponent<DragHandler>().enabled = false;
        numberSlotsTwoByTwo[3].gameObject.transform.GetChild(0).GetComponent<DragHandler>().enabled = false;
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            initiateGameOver();
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
        gameOverPanel.GetComponent<gameOverPanel>().crossCheckScores(1,score);
        gameOverPanel.SetActive(true);
    }

    public void updateSum() // updates the sum for the first and second row while player inserts numbers in slots
    {
        currentSum1 = 0;
        currentSum2 = 0;
        int panelsOccupied = firstRowTwoByTwo[0].transform.childCount + firstRowTwoByTwo[1].transform.childCount + secondRowTwoByTwo[0].transform.childCount + secondRowTwoByTwo[1].transform.childCount;
        switch(panelsOccupied)
        {
            

            case 1:
                StartCoroutine(animateDirections(1));
               // twoBox.enabled = false;
                numberSlotsTwoByTwo[0].gameObject.transform.GetChild(0).GetComponent<DragHandler>().enabled = true;
                numberSlotsTwoByTwo[1].gameObject.GetComponent<Image>().raycastTarget = false;
                StartCoroutine(shutOffDrag(twoBox));
                break;

            case 2:
                StartCoroutine(animateDirections(2));
                tutorialDialog.text = "Each row should add up to the number in the orange triangle.";
                numberSlotsTwoByTwo[2].gameObject.transform.GetChild(0).GetComponent<DragHandler>().enabled = true;
                numberSlotsTwoByTwo[0].gameObject.GetComponent<Image>().raycastTarget = false;
                StartCoroutine(shutOffDrag(oneBox));
                break;

            case 3:
                StartCoroutine(animateDirections(3));
                numberSlotsTwoByTwo[3].gameObject.transform.GetChild(0).GetComponent<DragHandler>().enabled = true;
                numberSlotsTwoByTwo[2].gameObject.GetComponent<Image>().raycastTarget = false;
                StartCoroutine(shutOffDrag(threeBox));
                break;
            case 4:
                StartCoroutine(animateDirections(4));
                numberSlotsTwoByTwo[3].gameObject.GetComponent<Image>().raycastTarget = false;
                StartCoroutine(shutOffDrag(fourBox));
                break;

        }
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
    IEnumerator animateDirections(int whichBox)
    {
        switch(whichBox)
        {
            case 0:
                tutorialAnimator.SetInteger("moving", 1);
                
                break;
            case 1:
                tutorialAnimator.SetInteger("moving", 2);
                answerPanelsTwoByTwo[0].gameObject.GetComponent<Image>().raycastTarget = true;
                break;
            case 2:
                tutorialAnimator.SetInteger("moving", 3);
                answerPanelsTwoByTwo[1].gameObject.GetComponent<Image>().raycastTarget = true;
                break;
            case 3:
                tutorialAnimator.SetInteger("moving", 4);
                answerPanelsTwoByTwo[3].gameObject.GetComponent<Image>().raycastTarget = true;
                break;
            case 4:
                dragDirectionsBox.SetActive(false);
                scoreDirectionsBox.SetActive(true);
                StartCoroutine(kartMoving());
                tutorialAnimator.SetInteger("moving", 5);
                dialogManager.isTimeToTap = true;
                //yield return new WaitForSeconds(3f);
                //scoreDirectionsBox.SetActive(false);
                //tutorialAnimator.SetInteger("moving", 6);
                //timerDirectionsBox.SetActive(true);
                //yield return new WaitForSeconds(1f);
                ////tutorialAnimator.SetInteger("moving", 6);
                //yield return new WaitForSeconds(3f);
                //tutorialAnimator.SetInteger("moving", 0);
                //yield return new WaitForSeconds(3f);
                //timerDirectionsBox.SetActive(false);
                //animatedHand.SetActive(false);
                //gameOverPanel.SetActive(true);
                //congratsPanel.SetActive(true);
                //congratsPanel.GetComponent<kickBackToMainMenu>().activateAutoKick();
                break;
        }
        

        yield return null;
    }
    public void pointToTimer()
    {
        scoreDirectionsBox.SetActive(false);
        timerDirectionsBox.SetActive(true);
        tutorialAnimator.SetInteger("moving", 6);
    }
    public void startGameOver()
    {
        timerDirectionsBox.SetActive(false);
        animatedHand.SetActive(false);
        gameOverPanel.SetActive(true);
        congratsPanel.SetActive(true);
        PlayerPrefs.SetString("KartCuroTutorialComplete", "true");
        congratsPanel.GetComponent<kickBackToMainMenu>().activateAutoKick(1);
    }
    IEnumerator kartMoving()
    {
        twoByTwoTopTriangle.SetActive(false);
        twoByTwoBottomTriangle.SetActive(false);
        cartAnimator.SetInteger("nextTransition2x2", 0);
        yield return new WaitForSeconds(kartMoveForward);
        twoBytwoCartPrefab.SetActive(false);
        cartAnimator.SetInteger("nextTransition2x2", 1);
        yield return new WaitForSeconds(.5f);
        twoBytwoCartPrefab.SetActive(true);
        yield return new WaitForSeconds(kartMoveBackward);
        cartAnimator.SetInteger("nextTransition2x2", -1);
        yield return null;
        twoByTwoTopTriangle.SetActive(true);
        twoByTwoBottomTriangle.SetActive(true);
    }

    IEnumerator shutOffDrag(DragHandler boxToShutOff)
    {
        yield return new WaitForSeconds(.1f);
        boxToShutOff.enabled = false;
    }
}
