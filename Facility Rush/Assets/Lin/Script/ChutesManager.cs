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

    private List<string> operators = new List<string>();
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
    // Use this for initialization
    void Start ()
    {
        answersGoUP.SetBool("NumberBack", true);
        operators.Add("+");
        operators.Add("-");
        operators.Add("×");
        operators.Add("÷");
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
            operationID = Random.Range(1, 2);
            currentLimit = 6;
        }                          //Here I added a condition that limits operaion range for first level 
        else if (currentLevel == 1)
        {
            operationID = Random.Range(1, 3);
            currentLimit = 11;
        }
        else if (currentLevel == 2)
        {
            //operationID = 3;
            operationID = Random.Range(1, 3);
            currentLimit = 51;
        }
        else if (currentLevel == 3)
        {
            operationID = Random.Range(1, 4);
            currentLimit = 51;
        }
        else if(currentLevel == 4)                                      //If we are at 1st level then we use only + or -
        {
            operationID = Random.Range(1, 5);           //Otherwise we use all operaions
            currentLimit = 51;
        }
        else
        {
            operationID = Random.Range(1, 6);
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
        switch (operationID)
        {
            case 1:                                     //Operation +
                right = Random.Range(1, currentLimit);  //Generating random "Right" integer in range
                operationText = "+";                     //Operation string assignment
                result = left + right;                  //Calculating the result of expression
                if (result > 100)
                {
                    //left = Random.Range((currentLevel - 1) * 5, 51);
                    left = Random.Range(currentLevel* 5, 51);
                    right = Random.Range(1, 51);
                    result = left + right;
                }
                tempAnswers.Add(result);                //Insert correct answer in answers list
                                                        //Here (and in each case) is logic part that generates three fake answers (should not be as true answer). All 4 results go to tempAnswers list
                if ((left - right) >= 0)
                {
                    tempAnswers.Add(left - right);
                }
                else
                {
                    if (right - left != result)
                        tempAnswers.Add(right - left);
                    else
                        tempAnswers.Add(result * 2);
                }
                tempAnswers.Add(!tempAnswers.Contains(result + 1) ? result + 1 : result + 2);
                tempAnswers.Add(result - 1);
                // tempEauation.Add(new String{});


                break;
            case 2:                                     //Operation -
                right = Random.Range(1, (int)Mathf.Round(left + 1));//Generating random "Right" integer in range. We limit this integer so that it could not be greater than "Left"
                operationText = "-";                     //Operation string assignment
                result = left - right;                  //Calculating the result of expression	

                tempAnswers.Add(result);                //Insert correct and three fake answers in answers list
                tempAnswers.Add(result + 1);
                tempAnswers.Add(result - 1);
                tempAnswers.Add(left + right);
                break;
            case 3:                                     //Operation ×
                left = Random.Range(1, 10);
                right = Random.Range(1, 10);
                //right = Random.Range(1, 3 + (currentLevel - 1) * 2);//Generating random "Right" integer in range. We limit this integer with special logic so the result won't be too big.
                operationText = "×";                     //Operation string assignment
                result = left * right;                  //Calculating the result of expression
                /*
                if (result > 99)
                {
                    Debug.Log("Over number");
                    left = Random.Range(1, 10);
                    result = left * right;
                }
                */
                tempAnswers.Add(result);                //Insert correct and three fake answers in answers list
                tempAnswers.Add(result + right);
                tempAnswers.Add(result - 1);
                tempAnswers.Add(result + 1);
                break;
            case 4:                                     //Operation ÷
                /*                                        //Here we generate left and right inegers with special logic so that result will be always whole number (not float)
                left = Random.Range(1, 3 + (currentLevel - 1) * 2);
                right = Random.Range(1, 3 + (currentLevel - 1) * 2);
                */
                left = Random.Range(1, 13);
                right = Random.Range(1, 13);
                float tempSum = left * right;
                left = tempSum;
                operationText = "÷";
                result = left / right;                  //Calculating the result of expression

                tempAnswers.Add(result);                //Insert correct and three fake answers in answers list
                if ((left - right) != result)
                    tempAnswers.Add(left - right);
                else
                    tempAnswers.Add(left + right);
                tempAnswers.Add(result + 1);
                if (left * right != result)
                    tempAnswers.Add(left * right);
                else
                    tempAnswers.Add(result * 5);
                break;
            case 5:                                     //Operation ×
                right = Random.Range(1, 13);
                //right = Random.Range(1, 3 + (currentLevel - 1) * 2);//Generating random "Right" integer in range. We limit this integer with special logic so the result won't be too big.
                operationText = "×";                     //Operation string assignment
                result = left * right;                  //Calculating the result of expression
                if (result > 144)
                {
                    Debug.Log("Over number");
                    left = Random.Range(1, 13);
                    result = left * right;
                }

                tempAnswers.Add(result);                //Insert correct and three fake answers in answers list
                tempAnswers.Add(result + right);
                tempAnswers.Add(result - 1);
                tempAnswers.Add(result + 1);
                break;
        }



        //expression.text = left.ToString() +operationTxt+ right.ToString();//Generated expression goes to UI element as text
        //generate different equation
        //expression1.text = left1.ToString() + operationTxt + right1.ToString();
        rightID = Random.Range(0, 4);
        equations[rightID].text = left.ToString() + operationText + right.ToString();
        equations[rightID].fontSize = GetFontSize(equations[trueID].text);
        int x = 0;
        foreach (Text equation in equations)
        {               //Here we go through answers list and insert fake answers that are randomly picked from tempAnswers
            if (x != rightID)
            {                           //Skip the element that is already filled with correct answer
                int rand = Random.Range(0, 4);
                float answer = 0;
                string op = operators[rand];
                float left1 = left + Random.Range(x + 1, x + 2);
                float right1 = right + Random.Range(x + 1, x + 2);
                //Debug.Log("tempAnswers[x]"+ tempAnswers[x].ToString());
                switch (rand)
                {
                    case 0:
                        answer = left1 + right1;
                        break;
                    case 1:
                        answer = left1 - right1;
                        break;
                    case 2:
                        answer = left1 * right1;
                        break;
                    case 3:
                        answer = left1 / right1;
                        break;

                }

                for (int n = 0; n < 4; n++)
                {
                    if (answer == tempAnswers[n] )
                    {
                        Debug.Log("Same!!!!");
                        left1 = left1+Random.Range(0,25);
                        right1 = right+Random.Range(0,25);
                        op = "+";
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