using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using Random = UnityEngine.Random;
using Slider = UnityEngine.UI.Slider;

public class ChutesManager : MonoBehaviour, IHasChanged
{
    public int currentLevel = 1;
    public List<Text> equations;

    public List<Text> answers;//4 answer variants texts
    public List<ChutesSlot> answerSlots;
    private int trueID;// ID of true answer that helps to place true answer at random position 
    //generate the right equation 
    private int rightID;
    private int score;
    public Text scoreValue, timerValue;
    //replacement
    public float timerNum;
    private List<float> tempAnswers = new List<float>();

    private string[] operators = new string[] { "+", "-", "*", "/" };   // an array to hold the possible operators used by the equation
    [SerializeField] private Transform slots;
    public GameObject playscene;
    public GameObject failscene;

    public Text bestScore;

    public Slider timeSlider;

    public bool game;
    public Animator answersGoUP;
    public Animator tubeOne;
    public Animator tubeTwo;
    public Animator tubeThree;
    public Animator tubeFour;

    public enum CounterState
    {
        oneQ = 0,
        twoQ,
        threeQ,
        fourQ,
        fiveQ
    }
    public int counter;
    public CounterState counterState;

    private int wrongNumber;

    int startingNumber;  // randomly generated number for beginning of equation generation for addition and subtraction, unless it is mutiplication or division
    int secondNumber;   //  a randomly generated number raging from 1 to 100
    int firstNumber;    //  a randomly generated number ranging from 1 to 100
    string equationOperator; // a string character that represents the operator in the equation

    private int[] factorsOf100 = new int[] { 1, 2, 4, 5, 10, 25, 50, 100 };
    private int[] MultipleOfTwo = new int[] { 1, 2, 5, 10 };
    private int[] MultipleOfTwoBig = new int[] { 10, 30, 50, 100 };
    private int[] factorsOf144 = new int[] { 1, 2, 3, 4, 6, 8, 9, 12, 16, 18, 24, 36, 48, 72, 144 };
    private int[] factorsOf1441 = new int[] { 1, 2, 3, 4, 6, 12 };
    private int[] factorsOf1442 = new int[] { 12, 24, 36, 48, 72, 144 };
    private int[] factorsOf900 = new int[] { 1, 2, 3, 4, 5, 6, 9, 10, 12, 15, 18, 20, 25, 30, 36, 45, 50, 60, 75, 90, 100, 150, 180, 225, 300, 450, 900 };

    private bool isToMuch;

    // Use this for initialization
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("grade");
        answersGoUP.SetBool("NumberBack", true);
        playscene.SetActive(true);
        failscene.SetActive(false);
        timeSlider.value = 1;
        //replacement to Time text
        timerNum = 2f;
        timerValue.text = (int)timerNum / 60 + ":" + (int)timerNum % 60;
        //--------------------------------------------
        game = false;

        //Generate();
    }

    // Update is called once per frame
    void Update()
    {
        bestScore.text = score.ToString();

        if (game)
        {
            timeSlider.value -= Time.deltaTime / 30;
            //replacement to Time text
            timerNum -= Time.deltaTime;
            timerValue.text = (int)timerNum / 60 + ":" + (int)timerNum % 60; 
            if (timerNum <= 0)
            {
                timeOut();
            }
            //-----------------------------------
            if (timeSlider.value <= 0) //Run "GameOver" function if timeleft(slider value) is out
            {
                Debug.Log("kkkkkkkkk");
                timeOut();
            }

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            answersGoUP.SetBool("NumberMove", false);
            answersGoUP.SetBool("NumberBack", true);
            game = true;
            score = 0;
            scoreValue.text = score.ToString();
            timeSlider.value = 1;
            // replacement of slider
            timerNum = 20f;
            timerValue.text = (int)timerNum / 60 + ":" + (int)timerNum % 60;
            //--------------------------
            failscene.SetActive(false);
            playscene.SetActive(true);

        }
    }

    public void restartButton()
    {
        answersGoUP.SetBool("NumberMove", false);
        answersGoUP.SetBool("NumberBack", true);
        game = true;
        score = 0;
        scoreValue.text = score.ToString();
        timeSlider.value = 1;
        // replacement of timer slider
        //timerNum = 21f;
        timerValue.text = (int)timerNum / 60 + ":" + (int)timerNum % 60;
        //---------------------------------------
        failscene.SetActive(false);
        //playscene.SetActive(true);
    }
    /*public void CheckAnswer(int asnwerID)
    {               //This is on all 4 of answer buttons that send here answer ID pre-declared in Button component.
                    //Here we get time between answers to prevent fast chaos selecting answers (otherwise we can cheat scores)
        if (asnwerID == trueID)      //If answer ID is equal to true ID that means right answer pressed. Also we check if period is ok and we run according function.
            gotRight();
    }*/
    //DragHandler dh= new DragHandler();
    //MoveBack mb = new MoveBack();

    public void ChangeCounterState()
    {
        if (counter <= 5)
        {
            counterState = CounterState.oneQ;
        }
        else if (counter > 5 && counter <= 15)
        {
            counterState = CounterState.twoQ;
        }
        else if (counter > 15 && counter <= 30)
        {
            counterState = CounterState.threeQ;
        }
        else if (counter > 30 && counter <= 50)
        {
            counterState = CounterState.fourQ;
        }
        else
        {
            counterState = CounterState.fiveQ;
        }
    }

    public void AddTimeToTimer()
    {
        switch (counterState)
        {
            case CounterState.oneQ:
                timerNum += 5;
                break;
            case CounterState.twoQ:
                if (counter % 2 == 1)
                {
                    timerNum += 5;
                }
                break;
            case CounterState.threeQ:
                if (counter % 3 == 0)
                {
                    timerNum += 5;
                }
                break;
            case CounterState.fourQ:
                if (counter % 4 == 2)
                {
                    timerNum += 5;
                }
                break;
            case CounterState.fiveQ:
                if (counter % 5 == 0)
                {
                    timerNum += 5;
                }
                break;
        }
    }
    
    public void isRight()
    {
        switch (rightID)
        {
            case 0:
                tubeOne.SetBool("Correct1", true);
                
                break;
            case 1:
                tubeTwo.SetBool("Correct2", true);
                break;
            case 2:
                tubeThree.SetBool("Correct3", true);
                break;
            case 3:
                tubeFour.SetBool("Correct4", true);
                break;

        }

        answersGoUP.SetBool("NumberMove", true);

        StartCoroutine("WaitAndRight", 0.5f);

    }

    public void gotRight()
    {   
        //This runs when we pressed right answer 
        //answersGoUP.SetBool("NumberBack", true);
        answersGoUP.SetBool("NumberMove", false);

        tubeOne.SetBool("Correct1", false);

        tubeTwo.SetBool("Correct2", false);

        tubeThree.SetBool("Correct3", false);

        tubeFour.SetBool("Correct4", false);
        score += 1;
        //score += currentLevel * 10;                         //Increasing score value proportionally to current level (at fisrt level +1, at second +2 etc)
        scoreValue.text = score.ToString();


        //
        int y = trueID + 1;
        string x = y.ToString();
        GameObject.Find(x).GetComponent<ChutesDrag>().MoveBack();

        counter++;
        ChangeCounterState();
        AddTimeToTimer();
        Generate();                                     //After got correct answer we generate new one
    }
    IEnumerator WaitAndRight(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gotRight();

    }
    public void isWrong()
    {
        //tubeOne.SetBool("Correct1", true);

        switch (wrongNumber)
        {
            case 1:
                tubeOne.SetBool("Wrong1", true);
                break;
            case 2:
                tubeTwo.SetBool("Wrong2", true);
                break;
            case 3:
                tubeThree.SetBool("Wrong3", true);
                break;
            case 4:
                tubeFour.SetBool("Wrong4", true);
                break;

        }

        answersGoUP.SetBool("NumberMove", true);

        answersGoUP.SetBool("NumberBack", false);
        StartCoroutine("WaitAndWrong", 0.5f);

    }

    private void timeOut()
    {
        tubeOne.SetBool("Wrong1", false);

        tubeTwo.SetBool("Wrong2", false);

        tubeThree.SetBool("Wrong3", false);

        tubeFour.SetBool("Wrong4", false);

        answersGoUP.SetBool("NumberMove", false);
        answersGoUP.SetBool("NumberBack", true);
        playscene.SetActive(false);
        failscene.SetActive(true);
        failscene.GetComponent<gameOverPanel>().crossCheckScores(0, score);
        game = false;
        calculateRestoration();
    }

    public void checkForNewHighScore()
    {
        int currentHighScore = PlayerPrefs.GetInt("chutesHighScore");
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("chutesHighScore", score);
        }
    }
    public void calculateRestoration()
    {
        DegradationManager degredationManager = GameObject.FindGameObjectWithTag("degredationManager").GetComponent<DegradationManager>();
        degredationManager.aAttempted = score + 1;
        degredationManager.aCorrect = score;
        degredationManager.chuteCalculate();
        degredationManager.gameHasBeenPlayed(1);
        checkForNewHighScore();
        degredationManager.setScoreOfRecentPlayedGame(score);
    }
    public void gotWrong()
    {

        tubeOne.SetBool("Wrong1", false);

        tubeTwo.SetBool("Wrong2", false);

        tubeThree.SetBool("Wrong3", false);

        tubeFour.SetBool("Wrong4", false);
        //answersGoUP.SetBool("NumberBack", true);

        answersGoUP.SetBool("NumberMove", false);
        answersGoUP.SetBool("NumberBack", true);
        Debug.Log("<color=#9400D3>" + "WRONG" + "</color>");
        if (game)
        {
            for (int i = 01; i <= 4; i++)
            {
                string x = i.ToString();
                GameObject.Find(x).GetComponent<ChutesDrag>().MoveBack();
                //GameObject.Find(x).GetComponent<DragHandler>().MoveBack();
            }
            failscene.SetActive(true);
        }
        game = false;
        //Generate();

    }
    IEnumerator WaitAndWrong(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gotWrong();

    }

    public void Generate()
    {

        //Runs at "StartPlay" and after every correct answer. Generates new expression and answers
        game = true;
        timeSlider.value = 1;
        // replacement of timer slider
        //timerNum = 21f;
        timerValue.text = (int)timerNum / 60 + ":" + (int)timerNum % 60;
        //------------------------------------
        tempAnswers.Clear();                            //Clear list filled with previous answers
        int operationID;                                //Declaring operation ID variable

        //Firstly we choose random opeation by generatig random number ("1" is "+", "2" is "-", "3" is "×", "4" is "÷")
        int currentLimit = 0;
        if (currentLevel == 0)
        {
            operationID = 1;
            currentLimit = 6;
        }                          //Here I added a condition that limits operaion range for first level 
        else if (currentLevel == 1)
        {
            operationID = 2;
            currentLimit = 11;
        }
        else if (currentLevel == 2)
        {
            //operationID = 3;
            operationID = 3;
            currentLimit = 51;
        }
        else if (currentLevel == 3)
        {
            operationID = 4;
            currentLimit = 51;
        }
        else if (currentLevel == 4)                                      //If we are at 1st level then we use only + or -
        {
            operationID = 5;           //Otherwise we use all operaions
            currentLimit = 51;
        }
        else
        {
            operationID = 6;
            currentLimit = 100;
        }

        //int currentLimit = 5 + (currentLevel - 1) * 5;     //Here we declaring basic maximum limit for numbers in expression proportionally to current level (level 1 - limit 10, level 2 - 15 etc +5 each)
        //float left = Random.Range((currentLevel - 1) * 5, currentLimit);//"Left side" integer. Generated according to current level and limit
        float left = Random.Range(currentLevel * 5, currentLimit);
        float right = 0;                                //Declaring "Right side" integer variable
        string operationText = "";                       //Declaring operation symbol variable (+-×÷)
        //string operatorText = "-";
        float result = 0;                               //Declaring vaiable that keeps correct answer

        // List<string> newtexts= new List<string>();

        //Then according to operation ID and "Left" integer we generate "Right" ineteger and answers
        //Here is logic for each operation
        int operatorDecider; // = (int)Random.Range(0, 2);  // random number generation to decide operator/ Type of problem
        string quation;
        switch (operationID)
        {
            case 1:                                     //Operation +
                operatorDecider = 0;  // random number generation to decide operator/ Type of problem
                quation = "";
                startingNumber = (int)Random.Range(1, 11);
                firstNumber = (int)Random.Range(1, startingNumber);
                if (startingNumber + firstNumber >= 10)
                {
                    firstNumber = 10 - startingNumber;
                    Debug.Log("too much!!!!");
                    isToMuch = true;
                }
                secondNumber = startingNumber - firstNumber;
                switch (operatorDecider) // based on result of operatorDecider generates either addition or subtraction problem
                {
                    case 0:
                        equationOperator = operators[0];
                        quation = "" + secondNumber + equationOperator + firstNumber + "=" + startingNumber;


                        right = firstNumber;//Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        operationText = equationOperator;                     //Operation string assignment
                        result = left + right;                  //Calculating the result of expression	
                        if (isToMuch)
                        {
                            tempAnswers.Add(result);                //Insert correct and three fake answers in answers list
                            tempAnswers.Add(result - 1);
                            tempAnswers.Add(result - 2);
                            tempAnswers.Add(result - 3);
                            isToMuch = false;
                        }
                        else
                        {

                            tempAnswers.Add(result);                //Insert correct and three fake answers in answers list
                            tempAnswers.Add(result + 1);
                            tempAnswers.Add(result - 1);
                            tempAnswers.Add(result + 2);
                        }
                        GenerateQuestion(left, right, operationID, operatorDecider);

                        break;
                }
                break;
            case 2:                                     //Operation -

                operatorDecider = (int)Random.Range(0, 2);  // random number generation to decide operator/ Type of problem
                quation = "";
                startingNumber = (int)Random.Range(1, 21);
                firstNumber = (int)Random.Range(1, startingNumber);
                if (startingNumber + firstNumber >= 20)
                {
                    firstNumber = 20 - startingNumber;
                    Debug.Log("too much!!!!");
                    isToMuch = true;
                }
                secondNumber = startingNumber - firstNumber;
                switch (operatorDecider) // based on result of operatorDecider generates either addition or subtraction problem
                {
                    case 0:
                        equationOperator = operators[0];
                        quation = "" + secondNumber + equationOperator + firstNumber + "=" + startingNumber;


                        right = firstNumber;//Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        operationText = equationOperator;                     //Operation string assignment
                        result = left + right;                  //Calculating the result of expression	

                        if (isToMuch)
                        {
                            tempAnswers.Add(result);                //Insert correct and three fake answers in answers list
                            tempAnswers.Add(result - 1);
                            tempAnswers.Add(result - 2);
                            tempAnswers.Add(result - 3);
                            isToMuch = false;
                        }
                        else
                        {

                            tempAnswers.Add(result);                //Insert correct and three fake answers in answers list
                            tempAnswers.Add(result + 1);
                            tempAnswers.Add(result - 1);
                            tempAnswers.Add(result + 2);
                        }
                        GenerateQuestion(left, right, operationID, operatorDecider);
                        break;

                    case 1:
                        equationOperator = operators[1];
                        quation = "" + startingNumber + equationOperator + firstNumber + "=" + secondNumber;


                        right = firstNumber;//Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        operationText = equationOperator;                     //Operation string assignment
                        result = left - right;                  //Calculating the result of expression	

                        tempAnswers.Add(result);                //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + 1);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(result + 2);
                        GenerateQuestion(left, right, operationID, operatorDecider);
                        break;

                    default:
                        break;
                }
                break;
            case 3:                                     //Operation ×

                operatorDecider = (int)Random.Range(0, 2);  // random number generation to decide operator/ Type of problem
                quation = "";
                startingNumber = factorsOf100[(int)Random.Range(0, 8)];
                firstNumber = (int)Random.Range(0, 10);
                secondNumber = startingNumber - firstNumber;
                switch (operatorDecider) // based on result of operatorDecider generates either addition or subtraction problem
                {

                    case 0:
                        equationOperator = operators[0];
                        quation = "" + startingNumber + equationOperator + firstNumber + "=" + secondNumber;


                        right = firstNumber;//Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        operationText = equationOperator;                     //Operation string assignment
                        result = left + right;                  //Calculating the result of expression	

                        tempAnswers.Add(result);                //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + 1);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(result + 2);
                        GenerateQuestion(left, right, operationID, operatorDecider);
                        break;

                    case 1:
                        equationOperator = operators[1];
                        while (startingNumber - firstNumber < 0)
                        {
                            firstNumber = (int)Random.Range(0, 10);
                            Debug.Log("change a number");
                        }
                        secondNumber = startingNumber * firstNumber;
                        quation = "" + secondNumber + equationOperator + firstNumber + "=" + startingNumber;


                        right = firstNumber;//Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        operationText = equationOperator;                     //Operation string assignment
                        result = left - right;                  //Calculating the result of expression	

                        tempAnswers.Add(result);                //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + 1);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(result + 2);
                        GenerateQuestion(left, right, operationID, operatorDecider);
                        break;

                    default:
                        break;
                }
                break;
            case 4:                                     //Operation ÷
                /*                                        //Here we generate left and right inegers with special logic so that result will be always whole number (not float)
                left = Random.Range(1, 3 + (currentLevel - 1) * 2);
                right = Random.Range(1, 3 + (currentLevel - 1) * 2);
                */
                operatorDecider = (int)Random.Range(0, 3);  // random number generation to decide operator/ Type of problem
                quation = "";
                startingNumber = factorsOf100[(int)Random.Range(0, 8)];
                firstNumber = (int)Random.Range(0, startingNumber);
                secondNumber = startingNumber - firstNumber;
                switch (operatorDecider
                ) // based on result of operatorDecider generates either addition or subtraction problem
                {
                    case 0:
                        equationOperator = operators[0];
                        secondNumber = startingNumber + firstNumber;
                        quation = "" + secondNumber + equationOperator + firstNumber + "=" + startingNumber;


                        right = firstNumber;//Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        operationText = equationOperator;                     //Operation string assignment
                        result = left + right;                  //Calculating the result of expression	

                        tempAnswers.Add(result);                //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + 1);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(result + 2);
                        GenerateQuestion(left, right, operationID, operatorDecider);
                        break;
                    case 1:
                        equationOperator = operators[1];
                        while (startingNumber - firstNumber < 0)
                        {
                            firstNumber = (int)Random.Range(0, 10);
                            Debug.Log("change a number");
                        }
                        quation = "" + startingNumber + equationOperator + firstNumber + "=" + secondNumber;


                        right = firstNumber;//Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        operationText = equationOperator;                     //Operation string assignment
                        result = left - right;                  //Calculating the result of expression	

                        tempAnswers.Add(result);                //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + 1);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(result + 2);
                        GenerateQuestion(left, right, operationID, operatorDecider);
                        break;

                    case 2:
                        equationOperator = operators[2];

                        startingNumber = (int)Random.Range(1, 10);
                        firstNumber = (int)Random.Range(1, 13);
                        secondNumber = startingNumber * firstNumber;
                        quation = "" + secondNumber + equationOperator + firstNumber + "=" + startingNumber;


                        right = firstNumber; //Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        operationText = equationOperator; //Operation string assignment
                        result = left * right; //Calculating the result of expression	

                        tempAnswers.Add(result); //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + 1);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(result + 2);
                        GenerateQuestion(left, right, operationID, operatorDecider);
                        break;

                    default:
                        break;
                }
                break;
            case 5:                                     //Operation ×
                //right = Random.Range(1, 13);
                operatorDecider = (int)Random.Range(0, 4);  // random number generation to decide operator/ Type of problem
                quation = "";

                switch (operatorDecider
                ) // based on result of operatorDecider generates either addition or subtraction problem
                {
                    case 0:
                        equationOperator = operators[0];
                        startingNumber = factorsOf100[(int)Random.Range(0, 8)];
                        firstNumber = (int)Random.Range(0, startingNumber);
                        secondNumber = startingNumber + firstNumber;
                        quation = "" + secondNumber + equationOperator + firstNumber + "=" + startingNumber;


                        right = firstNumber;//Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        operationText = equationOperator;                     //Operation string assignment
                        result = left + right;                  //Calculating the result of expression	

                        tempAnswers.Add(result);                //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + 1);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(result + 2);
                        GenerateQuestion(left, right, operationID, operatorDecider);
                        break;
                    case 1:
                        equationOperator = operators[1];
                        startingNumber = factorsOf100[(int)Random.Range(0, 8)];
                        firstNumber = (int)Random.Range(0, startingNumber);
                        while (startingNumber - firstNumber < 0)
                        {
                            firstNumber = (int)Random.Range(0, 10);
                            Debug.Log("change a number");
                        }
                        quation = "" + startingNumber + equationOperator + firstNumber + "=" + secondNumber;


                        right = firstNumber;//Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        operationText = equationOperator;                     //Operation string assignment
                        result = left - right;                  //Calculating the result of expression	

                        tempAnswers.Add(result);                //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + 1);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(result + 2);
                        GenerateQuestion(left, right, operationID, operatorDecider);
                        break;
                    case 2:
                        equationOperator = operators[2];

                        startingNumber = factorsOf100[(int)Random.Range(0, factorsOf100.Length)];
                        firstNumber = (int)Random.Range(1, 13);
                        secondNumber = startingNumber * firstNumber;
                        quation = "" + secondNumber + equationOperator + firstNumber + "=" + startingNumber;


                        right = firstNumber; //Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        operationText = equationOperator; //Operation string assignment
                        result = left * right; //Calculating the result of expression	

                        tempAnswers.Add(result); //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + 1);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(result + 2);
                        GenerateQuestion(left, right, operationID, operatorDecider);
                        break;

                    case 3:
                        equationOperator = operators[3];
                        startingNumber = MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                        firstNumber = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)];
                        secondNumber = firstNumber / startingNumber;

                        right = firstNumber; //Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        result = left / right; //Calculating the result of expression
                        operationText = equationOperator; //Operation string assignment                            
                        tempAnswers.Add(result); //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + 1);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(result + 2);
                        GenerateQuestion(left, right, operationID, operatorDecider);
                        break;
                    default:
                        break;
                }
                break;
            case 6:                                     //Operation ×
                //right = Random.Range(1, 13);
                operatorDecider = (int)Random.Range(0, 5);  // random number generation to decide operator/ Type of problem
                quation = "";
                switch (operatorDecider) // based on result of operatorDecider generates either addition or subtraction problem
                {
                    case 0:
                        equationOperator = operators[0];
                        startingNumber = factorsOf100[(int)Random.Range(0, 8)];
                        firstNumber = (int)Random.Range(0, startingNumber);
                        secondNumber = startingNumber + firstNumber;
                        quation = "" + secondNumber + equationOperator + firstNumber + "=" + startingNumber;


                        right = firstNumber;//Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        operationText = equationOperator;                     //Operation string assignment
                        result = left + right;                  //Calculating the result of expression	

                        tempAnswers.Add(result);                //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + 1);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(result + 2);
                        GenerateQuestion(left, right, operationID, operatorDecider);
                        break;
                    case 1:
                        equationOperator = operators[1];
                        startingNumber = factorsOf100[(int)Random.Range(0, 8)];
                        firstNumber = (int)Random.Range(0, startingNumber);
                        while (startingNumber - firstNumber < 0)
                        {
                            firstNumber = (int)Random.Range(0, 10);
                            Debug.Log("change a number");
                        }
                        quation = "" + startingNumber + equationOperator + firstNumber + "=" + secondNumber;


                        right = firstNumber;//Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        operationText = equationOperator;                     //Operation string assignment
                        result = left - right;                  //Calculating the result of expression	

                        tempAnswers.Add(result);                //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + 1);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(result + 2);
                        GenerateQuestion(left, right, operationID, operatorDecider);
                        break;
                    case 2:
                        equationOperator = operators[2];

                        startingNumber = factorsOf100[(int)Random.Range(0, factorsOf100.Length)];
                        firstNumber = (int)Random.Range(1, 13);
                        secondNumber = startingNumber * firstNumber;
                        quation = "" + secondNumber + equationOperator + firstNumber + "=" + startingNumber;


                        right = firstNumber; //Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        operationText = equationOperator; //Operation string assignment
                        result = left * right; //Calculating the result of expression	

                        tempAnswers.Add(result); //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + 1);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(result + 2);
                        GenerateQuestion(left, right, operationID, operatorDecider);
                        break;
                    case 3:
                        equationOperator = operators[3];
                        float wholeNumber;
                        startingNumber = factorsOf900[(int)Random.Range(0, factorsOf900.Length)];
                        firstNumber = (int)Random.Range(1, 13);
                        //float startN;
                        //float secondN;

                        wholeNumber = (float)startingNumber / (float)firstNumber;
                        while (wholeNumber % 1 != 0)
                        {
                            Debug.Log("not whole number");
                            firstNumber = (int)Random.Range(1, 13);
                            wholeNumber = (float)startingNumber / (float)firstNumber;
                        }
                        //secondNumber = startingNumber * firstNumber;
                        //quation = "" + secondNumber + equationOperator + firstNumber + "=" + startingNumber;


                        right = firstNumber; //Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        operationText = equationOperator; //Operation string assignment
                        result = left / right; //Calculating the result of expression	

                        tempAnswers.Add(result); //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + 1);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(result + 2);
                        GenerateQuestion(left, right, operationID, operatorDecider);
                        break;

                    case 4:
                        equationOperator = operators[3];
                        startingNumber = factorsOf1442[(int)Random.Range(0, factorsOf1442.Length)];
                        firstNumber = factorsOf1441[(int)Random.Range(0, factorsOf1441.Length)];
                        secondNumber = firstNumber / startingNumber;

                        left = startingNumber; //Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        right = firstNumber;
                        result = left / right; //Calculating the result of expression
                        operationText = equationOperator; //Operation string assignment                            
                        tempAnswers.Add(result); //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + 1);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(result + 2);
                        GenerateQuestion(left, right, operationID, operatorDecider);
                        break;
                    default:
                        break;
                }
                break;
        }



        trueID = Random.Range(0, 4);						//Generating trueID (for random position)
        answers[trueID].text = result.ToString();       //Correct answer as string goes to answers list with generated index
        answers[trueID].fontSize = GetFontSize(answers[trueID].text); //Setting suitable font
        tempAnswers.RemoveAt(0);                        //Remove correct answer, it is always with index 0 coz we insert it first
        int i = 0;                                      //Declaring counter variable
        foreach (Text answer in answers)
        {               //Here we go through answers list and insert fake answers that are randomly picked from tempAnswers
            if (i != trueID)
            {                           //Skip the element that is already filled with correct answer
                int rand = Random.Range(0, tempAnswers.Count);
                answer.text = tempAnswers[rand].ToString();
                answer.fontSize = GetFontSize(answer.text);//Setting suitable font
                tempAnswers.RemoveAt(rand);
            }
            i++;
        }

    }

    public void GenerateQuestion(float left, float right, int operationId, int operatorDecider)
    {
        switch (operationId)
        {
            case 1:
                rightID = Random.Range(0, 4);
                equations[rightID].text = left.ToString() + equationOperator + right.ToString();
                equations[rightID].fontSize = GetFontSize(equations[trueID].text);
                int a0 = 0;
                foreach (Text equation in equations)
                {               //Here we go through answers list and insert fake answers that are randomly picked from tempAnswers
                    if (a0 != rightID)
                    {                           //Skip the element that is already filled with correct answer
                        int rand = Random.Range(0, 4);
                        float answer = 0;
                        string op = equationOperator;
                        //float left1 = left + Random.Range(x + 1, x + 2);
                        //float right1 = right + Random.Range(x + 1, x + 2);
                        float left1 = Random.Range(0, 6);
                        float right1 = Random.Range(0, 6);
                        //Debug.Log("tempAnswers[x]"+ tempAnswers[x].ToString());
                        switch (equationOperator)
                        {
                            case "+":
                                answer = left1 + right1;
                                break;
                            case "-":
                                answer = left1 - right1;
                                break;
                            case "*":
                                answer = left1 * right1;
                                break;
                            case "/":
                                //answer = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)] / MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                                answer = left / right;
                                break;

                        }
                        while (answer == tempAnswers[0] || answer == tempAnswers[1] || answer == tempAnswers[2] || answer == tempAnswers[3])
                        {
                            left1 = Random.Range(0, 6);
                            right1 = Random.Range(0, 6);
                            op = equationOperator;
                            answer = left1 + right1;
                            Debug.Log("regenerate");
                        }
                        equation.text = left1.ToString() + op + right1.ToString();
                        equation.fontSize = GetFontSize(equation.text);//Setting suitable font
                                                                       //tempAnswers.RemoveAt(rand);
                    }
                    a0++;
                }
                break;
            case 2:
                switch (operatorDecider)
                {
                    case 0:
                        rightID = Random.Range(0, 4);
                        equations[rightID].text = left.ToString() + equationOperator + right.ToString();
                        equations[rightID].fontSize = GetFontSize(equations[trueID].text);
                        int a1 = 0;
                        foreach (Text equation in equations)
                        {               //Here we go through answers list and insert fake answers that are randomly picked from tempAnswers
                            if (a1 != rightID)
                            {                           //Skip the element that is already filled with correct answer
                                int rand = Random.Range(0, 4);
                                float answer = 0;
                                string op = equationOperator;
                                //float left1 = left + Random.Range(x + 1, x + 2);
                                //float right1 = right + Random.Range(x + 1, x + 2);
                                float left1 = Random.Range(0, 11);
                                float right1 = Random.Range(0, 11);
                                //Debug.Log("tempAnswers[x]"+ tempAnswers[x].ToString());
                                switch (equationOperator)
                                {
                                    case "+":
                                        answer = left1 + right1;
                                        break;
                                    case "-":
                                        answer = left1 - right1;
                                        break;
                                    case "*":
                                        answer = left1 * right1;
                                        break;
                                    case "/":
                                        //answer = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)] / MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                                        answer = left / right;
                                        break;

                                }
                                while (answer == tempAnswers[0] || answer == tempAnswers[1] || answer == tempAnswers[2] || answer == tempAnswers[3])
                                {
                                    left1 = Random.Range(0, 11);
                                    right1 = Random.Range(0, 11);
                                    op = equationOperator;
                                    answer = left1 + right1;
                                    Debug.Log("regenerate");
                                }
                                equation.text = left1.ToString() + op + right1.ToString();
                                equation.fontSize = GetFontSize(equation.text);//Setting suitable font
                                                                               //tempAnswers.RemoveAt(rand);
                            }
                            a1++;
                        }
                        break;
                    case 1:
                        rightID = Random.Range(0, 4);
                        equations[rightID].text = left.ToString() + equationOperator + right.ToString();
                        equations[rightID].fontSize = GetFontSize(equations[trueID].text);
                        int a2 = 0;
                        foreach (Text equation in equations)
                        {               //Here we go through answers list and insert fake answers that are randomly picked from tempAnswers
                            if (a2 != rightID)
                            {                           //Skip the element that is already filled with correct answer
                                int rand = Random.Range(0, 4);
                                float answer = 0;
                                string op = equationOperator;
                                //float left1 = left + Random.Range(x + 1, x + 2);
                                //float right1 = right + Random.Range(x + 1, x + 2);
                                float left1 = Random.Range(0, 21);
                                float right1 = Random.Range(0, 21);
                                //Debug.Log("tempAnswers[x]"+ tempAnswers[x].ToString());
                                switch (equationOperator)
                                {
                                    case "+":
                                        answer = left1 + right1;
                                        break;
                                    case "-":
                                        answer = left1 - right1;
                                        break;
                                    case "*":
                                        answer = left1 * right1;
                                        break;
                                    case "/":
                                        //answer = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)] / MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                                        answer = left / right;
                                        break;

                                }
                                while (answer == tempAnswers[0] || answer == tempAnswers[1] || answer == tempAnswers[2] || answer == tempAnswers[3])
                                {
                                    left1 = Random.Range(0, 21);
                                    right1 = Random.Range(0, 21);
                                    op = equationOperator;
                                    answer = left1 - right1;
                                    Debug.Log("regenerate");
                                }
                                equation.text = left1.ToString() + op + right1.ToString();
                                equation.fontSize = GetFontSize(equation.text);//Setting suitable font
                                                                               //tempAnswers.RemoveAt(rand);
                            }
                            a2++;
                        }
                        break;
                }
                break;
            case 3:
                switch (operatorDecider)
                {
                    case 0:
                        rightID = Random.Range(0, 4);
                        equations[rightID].text = left.ToString() + equationOperator + right.ToString();
                        equations[rightID].fontSize = GetFontSize(equations[trueID].text);
                        int a1 = 0;
                        foreach (Text equation in equations)
                        {               //Here we go through answers list and insert fake answers that are randomly picked from tempAnswers
                            if (a1 != rightID)
                            {                           //Skip the element that is already filled with correct answer
                                int rand = Random.Range(0, 4);
                                float answer = 0;
                                string op = equationOperator;
                                //float left1 = left + Random.Range(x + 1, x + 2);
                                //float right1 = right + Random.Range(x + 1, x + 2);
                                float left1 = factorsOf100[(int)Random.Range(0, 8)];
                                float right1 = Random.Range(0, 10);
                                //Debug.Log("tempAnswers[x]"+ tempAnswers[x].ToString());
                                switch (equationOperator)
                                {
                                    case "+":
                                        answer = left1 + right1;
                                        break;
                                    case "-":
                                        answer = left1 - right1;
                                        break;
                                    case "*":
                                        answer = left1 * right1;
                                        break;
                                    case "/":
                                        //answer = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)] / MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                                        answer = left / right;
                                        break;

                                }
                                while (answer == tempAnswers[0] || answer == tempAnswers[1] || answer == tempAnswers[2] || answer == tempAnswers[3])
                                {
                                    left1 = factorsOf100[(int)Random.Range(0, 8)];
                                    right1 = Random.Range(0, 10);
                                    op = equationOperator;
                                    answer = left1 + right1;
                                    Debug.Log("regenerate");
                                }
                                equation.text = left1.ToString() + op + right1.ToString();
                                equation.fontSize = GetFontSize(equation.text);//Setting suitable font
                                                                               //tempAnswers.RemoveAt(rand);
                            }
                            a1++;
                        }
                        break;
                    case 1:
                        rightID = Random.Range(0, 4);
                        equations[rightID].text = left.ToString() + equationOperator + right.ToString();
                        equations[rightID].fontSize = GetFontSize(equations[trueID].text);
                        int a2 = 0;
                        foreach (Text equation in equations)
                        {               //Here we go through answers list and insert fake answers that are randomly picked from tempAnswers
                            if (a2 != rightID)
                            {                           //Skip the element that is already filled with correct answer
                                int rand = Random.Range(0, 4);
                                float answer = 0;
                                string op = equationOperator;
                                //float left1 = left + Random.Range(x + 1, x + 2);
                                //float right1 = right + Random.Range(x + 1, x + 2);
                                float left1 = factorsOf100[(int)Random.Range(0, 8)];
                                float right1 = Random.Range(0, 10);
                                //Debug.Log("tempAnswers[x]"+ tempAnswers[x].ToString());
                                switch (equationOperator)
                                {
                                    case "+":
                                        answer = left1 + right1;
                                        break;
                                    case "-":
                                        answer = left1 - right1;
                                        break;
                                    case "*":
                                        answer = left1 * right1;
                                        break;
                                    case "/":
                                        //answer = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)] / MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                                        answer = left / right;
                                        break;

                                }
                                while (answer == tempAnswers[0] || answer == tempAnswers[1] || answer == tempAnswers[2] || answer == tempAnswers[3])
                                {
                                    left1 = factorsOf100[(int)Random.Range(0, 8)];
                                    right1 = Random.Range(0, 10);
                                    op = equationOperator;
                                    answer = left1 - right1;
                                    Debug.Log("regenerate");
                                }
                                equation.text = left1.ToString() + op + right1.ToString();
                                equation.fontSize = GetFontSize(equation.text);//Setting suitable font
                                                                               //tempAnswers.RemoveAt(rand);
                            }
                            a2++;
                        }
                        break;
                }
                break;
            case 4:
                switch (operatorDecider)
                {
                    case 0:
                        rightID = Random.Range(0, 4);
                        equations[rightID].text = left.ToString() + equationOperator + right.ToString();
                        equations[rightID].fontSize = GetFontSize(equations[trueID].text);
                        int a1 = 0;
                        foreach (Text equation in equations)
                        {               //Here we go through answers list and insert fake answers that are randomly picked from tempAnswers
                            if (a1 != rightID)
                            {                           //Skip the element that is already filled with correct answer
                                int rand = Random.Range(0, 4);
                                float answer = 0;
                                string op = equationOperator;
                                //float left1 = left + Random.Range(x + 1, x + 2);
                                //float right1 = right + Random.Range(x + 1, x + 2);
                                float left1 = factorsOf100[(int)Random.Range(0, 8)];
                                float right1 = Random.Range(0, 10);
                                //Debug.Log("tempAnswers[x]"+ tempAnswers[x].ToString());
                                switch (equationOperator)
                                {
                                    case "+":
                                        answer = left1 + right1;
                                        break;
                                    case "-":
                                        answer = left1 - right1;
                                        break;
                                    case "*":
                                        answer = left1 * right1;
                                        break;
                                    case "/":
                                        //answer = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)] / MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                                        answer = left / right;
                                        break;

                                }
                                while (answer == tempAnswers[0] || answer == tempAnswers[1] || answer == tempAnswers[2] || answer == tempAnswers[3])
                                {
                                    left1 = factorsOf100[(int)Random.Range(0, 8)];
                                    right1 = Random.Range(0, 10);
                                    op = equationOperator;
                                    answer = left1 + right1;
                                    Debug.Log("regenerate");
                                }
                                equation.text = left1.ToString() + op + right1.ToString();
                                equation.fontSize = GetFontSize(equation.text);//Setting suitable font
                                                                               //tempAnswers.RemoveAt(rand);
                            }
                            a1++;
                        }
                        break;
                    case 1:
                        rightID = Random.Range(0, 4);
                        equations[rightID].text = left.ToString() + equationOperator + right.ToString();
                        equations[rightID].fontSize = GetFontSize(equations[trueID].text);
                        int a2 = 0;
                        foreach (Text equation in equations)
                        {               //Here we go through answers list and insert fake answers that are randomly picked from tempAnswers
                            if (a2 != rightID)
                            {                           //Skip the element that is already filled with correct answer
                                int rand = Random.Range(0, 4);
                                float answer = 0;
                                string op = equationOperator;
                                //float left1 = left + Random.Range(x + 1, x + 2);
                                //float right1 = right + Random.Range(x + 1, x + 2);
                                float left1 = factorsOf100[(int)Random.Range(0, 8)];
                                float right1 = Random.Range(0, 10);
                                //Debug.Log("tempAnswers[x]"+ tempAnswers[x].ToString());
                                switch (equationOperator)
                                {
                                    case "+":
                                        answer = left1 + right1;
                                        break;
                                    case "-":
                                        answer = left1 - right1;
                                        break;
                                    case "*":
                                        answer = left1 * right1;
                                        break;
                                    case "/":
                                        //answer = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)] / MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                                        answer = left / right;
                                        break;

                                }
                                while (answer == tempAnswers[0] || answer == tempAnswers[1] || answer == tempAnswers[2] || answer == tempAnswers[3])
                                {
                                    left1 = factorsOf100[(int)Random.Range(0, 8)];
                                    right1 = Random.Range(0, 10);
                                    op = equationOperator;
                                    answer = left1 - right1;
                                    Debug.Log("regenerate");
                                }
                                equation.text = left1.ToString() + op + right1.ToString();
                                equation.fontSize = GetFontSize(equation.text);//Setting suitable font
                                                                               //tempAnswers.RemoveAt(rand);
                            }
                            a2++;
                        }
                        break;
                    case 2:
                        rightID = Random.Range(0, 4);
                        equations[rightID].text = left.ToString() + equationOperator + right.ToString();
                        equations[rightID].fontSize = GetFontSize(equations[trueID].text);
                        int a3 = 0;
                        foreach (Text equation in equations)
                        {               //Here we go through answers list and insert fake answers that are randomly picked from tempAnswers
                            if (a3 != rightID)
                            {                           //Skip the element that is already filled with correct answer
                                int rand = Random.Range(0, 4);
                                float answer = 0;
                                string op = equationOperator;
                                float left1 = Random.Range(0, 10);
                                float right1 = Random.Range(0, 13);
                                //Debug.Log("tempAnswers[x]"+ tempAnswers[x].ToString());
                                switch (equationOperator)
                                {
                                    case "+":
                                        answer = left1 + right1;
                                        break;
                                    case "-":
                                        answer = left1 - right1;
                                        break;
                                    case "*":
                                        answer = left1 * right1;
                                        break;
                                    case "/":
                                        //answer = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)] / MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                                        answer = left / right;
                                        break;

                                }
                                while (answer == tempAnswers[0] || answer == tempAnswers[1] || answer == tempAnswers[2] || answer == tempAnswers[3])
                                {
                                    left1 = Random.Range(0, 10);
                                    right1 = Random.Range(0, 13);
                                    op = equationOperator;
                                    answer = left1 * right1;
                                    Debug.Log("regenerate");
                                }
                                equation.text = left1.ToString() + op + right1.ToString();
                                equation.fontSize = GetFontSize(equation.text);//Setting suitable font
                                                                               //tempAnswers.RemoveAt(rand);
                            }
                            a3++;
                        }
                        break;
                }
                break;
            case 5:
                switch (operatorDecider)
                {
                    case 0:
                        rightID = Random.Range(0, 4);
                        equations[rightID].text = left.ToString() + equationOperator + right.ToString();
                        equations[rightID].fontSize = GetFontSize(equations[trueID].text);
                        int a1 = 0;
                        foreach (Text equation in equations)
                        {               //Here we go through answers list and insert fake answers that are randomly picked from tempAnswers
                            if (a1 != rightID)
                            {                           //Skip the element that is already filled with correct answer
                                int rand = Random.Range(0, 4);
                                float answer = 0;
                                string op = equationOperator;
                                //float left1 = left + Random.Range(x + 1, x + 2);
                                //float right1 = right + Random.Range(x + 1, x + 2);
                                float left1 = factorsOf100[(int)Random.Range(0, 8)];
                                float right1 = Random.Range(0, 10);
                                //Debug.Log("tempAnswers[x]"+ tempAnswers[x].ToString());
                                switch (equationOperator)
                                {
                                    case "+":
                                        answer = left1 + right1;
                                        break;
                                    case "-":
                                        answer = left1 - right1;
                                        break;
                                    case "*":
                                        answer = left1 * right1;
                                        break;
                                    case "/":
                                        //answer = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)] / MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                                        answer = left / right;
                                        break;

                                }
                                while (answer == tempAnswers[0] || answer == tempAnswers[1] || answer == tempAnswers[2] || answer == tempAnswers[3])
                                {
                                    left1 = factorsOf100[(int)Random.Range(0, 8)];
                                    right1 = Random.Range(0, 10);
                                    op = equationOperator;
                                    answer = left1 + right1;
                                    Debug.Log("regenerate");
                                }
                                equation.text = left1.ToString() + op + right1.ToString();
                                equation.fontSize = GetFontSize(equation.text);//Setting suitable font
                                                                               //tempAnswers.RemoveAt(rand);
                            }
                            a1++;
                        }
                        break;
                    case 1:
                        rightID = Random.Range(0, 4);
                        equations[rightID].text = left.ToString() + equationOperator + right.ToString();
                        equations[rightID].fontSize = GetFontSize(equations[trueID].text);
                        int a2 = 0;
                        foreach (Text equation in equations)
                        {               //Here we go through answers list and insert fake answers that are randomly picked from tempAnswers
                            if (a2 != rightID)
                            {                           //Skip the element that is already filled with correct answer
                                int rand = Random.Range(0, 4);
                                float answer = 0;
                                string op = equationOperator;
                                //float left1 = left + Random.Range(x + 1, x + 2);
                                //float right1 = right + Random.Range(x + 1, x + 2);
                                float left1 = factorsOf100[(int)Random.Range(0, 8)];
                                float right1 = Random.Range(0, 10);
                                //Debug.Log("tempAnswers[x]"+ tempAnswers[x].ToString());
                                switch (equationOperator)
                                {
                                    case "+":
                                        answer = left1 + right1;
                                        break;
                                    case "-":
                                        answer = left1 - right1;
                                        break;
                                    case "*":
                                        answer = left1 * right1;
                                        break;
                                    case "/":
                                        //answer = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)] / MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                                        answer = left / right;
                                        break;

                                }
                                while (answer == tempAnswers[0] || answer == tempAnswers[1] || answer == tempAnswers[2] || answer == tempAnswers[3])
                                {
                                    left1 = factorsOf100[(int)Random.Range(0, 8)];
                                    right1 = Random.Range(0, 10);
                                    op = equationOperator;
                                    answer = left1 - right1;
                                    Debug.Log("regenerate");
                                }
                                equation.text = left1.ToString() + op + right1.ToString();
                                equation.fontSize = GetFontSize(equation.text);//Setting suitable font
                                                                               //tempAnswers.RemoveAt(rand);
                            }
                            a2++;
                        }
                        break;
                    case 2:
                        rightID = Random.Range(0, 4);
                        equations[rightID].text = left.ToString() + equationOperator + right.ToString();
                        equations[rightID].fontSize = GetFontSize(equations[trueID].text);
                        int a3 = 0;
                        foreach (Text equation in equations)
                        {               //Here we go through answers list and insert fake answers that are randomly picked from tempAnswers
                            if (a3 != rightID)
                            {                           //Skip the element that is already filled with correct answer
                                int rand = Random.Range(0, 4);
                                float answer = 0;
                                string op = equationOperator;
                                float left1 = factorsOf100[(int)Random.Range(0, factorsOf100.Length)];
                                float right1 = Random.Range(0, 13);
                                //Debug.Log("tempAnswers[x]"+ tempAnswers[x].ToString());
                                switch (equationOperator)
                                {
                                    case "+":
                                        answer = left1 + right1;
                                        break;
                                    case "-":
                                        answer = left1 - right1;
                                        break;
                                    case "*":
                                        answer = left1 * right1;
                                        break;
                                    case "/":
                                        //answer = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)] / MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                                        answer = left / right;
                                        break;

                                }
                                while (answer == tempAnswers[0] || answer == tempAnswers[1] || answer == tempAnswers[2] || answer == tempAnswers[3])
                                {
                                    left1 = factorsOf100[(int)Random.Range(0, factorsOf100.Length)];
                                    right1 = Random.Range(0, 13);
                                    op = equationOperator;
                                    answer = left1 * right1;
                                    Debug.Log("regenerate");
                                }
                                equation.text = left1.ToString() + op + right1.ToString();
                                equation.fontSize = GetFontSize(equation.text);//Setting suitable font
                                                                               //tempAnswers.RemoveAt(rand);
                            }
                            a3++;
                        }
                        break;
                    case 3:
                        rightID = Random.Range(0, 4);
                        equations[rightID].text = left.ToString() + equationOperator + right.ToString();
                        equations[rightID].fontSize = GetFontSize(equations[trueID].text);
                        int a4 = 0;
                        foreach (Text equation in equations)
                        {               //Here we go through answers list and insert fake answers that are randomly picked from tempAnswers
                            if (a4 != rightID)
                            {                           //Skip the element that is already filled with correct answer
                                int rand = Random.Range(0, 4);
                                float answer = 0;
                                string op = equationOperator;
                                float right1 = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)];
                                float left1 = MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                                //Debug.Log("tempAnswers[x]"+ tempAnswers[x].ToString());
                                switch (equationOperator)
                                {
                                    case "+":
                                        answer = left1 + right1;
                                        break;
                                    case "-":
                                        answer = left1 - right1;
                                        break;
                                    case "*":
                                        answer = left1 * right1;
                                        break;
                                    case "/":
                                        //answer = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)] / MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                                        answer = left / right;
                                        break;

                                }
                                while (answer == tempAnswers[0] || answer == tempAnswers[1] || answer == tempAnswers[2] || answer == tempAnswers[3])
                                {
                                    right1 = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)];
                                    left1 = MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                                    op = equationOperator;
                                    answer = left1 / right1;
                                    Debug.Log("regenerate");
                                }
                                equation.text = left1.ToString() + op + right1.ToString();
                                equation.fontSize = GetFontSize(equation.text);//Setting suitable font
                                                                               //tempAnswers.RemoveAt(rand);
                            }
                            a4++;
                        }
                        break;
                }

                break;
            case 6:
                switch (operatorDecider)
                {
                    case 0:
                        rightID = Random.Range(0, 4);
                        equations[rightID].text = left.ToString() + equationOperator + right.ToString();
                        equations[rightID].fontSize = GetFontSize(equations[trueID].text);
                        int a1 = 0;
                        foreach (Text equation in equations)
                        {               //Here we go through answers list and insert fake answers that are randomly picked from tempAnswers
                            if (a1 != rightID)
                            {                           //Skip the element that is already filled with correct answer
                                int rand = Random.Range(0, 4);
                                float answer = 0;
                                string op = equationOperator;
                                //float left1 = left + Random.Range(x + 1, x + 2);
                                //float right1 = right + Random.Range(x + 1, x + 2);
                                float left1 = factorsOf100[(int)Random.Range(0, 8)];
                                float right1 = Random.Range(0, 10);
                                //Debug.Log("tempAnswers[x]"+ tempAnswers[x].ToString());
                                switch (equationOperator)
                                {
                                    case "+":
                                        answer = left1 + right1;
                                        break;
                                    case "-":
                                        answer = left1 - right1;
                                        break;
                                    case "*":
                                        answer = left1 * right1;
                                        break;
                                    case "/":
                                        //answer = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)] / MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                                        answer = left / right;
                                        break;

                                }
                                while (answer == tempAnswers[0] || answer == tempAnswers[1] || answer == tempAnswers[2] || answer == tempAnswers[3])
                                {
                                    left1 = factorsOf100[(int)Random.Range(0, 8)];
                                    right1 = Random.Range(0, 10);
                                    op = equationOperator;
                                    answer = left1 + right1;
                                    Debug.Log("regenerate");
                                }
                                equation.text = left1.ToString() + op + right1.ToString();
                                equation.fontSize = GetFontSize(equation.text);//Setting suitable font
                                                                               //tempAnswers.RemoveAt(rand);
                            }
                            a1++;
                        }
                        break;
                    case 1:
                        rightID = Random.Range(0, 4);
                        equations[rightID].text = left.ToString() + equationOperator + right.ToString();
                        equations[rightID].fontSize = GetFontSize(equations[trueID].text);
                        int a2 = 0;
                        foreach (Text equation in equations)
                        {               //Here we go through answers list and insert fake answers that are randomly picked from tempAnswers
                            if (a2 != rightID)
                            {                           //Skip the element that is already filled with correct answer
                                int rand = Random.Range(0, 4);
                                float answer = 0;
                                string op = equationOperator;
                                //float left1 = left + Random.Range(x + 1, x + 2);
                                //float right1 = right + Random.Range(x + 1, x + 2);
                                float left1 = factorsOf100[(int)Random.Range(0, 8)];
                                float right1 = Random.Range(0, 10);
                                //Debug.Log("tempAnswers[x]"+ tempAnswers[x].ToString());
                                switch (equationOperator)
                                {
                                    case "+":
                                        answer = left1 + right1;
                                        break;
                                    case "-":
                                        answer = left1 - right1;
                                        break;
                                    case "*":
                                        answer = left1 * right1;
                                        break;
                                    case "/":
                                        //answer = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)] / MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                                        answer = left / right;
                                        break;

                                }
                                while (answer == tempAnswers[0] || answer == tempAnswers[1] || answer == tempAnswers[2] || answer == tempAnswers[3])
                                {
                                    left1 = factorsOf100[(int)Random.Range(0, 8)];
                                    right1 = Random.Range(0, 10);
                                    op = equationOperator;
                                    answer = left1 - right1;
                                    Debug.Log("regenerate");
                                }
                                equation.text = left1.ToString() + op + right1.ToString();
                                equation.fontSize = GetFontSize(equation.text);//Setting suitable font
                                                                               //tempAnswers.RemoveAt(rand);
                            }
                            a2++;
                        }
                        break;
                    case 2:
                        rightID = Random.Range(0, 4);
                        equations[rightID].text = left.ToString() + equationOperator + right.ToString();
                        equations[rightID].fontSize = GetFontSize(equations[trueID].text);
                        int a3 = 0;
                        foreach (Text equation in equations)
                        {               //Here we go through answers list and insert fake answers that are randomly picked from tempAnswers
                            if (a3 != rightID)
                            {                           //Skip the element that is already filled with correct answer
                                int rand = Random.Range(0, 4);
                                float answer = 0;
                                string op = equationOperator;
                                float left1 = factorsOf100[(int)Random.Range(0, factorsOf100.Length)];
                                float right1 = Random.Range(0, 13);
                                //Debug.Log("tempAnswers[x]"+ tempAnswers[x].ToString());
                                switch (equationOperator)
                                {
                                    case "+":
                                        answer = left1 + right1;
                                        break;
                                    case "-":
                                        answer = left1 - right1;
                                        break;
                                    case "*":
                                        answer = left1 * right1;
                                        break;
                                    case "/":
                                        //answer = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)] / MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                                        answer = left / right;
                                        break;

                                }
                                while (answer == tempAnswers[0] || answer == tempAnswers[1] || answer == tempAnswers[2] || answer == tempAnswers[3])
                                {
                                    left1 = factorsOf100[(int)Random.Range(0, factorsOf100.Length)];
                                    right1 = Random.Range(0, 13);
                                    op = equationOperator;
                                    answer = left1 * right1;
                                    Debug.Log("regenerate");
                                }
                                equation.text = left1.ToString() + op + right1.ToString();
                                equation.fontSize = GetFontSize(equation.text);//Setting suitable font
                                                                               //tempAnswers.RemoveAt(rand);
                            }
                            a3++;
                        }
                        break;
                    case 3:
                        rightID = Random.Range(0, 4);
                        equations[rightID].text = left.ToString() + equationOperator + right.ToString();
                        equations[rightID].fontSize = GetFontSize(equations[trueID].text);
                        int a4 = 0;
                        foreach (Text equation in equations)
                        {               //Here we go through answers list and insert fake answers that are randomly picked from tempAnswers
                            if (a4 != rightID)
                            {                           //Skip the element that is already filled with correct answer
                                int rand = Random.Range(0, 4);
                                float answer = 0;
                                string op = equationOperator;
                                float right1 = factorsOf900[(int)Random.Range(0, factorsOf900.Length)];
                                float left1 = Random.Range(0, 13);
                                //Debug.Log("tempAnswers[x]"+ tempAnswers[x].ToString());
                                switch (equationOperator)
                                {
                                    case "+":
                                        answer = left1 + right1;
                                        break;
                                    case "-":
                                        answer = left1 - right1;
                                        break;
                                    case "*":
                                        answer = left1 * right1;
                                        break;
                                    case "/":
                                        //answer = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)] / MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                                        answer = left / right;
                                        break;

                                }
                                while (answer == tempAnswers[0] || answer == tempAnswers[1] || answer == tempAnswers[2] || answer == tempAnswers[3])
                                {
                                    right1 = factorsOf900[(int)Random.Range(0, factorsOf900.Length)];
                                    left1 = Random.Range(0, 13);
                                    op = equationOperator;
                                    answer = left1 / right1;
                                    Debug.Log("regenerate");
                                }
                                equation.text = left1.ToString() + op + right1.ToString();
                                equation.fontSize = GetFontSize(equation.text);//Setting suitable font
                                                                               //tempAnswers.RemoveAt(rand);
                            }
                            a4++;
                        }
                        break;
                    case 4:
                        rightID = Random.Range(0, 4);
                        equations[rightID].text = left.ToString() + equationOperator + right.ToString();
                        equations[rightID].fontSize = GetFontSize(equations[trueID].text);
                        int a5 = 0;
                        foreach (Text equation in equations)
                        {               //Here we go through answers list and insert fake answers that are randomly picked from tempAnswers
                            if (a5 != rightID)
                            {                           //Skip the element that is already filled with correct answer
                                int rand = Random.Range(0, 4);
                                float answer = 0;
                                string op = equationOperator;
                                float right1 = factorsOf1442[(int)Random.Range(0, factorsOf1442.Length)];
                                float left1 = factorsOf1441[(int)Random.Range(0, factorsOf1441.Length)];
                                //Debug.Log("tempAnswers[x]"+ tempAnswers[x].ToString());
                                switch (equationOperator)
                                {
                                    case "+":
                                        answer = left1 + right1;
                                        break;
                                    case "-":
                                        answer = left1 - right1;
                                        break;
                                    case "*":
                                        answer = left1 * right1;
                                        break;
                                    case "/":
                                        //answer = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)] / MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                                        answer = left / right;
                                        break;

                                }
                                while (answer == tempAnswers[0] || answer == tempAnswers[1] || answer == tempAnswers[2] || answer == tempAnswers[3])
                                {
                                    right1 = factorsOf1442[(int)Random.Range(0, factorsOf1442.Length)];
                                    left1 = factorsOf1441[(int)Random.Range(0, factorsOf1441.Length)];
                                    op = equationOperator;
                                    answer = left1 / right1;
                                    Debug.Log("regenerate");
                                }
                                equation.text = left1.ToString() + op + right1.ToString();
                                equation.fontSize = GetFontSize(equation.text);//Setting suitable font
                                                                               //tempAnswers.RemoveAt(rand);
                            }
                            a5++;
                        }
                        break;
                }
                break;
        }


    }
    public int GetFontSize(string answer)
    {   //Function that gets suitable font size for given string length
        int fontSize;
        /*
        switch (answer.Length)
        {
            case 1:
                fontSize = 210;
                break;
            case 2:
                fontSize = 200;
                break;
            case 3:
                fontSize = 140;
                break;
            case 4:
                fontSize = 100;
                break;
            default:
                fontSize = 75;
                break;
        }
        */
        switch (answer.Length)
        {
            case 1:
                fontSize = 150;
                break;
            case 2:
                fontSize = 100;
                break;
            case 3:
                fontSize = 70;
                break;
            case 4:
                fontSize = 50;
                break;
            default:
                fontSize = 35;
                break;
        }
        return fontSize;
    }



    public void HasChanged()
    {
        Judge();
    }

    IEnumerator WaitAndGo(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        answersGoUP.SetBool("NumberMove", false);
        Generate();
    }


    public void Judge()
    {
        List<int> x = new List<int>();
        int z = 1;
        string str = string.Empty;
        foreach (Transform slotTransform in slots)
        {
            GameObject item = slotTransform.GetComponent<ChutesSlot>().item;
            if (item)
            {
                x.Add(z);

                str = item.name;
                //wrongNumber = int.Parse(str);
            }
            else
            {
                x.Add(0);
            }

            z++;
        }

        int addup = 0;
        for (int i = 0; i < x.Count; i++)
        {
            addup += x[i];
        }

        wrongNumber = addup;
        if (rightID + 1 == addup)
        {
            if (trueID + 1 == int.Parse(str))
            {
                Invoke("isRight", 0.4f);
                AudioManager.instance.soundAudioSource2.clip = AudioManager.instance.soundClip[2];
                AudioManager.instance.soundAudioSource2.Play();
            }
            else
            {
                Invoke("isWrong", 0.4f);
                AudioManager.instance.soundAudioSource2.clip = AudioManager.instance.soundClip[2];
                AudioManager.instance.soundAudioSource2.Play();
            }
        }
        else
        {
            Invoke("isWrong", 0.3f);
            AudioManager.instance.soundAudioSource2.clip = AudioManager.instance.soundClip[2];
            AudioManager.instance.soundAudioSource2.Play();
        }
    }

    public void SetAnswerBoxRaytartTrue()
    {
        for (int i = 0; i < answers.Count; i++)
        {
            Debug.Log("change");
            answers[i].GetComponent<Text>().raycastTarget = true;
        }
    }
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}
