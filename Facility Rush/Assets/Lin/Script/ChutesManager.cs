using System;
using System.Collections;
//using System;
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
    //public DragHandler dh;

    public int currentLevel = 1;
    public List<Text> equations;

    public List<Text> answers;//4 answer variants texts
    //public List<Slot> answerSlots;
    public List<ChutesSlot> answerSlots;
    private int trueID;// ID of true answer that helps to place true answer at random position 
    //generate the right equation 
    private int rightID;
    private int score;
    public Text scoreValue;
    private List<float> tempAnswers = new List<float>();

    private string[] operators = new string[] { "+", "-", "*", "/" };   // an array to hold the possible operators used by the equation
    [SerializeField] private Transform slots;
    //[SerializeField] private Animation gobackAnimation;
    public GameObject playscene;
    public GameObject failscene;

    public Text bestScore;

    public Slider timeSlider;

    private float timer = 1;
    public bool game;
    //public Animation answersGoUP;
    public Animator answersGoUP;
    public Animator tubeOne;
    public Animator tubeTwo;
    public Animator tubeThree;
    public Animator tubeFour;

    private int wrongNumber;

    int startingNumber;  // randomly generated number for beginning of equation generation for addition and subtraction, unless it is mutiplication or division
    int secondNumber;   //  a randomly generated number raging from 1 to 100
    int firstNumber;    //  a randomly generated number ranging from 1 to 100
    string equationOperator; // a string character that represents the operator in the equation

    private int[] factorsOf100 = new int[] { 1, 2, 4, 5, 10, 25, 50, 100 };
    private int[] MultipleOfTwo = new int[] { 1, 2, 5, 10 };
    private int[] MultipleOfTwoBig = new int[] { 10, 30, 50, 100 };
    private int[] factorsOf144 = new int[] { 1, 2, 3, 4, 6, 8, 9, 12, 16, 18, 24, 36, 48, 72, 144 };
    private int[] factorsOf1441 = new int[] { 1, 2, 3, 4, 6};
    private int[] factorsOf1442 = new int[] {12, 18, 24, 36, 48, 72, 144 };
    private int[] factorsOf900 = new int[] { 1, 2, 3, 4, 5, 6, 9, 10, 12, 15, 18, 20, 25, 30, 36, 45, 50, 60, 75, 90, 100, 150, 180, 225, 300, 450, 900 };

    // Use this for initialization
    void Start ()
    {
        answersGoUP.SetBool("NumberBack", true);
        playscene.SetActive(true);
        failscene.SetActive(false);
        timeSlider.value = 1;
        game = true;
        //Debug.Log(operators[1]);
		Generate();
        //HasChanged();
        //currentLevel = 1;
    }
	
	// Update is called once per frame
	void Update () {
	    bestScore.text = score.ToString();
	    //timeSlider.value -= Time.deltaTime / 10;

	    if (game)
	    {
	        timeSlider.value -= Time.deltaTime / 20;
            if (timeSlider.value <= 0) //Run "GameOver" function if timeleft(slider value) is out
	        {
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
        failscene.SetActive(false);
        //playscene.SetActive(true);
    }
    public void CheckAnswer(int asnwerID)
    {               //This is on all 4 of answer buttons that send here answer ID pre-declared in Button component.
                     //Here we get time between answers to prevent fast chaos selecting answers (otherwise we can cheat scores)
        if (asnwerID == trueID )      //If answer ID is equal to true ID that means right answer pressed. Also we check if period is ok and we run according function.
            gotRight();
    }
    //DragHandler dh= new DragHandler();
    //MoveBack mb = new MoveBack();
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
        //answersGoUP.SetBool("NumberBack", false);
        //StartCoroutine("WaitAndGo", 0.5f);
        StartCoroutine("WaitAndRight", 0.5f);

    }

    public void gotRight()
    {                               //This runs when we pressed right answer 
        //answersGoUP.SetBool("NumberBack", true);
        answersGoUP.SetBool("NumberMove", false);
        
        tubeOne.SetBool("Correct1", false);
             
        tubeTwo.SetBool("Correct2", false);
  
        tubeThree.SetBool("Correct3", false);

        tubeFour.SetBool("Correct4", false);
        score += 1;
        //score += currentLevel * 10;                         //Increasing score value proportionally to current level (at fisrt level +1, at second +2 etc)
        scoreValue.text = score.ToString();
        //bestScore.text = score.ToString();
        //GameObject.Find("GameController").GetComponent<MoveBack>().StartPoint();
        
        //
        int y = trueID + 1;
        string x = y.ToString();
        GameObject.Find(x).GetComponent<ChutesDrag>().MoveBack();
        //GameObject.Find(x).GetComponent<DragHandler>().MoveBack();
        //GameObject.Find("2").GetComponent<DragHandler>().MoveBack();
        //GameObject.Find("3").GetComponent<DragHandler>().MoveBack();
        // GameObject.Find("4").GetComponent<DragHandler>().MoveBack();
        //Here is "level up" logic. We check current level then current score and compare it with level marks that you can set as wish. If need we run level up function
        // DragHandler
        //GameObject.Find("MoveBack").GetComponent<DragHandler>();
        //dh.MoveBack();
        // mb.GoBack(gameObject.transform.position);
        // gobackAnimation.Play();
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
        failscene.SetActive(true);
        game = false;
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
            //playscene.SetActive(false);
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
        else if(currentLevel == 4)                                      //If we are at 1st level then we use only + or -
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

                        tempAnswers.Add(result);                //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + 1);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(left - right);
                        break;
                }
                break;
            case 2:                                     //Operation -

                operatorDecider = (int)Random.Range(0, 2);  // random number generation to decide operator/ Type of problem
                quation = "";
                startingNumber = (int)Random.Range(1, 21);
                firstNumber = (int)Random.Range(1, startingNumber);
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

                        tempAnswers.Add(result);                //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + 1);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(left - right);
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
                        tempAnswers.Add(left + right);
                        break;

                    default:
                        break;
                }
                break;
            case 3:                                     //Operation ×
 
                operatorDecider = (int)Random.Range(1, 3);  // random number generation to decide operator/ Type of problem
                quation = "";
                startingNumber = factorsOf100[(int)Random.Range(0, 8)];
                firstNumber = (int)Random.Range(0, 10);
                secondNumber = startingNumber - firstNumber;
                switch (operatorDecider) // based on result of operatorDecider generates either addition or subtraction problem
                {

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
                        tempAnswers.Add(left + right);
                        break;

                    case 2:
                        equationOperator = operators[2];

                        secondNumber = startingNumber * firstNumber;
                        quation = "" + secondNumber + equationOperator + firstNumber + "=" + startingNumber;


                        right = firstNumber;//Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        operationText = equationOperator;                     //Operation string assignment
                        result = left * right;                  //Calculating the result of expression	

                        tempAnswers.Add(result);                //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + right);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(left - right);
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
                operatorDecider = (int)Random.Range(1, 4);  // random number generation to decide operator/ Type of problem
                quation = "";
                startingNumber = factorsOf100[(int)Random.Range(0, 8)];
                firstNumber = (int)Random.Range(0, 10);
                secondNumber = startingNumber - firstNumber;
                switch (operatorDecider
                ) // based on result of operatorDecider generates either addition or subtraction problem
                {
                    case 1:
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
                        tempAnswers.Add(left - right);
                        break;
                    case 2:
                        equationOperator = operators[1];
                        quation = "" + startingNumber + equationOperator + firstNumber + "=" + secondNumber;


                        right = firstNumber;//Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        operationText = equationOperator;                     //Operation string assignment
                        result = left - right;                  //Calculating the result of expression	

                        tempAnswers.Add(result);                //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + 1);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(left + right);
                        break;

                    case 3:
                        equationOperator = operators[2];

                        startingNumber = (int) Random.Range(1, 10);
                        firstNumber = (int)Random.Range(1, 13);
                        secondNumber = startingNumber * firstNumber;
                        quation = "" + secondNumber + equationOperator + firstNumber + "=" + startingNumber;


                        right = firstNumber; //Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        operationText = equationOperator; //Operation string assignment
                        result = left * right; //Calculating the result of expression	

                        tempAnswers.Add(result); //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + right);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(left - right);
                        break;

                    default:
                        break;
                }
                break;
            case 5:                                     //Operation ×
                //right = Random.Range(1, 13);
                operatorDecider = (int)Random.Range(2, 4);  // random number generation to decide operator/ Type of problem
                quation = "";
                
                switch (operatorDecider
                ) // based on result of operatorDecider generates either addition or subtraction problem
                {
                    case 2:
                        equationOperator = operators[2];

                        startingNumber = factorsOf100[(int)Random.Range(0, factorsOf100.Length)]; ;
                        firstNumber = (int)Random.Range(1, 13);
                        secondNumber = startingNumber * firstNumber;
                        quation = "" + secondNumber + equationOperator + firstNumber + "=" + startingNumber;


                        right = firstNumber; //Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        operationText = equationOperator; //Operation string assignment
                        result = left * right; //Calculating the result of expression	

                        tempAnswers.Add(result); //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + right);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(left - right);
                        break;

                    case 3:
                        equationOperator = operators[3];
                        startingNumber = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)]; ;
                        firstNumber = MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                        secondNumber = firstNumber / startingNumber;

                        right = firstNumber; //Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        result = right / left; //Calculating the result of expression
                        operationText = equationOperator; //Operation string assignment                            
                        tempAnswers.Add(result); //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + right);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(left - right);
                        break;
                    default:
                        break;
                }
                break;
            case 6:                                     //Operation ×
                //right = Random.Range(1, 13);
                operatorDecider = (int)Random.Range(2, 4);  // random number generation to decide operator/ Type of problem
                quation = "";

                switch (operatorDecider
                ) // based on result of operatorDecider generates either addition or subtraction problem
                {
                    case 2:
                        equationOperator = operators[2];

                        startingNumber = factorsOf900[(int)Random.Range(0, factorsOf900.Length)]; ;
                        firstNumber = (int)Random.Range(1, 13);
                        secondNumber = startingNumber * firstNumber;
                        quation = "" + secondNumber + equationOperator + firstNumber + "=" + startingNumber;


                        right = firstNumber; //Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        operationText = equationOperator; //Operation string assignment
                        result = left * right; //Calculating the result of expression	

                        tempAnswers.Add(result); //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + right);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(left - right);
                        break;

                    case 3:
                        equationOperator = operators[3];
                        startingNumber = factorsOf1441[(int)Random.Range(0, factorsOf1441.Length)]; ;
                        firstNumber = factorsOf1442[(int)Random.Range(0, factorsOf1442.Length)];
                        secondNumber = firstNumber / startingNumber;

                        right = firstNumber; //Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                        left = startingNumber;
                        result = right / left; //Calculating the result of expression
                        operationText = equationOperator; //Operation string assignment                            
                        tempAnswers.Add(result); //Insert correct and three fake answers in answers list
                        tempAnswers.Add(result + right);
                        tempAnswers.Add(result - 1);
                        tempAnswers.Add(left - right);
                        break;
                    default:
                        break;
                }
                break;
        }



        //expression.text = left.ToString() +operationTxt+ right.ToString();//Generated expression goes to UI element as text
        //generate different equation
        //expression1.text = left1.ToString() + operationTxt + right1.ToString();
        rightID = Random.Range(0, 4);
        equations[rightID].text = left.ToString() + equationOperator + right.ToString();
        equations[rightID].fontSize = GetFontSize(equations[trueID].text);
        int x = 0;
        foreach (Text equation in equations)
        {               //Here we go through answers list and insert fake answers that are randomly picked from tempAnswers
            if (x != rightID)
            {                           //Skip the element that is already filled with correct answer
                int rand = Random.Range(0, 4);
                float answer = 0;
                string op = equationOperator;
                float left1 = left + Random.Range(x + 1, x + 2);
                float right1 = right + Random.Range(x + 1, x + 2);
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
                        answer = MultipleOfTwo[(int)Random.Range(0, MultipleOfTwo.Length)] / MultipleOfTwoBig[(int)Random.Range(0, MultipleOfTwoBig.Length)];
                        break;

                }

                for (int n = 0; n < 4; n++)
                {
                    if (answer == tempAnswers[n] )
                    {
                        Debug.Log("Same!!!!");
                        left1 = left1+Random.Range(0,25);
                        right1 = right+Random.Range(0,25);
                        op = equationOperator;
                    }
                }
                /*
                if (answer == tempAnswers[0]|| answer == tempAnswers[1] || answer == tempAnswers[2] || answer == tempAnswers[3])
                {
                    Debug.Log("Same!!!!");
                    left1 += left;
                    right1 += right;
                }*/
                
                equation.text = left1.ToString() + op + right1.ToString();
                equation.fontSize = GetFontSize(equation.text);//Setting suitable font
                                                               //tempAnswers.RemoveAt(rand);
            }
            x++;
        }
        //Debug.Log("right place = "+ answerSlots[rightID].name);
        //answerSlots[rightID]


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
    public int GetFontSize(string answer)
    {               //Function that gets suitable font size for given string length
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
        /*
        answersGoUP.SetBool("NumberMove",true);
        answersGoUP.SetBool("NumberBack", false);
        StartCoroutine("WaitAndGo", 0.5f);
        */
        //Debug.LogError("!!!!!!!!!!!");
        //Invoke("TestFunc", 0.3f);
        
        
        Judge();
        /*
        Debug.Log("answer true =" + answers[trueID].text );
        Debug.Log("put slot "+ answerSlots[rightID].name);
        List<int> x = new List<int>();
        int z = 1;
        string str = string.Empty;
        foreach (Transform slotTransform in slots)
        {
            GameObject item = slotTransform.GetComponent<Slot>().item;
            if (item)
            {
                x.Add(z);
                
                str = item.name;
            }
            else
            {
                x.Add(0);
            }

            z++;
        }

        int addup = 0;
        for (int i = 0; i<x.Count; i++)
        {
            //Debug.Log(x[i]);
            addup += x[i];
        }

        if (rightID+1 == addup)
        {
            //Debug.Log("Slot TRUE!!!!!!");
            if (trueID + 1 == int.Parse(str))
            {

               Debug.Log("Correct!!!!!!!!!");
                gotRight();
                //Generate();
            }
            else
            {
                gotWrong();
            }
        }
        else
        {
            //Debug.Log("<color=#9400D3>" + "WRONG" + "</color>");
            gotWrong();
        }
        //Debug.Log("Item name" + str);
        */
    }

    IEnumerator WaitAndGo(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
        answersGoUP.SetBool("NumberMove", false);
        Generate();
    }
    

    public void Judge()
    {
        Debug.Log("answer true =" + answers[trueID].text);
        Debug.Log("put slot " + answerSlots[rightID].name);
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
            //Debug.Log(x[i]);
            addup += x[i];
        }

        wrongNumber = addup;
        if (rightID + 1 == addup)
        {
            //Debug.Log("Slot TRUE!!!!!!");
            if (trueID + 1 == int.Parse(str))
            {

                Debug.Log("Correct!!!!!!!!!");
                isRight();
                //Generate();
            }
            else
            {
                isWrong();
            }
        }
        else
        {
            //Debug.Log("<color=#9400D3>" + "WRONG" + "</color>");
            isWrong();
        }
        //Debug.Log("Item name" + str);
        //answersGoUP.SetBool("NumberBack", true);
    }

}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }    

}