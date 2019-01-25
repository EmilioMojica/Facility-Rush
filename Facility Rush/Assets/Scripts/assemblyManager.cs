using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/*
* Script Goals: Manage Assembly Line minigame- Entails calling for an equation to be generated based on grade level, displaying each part of the equation to be generated in the three 
* pipes, check the equation, give feedback, and go to next equation
*/
public class assemblyManager : MonoBehaviour
{
    public int gradelevel;                  // 

    public GameObject additionPanel;
    public GameObject subtractionPanel;
    public GameObject multiplicationPanel;
    public GameObject divisionPanel;

    public GameObject checkEquationButton;
    public GameObject GameOverPanel;

    public GameObject equationGenerator;

    private string chuteOneChoice;
    private string chuteTwoChoice;
    private string chuteThreeChoice;
    
    public Text[] pipeOne;
    public Text[] pipeThree;

    private int firstPart;
    private int secondPart;
    private int answer;

    public Text showChoice1;
    public Text showChoice2;
    public Text showChoice3;

    public Text solution;
    private int positionInPipeOne;
    private int positionInPipeThree;

    int[] badPipeNumbers;
    private bool gameOver;

    int score;
    public Text scoreText;
    public Text feedbackText;

    // Use this for initialization

    public void setChuteOneChoice(string choice)
    {
        chuteOneChoice = choice;
    }

    public void setChuteTwoChoice(string choice)
    {
        chuteTwoChoice = choice;
    }

    public void setChuteThreeChoice(string choice)
    {
        chuteThreeChoice = choice;
    }
    void Start ()
    {
        feedbackText.gameObject.SetActive(false);
        score = 0;
        scoreText.text = "Score: " + score;
        showChoice1.text = "";
        showChoice2.text = "";
        showChoice3.text = "";
        gradelevel = PlayerPrefs.GetInt("grade");
        initiateProperMiddlePanel();
        badPipeNumbers = new int[2];
        nextEquation();
    }
    public void initiateProperMiddlePanel()
    {
        switch(gradelevel)
        {
            case 0:
                additionPanel.SetActive(true);
                break;
            case 1:
                additionPanel.SetActive(true);
                subtractionPanel.SetActive(true);
                break;
            case 2:
                additionPanel.SetActive(true);
                subtractionPanel.SetActive(true);
                break;
            case 3:
                additionPanel.SetActive(true);
                subtractionPanel.SetActive(true);
                multiplicationPanel.SetActive(true);
                divisionPanel.SetActive(true);
                break;
            case 4:
                additionPanel.SetActive(true);
                subtractionPanel.SetActive(true);
                multiplicationPanel.SetActive(true);
                divisionPanel.SetActive(true);
                break;
            case 5:
                additionPanel.SetActive(true);
                subtractionPanel.SetActive(true);
                multiplicationPanel.SetActive(true);
                divisionPanel.SetActive(true);
                break;
        }
    }
    public void nextEquation()
    {
        string equation=equationGenerator.GetComponent<generateEquations>().generateEquation(gradelevel);
        print(equation);

        //print(ExpressionEvaluator.Evaluate<int>("4"));
        breakdownEquation(equation);
        solution.text = answer + "";
    }
    public void breakdownEquation(string wholeEquation)
    {
        string numericalValue="";
        for(int i=0; i<wholeEquation.Length;i++)
        {
           // print("wholeEquation[i] is " + wholeEquation[i]);
            if(wholeEquation[i].Equals('+')|| wholeEquation[i].Equals('-')|| wholeEquation[i].Equals('*')|| wholeEquation[i].Equals('/'))
            {
                firstPart = ExpressionEvaluator.Evaluate<int>(numericalValue);
                //operatorSign = ""+wholeEquation[i];
                numericalValue = "";
            }
            else if(wholeEquation[i].Equals('='))
            {
                secondPart = ExpressionEvaluator.Evaluate<int>(numericalValue);
                numericalValue = "";
            }
            else
            {
                numericalValue += wholeEquation[i];
                //print("Right now numerical value is: " + numericalValue);
            }
        }
        answer = ExpressionEvaluator.Evaluate<int>(numericalValue);
        positionInPipeOne = Random.Range(0, 3);
        positionInPipeThree= Random.Range(0, 3);
        pipeOne[positionInPipeOne].text = firstPart + "";
        pipeThree[positionInPipeThree].text = secondPart + "";

        solution.text = answer + "";

        setNumberinPipe(firstPart, pipeOne,positionInPipeOne);
        setNumberinPipe(secondPart, pipeThree,positionInPipeThree);
    }

    public void setNumberinPipe(int numberInPipe,Text [] pipe,int positionInPipe)
    {
        int k = 0;
        determineRandomNumbers(gradelevel, numberInPipe);
        for(int i =0;i<3;i++)
        {
            if(i!=positionInPipe)
            {
                pipe[i].text=badPipeNumbers[k]+"";
                k++;
            }
            
        }
    }

    public void determineRandomNumbers(int gradeLevel, int doNotEqual)
    {
        int firstGenerated=0;
        int secondGenerated=0;
        switch(gradeLevel)
        {
            case 0:
                do
                {
                    firstGenerated= Random.Range(1, 11);
                    secondGenerated= Random.Range(1, 11);
                } while (firstGenerated == secondGenerated && firstGenerated == doNotEqual && secondGenerated == doNotEqual);
                badPipeNumbers[0] = firstGenerated;
                badPipeNumbers[1] = secondGenerated;
                break;
            case 1:
                do
                {
                    firstGenerated = Random.Range(1, 21);
                    secondGenerated = Random.Range(1, 21);
                } while (firstGenerated == secondGenerated && firstGenerated == doNotEqual && secondGenerated == doNotEqual);
                badPipeNumbers[0] = firstGenerated;
                badPipeNumbers[1] = secondGenerated;
                break;
            case 2:
                do
                {
                    firstGenerated = Random.Range(1, 101);
                    secondGenerated = Random.Range(1, 101);
                } while (firstGenerated == secondGenerated && firstGenerated == doNotEqual && secondGenerated == doNotEqual);
                badPipeNumbers[0] = firstGenerated;
                badPipeNumbers[1] = secondGenerated;
                break;
            case 3:
                do
                {
                    firstGenerated = Random.Range(1, 101);
                    secondGenerated = Random.Range(1, 101);
                } while (firstGenerated == secondGenerated && firstGenerated == doNotEqual && secondGenerated == doNotEqual);
                badPipeNumbers[0] = firstGenerated;
                badPipeNumbers[1] = secondGenerated;
                break;
            case 4:
                do
                {
                    firstGenerated = Random.Range(1, 101);
                    secondGenerated = Random.Range(1, 101);
                } while (firstGenerated == secondGenerated && firstGenerated == doNotEqual && secondGenerated == doNotEqual);
                badPipeNumbers[0] = firstGenerated;
                badPipeNumbers[1] = secondGenerated;
                break;
            case 5:
                do
                {
                    firstGenerated = Random.Range(1, 101);
                    secondGenerated = Random.Range(1, 101);
                } while (firstGenerated == secondGenerated && firstGenerated == doNotEqual && secondGenerated == doNotEqual);
                break;
            default:
                print("nope");
                break;
        }
        
    }
	// Update is called once per frame
	void Update ()
    {
		if(gameOver==true)
        {
            checkEquationButton.SetActive(false);
            GameOverPanel.SetActive(true);
        }
	}

    public void initiateGameOver()
    {
        gameOver = true;
    }

    public void checkequation()
    {
        string playerEquation = chuteOneChoice + chuteTwoChoice + chuteThreeChoice;
        feedbackText.gameObject.SetActive(true);
        if(ExpressionEvaluator.Evaluate<int>(playerEquation)==answer)
        {
            print("Correct");
            score += 100;
            PlayerPrefs.SetInt("recentAssemblyHighScore", score);
            scoreText.text = "Score: " + score;
            feedbackText.text = "Correct";
        }
        else
        {
            print("Incorrect");
            feedbackText.text = "Incorrect";

        }
        showChoice1.text = "";
        showChoice2.text = "";
        showChoice3.text = "";
        nextEquation();
    }

    
}
