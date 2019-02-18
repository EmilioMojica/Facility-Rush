using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PipesManager : MonoBehaviour
{
    [SerializeField]private Text problemNumber;
    public Text scoreText;
    private int Score;
    public Text textOne;
    public Text textTwo;
    public Text textThree;
    public Text textFour;

    public Text[] equations;
    public Text[] spheres;

    public int[] answerBankForSphereAnswersToBeRemoved = new int[3];
    public string[] sphereNumber= new string[3];

    List <int> KindergartenAnswerList= new List<int>() {0,1,2,3,4,5,6,7,8,9,10};
    List<int> FirstGradeAnswerList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20};
    List<int> SecondGradeAnswerList = new List<int>(); // numbers 1 through 100
    List<int> ThirdGradeAnswerList = new List<int>();// numbers 1 through 108
    List<int> FourthGradeAnswerList = new List<int>(); // numbers 1 through 100
    List<int> FifthGradeAnswerList = new List<int>(); // numbers in factors of 900 and 144

    List<int> ListModifiedDuringLevel;

    private int[] factorsOf100 = new int[] { 1, 2, 4, 5, 10, 25, 50, 100 };
    private int[] factorsOf144 = new int[] { 1, 2, 3, 4, 6, 8, 9, 12, 16, 18, 24, 36, 48, 72, 144 };
    private int[] factorsOf900 = new int[] { 1, 2, 3, 4, 5, 6, 9, 10, 12, 15, 18, 20, 25, 30, 36, 45, 50, 60, 75, 90, 100, 150, 180, 225, 300, 450, 900 };

    private int[] correctAnswers= new int [10];
    private string[] correctProblems=new string[10];

    public bool gameOver;
    public float gameTimer;
    public float maxTime;
    public Text gameTimerText;

    private int currentProblem;

    [SerializeField] private GameObject equationGeneration; 
    private int showList;
    // Start is called before the first frame update
    public int gradelevel;

    private int problemGenerated;
    private int answer;


    public void setAppropriateListForGradeLevels2through5()
    {
        //print("The list has been made");
        switch(gradelevel)
        {
            case 2:
                for(int i=0;i<100;i++)
                {
                    SecondGradeAnswerList.Add(i + 1);
                }
                ListModifiedDuringLevel = SecondGradeAnswerList;
                break;
            case 3:
                for(int i=0;i<108;i++)
                {
                    ThirdGradeAnswerList.Add(i + 1);
                }
                ListModifiedDuringLevel = ThirdGradeAnswerList;
                break;
            case 4:
                for(int i=0;i<factorsOf100.Length;i++)
                {
                    for(int j=0;j<12;j++)
                    {
                        FourthGradeAnswerList.Add(factorsOf100[i] * (j+1));
                    }
                }
                ListModifiedDuringLevel = FourthGradeAnswerList;
                break;
            case 5:
               // print("5 is selected");
                int possibleaddition;
                bool alreadyExists = false;
                for (int i=0;i<factorsOf144.Length;i++)
                {
                    for(int j=0;j<12;j++)
                    {
                        possibleaddition = factorsOf144[i] % (j + 1);
                        alreadyExists = FifthGradeAnswerList.Contains(factorsOf144[i] / (j + 1));
                        if (possibleaddition==0 && alreadyExists == false)
                        {
                            FifthGradeAnswerList.Add(factorsOf144[i] / (j + 1));
                        }
                    }
                }

                for(int i=0;i<factorsOf900.Length;i++)
                {
                    for( int j=0;j<12;j++)
                    {
                        possibleaddition = factorsOf900[i] % (j + 1);
                        alreadyExists = FifthGradeAnswerList.Contains(factorsOf900[i] / (j + 1));
                       // print(alreadyExists);
                        if (possibleaddition == 0 && alreadyExists == false)
                        {
                            //print("this ran");
                            FifthGradeAnswerList.Add(factorsOf900[i] / (j + 1));
                        }
                    }
                }
                string items = "";
                foreach(int number in FifthGradeAnswerList)
                {
                    items += (number+", ");
                }
                FifthGradeAnswerList.Sort();
                print("List: " + items);
                ListModifiedDuringLevel = FifthGradeAnswerList;
                break;
                
        }
    }

    void Start()
    {
        if(gradelevel>1)
        {
            setAppropriateListForGradeLevels2through5();
        }
        Score = 0;
        scoreText.text = "Score: 0";
        currentProblem = 0;
        //string number = "45";
        //int n = int.Parse(number);
        //print("Testing my parser: "+n);
        showList = 0;
        //for (int i = 0; i < 10; i++)
        //{
        //    listOne.Add(Random.Range(0, 10));
        //    listTwo.Add(Random.Range(0, 10));
        //    listThree.Add(Random.Range(0, 10));
        //    listFour.Add(Random.Range(0, 10));
        //}
        generateProblems();
        printStuff();
        ShowText();
        generateProblemOnBoard();
    }
    public void printStuff()
    {
        string arrayOfCorrectAnswers = "";
        string arrayOfCorrectProblems = "";
        for(int i=0;i<10;i++)
        {
            arrayOfCorrectAnswers+= "This is correctAnswers[" + i + "]: " + correctAnswers[i]+", ";
            arrayOfCorrectProblems+="This is correctProblems[" + i + "]: " + correctProblems[i]+", ";
        }
        print(arrayOfCorrectAnswers);
        print(arrayOfCorrectProblems);
    }

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

    public void generateProblems()
    {
        for (int i = 0; i < 10; i++)
        {
            string equation = equationGeneration.GetComponent<generateEquations>().generateEquation(gradelevel);
            equationBreakdown(equation,i);
        }

    }

    
    public void equationBreakdown(string wholeEquation, int index)
    {
        string numericalValue = "";
        for (int i = 0; i < wholeEquation.Length; i++)
        {
            if (wholeEquation[i].Equals('='))
            {
                correctProblems[index] = numericalValue;
                numericalValue = "";
                
            }
            else
            {
                numericalValue += wholeEquation[i];
                //print("Right now numerical value is: " + numericalValue);
            }
        }
        //ExpressionEvaluator.Evaluate<int>(numericalValue, out answer);
        answer = int.Parse(numericalValue);
        correctAnswers[index] = answer;
        
    }

    public string getEquation(string solvedEquation)
    {
        string extractedEquation = "";
        string finalExtraction = "";
        for(int i=0;i< solvedEquation.Length;i++)
        {
            if(solvedEquation[i].Equals('='))
            {
                finalExtraction = extractedEquation;
                print("extracted equation is " + finalExtraction);
                extractedEquation = "";
            }
            else
            {
                extractedEquation += solvedEquation[i];
            }
        }
        return finalExtraction;
    }

    public void generateProblemOnBoard()
    {
        print("generateProblemONBoard is called");
        problemNumber.text = "Problem " + (currentProblem + 1) + " out of 10";
        ListModifiedDuringLevel.Remove(correctAnswers[currentProblem]);
        int indexofCorrectSphereAnswer = (int)Random.Range(0, 4);
        int indexofCorrectEquationPlacement= (int)Random.Range(0, 4);

        equations[indexofCorrectEquationPlacement].text = correctProblems[currentProblem];
        spheres[indexofCorrectSphereAnswer].text = correctAnswers[currentProblem]+"";

        int index = 0;
        for(int i=0;i<4;i++)
        {
            if (i != indexofCorrectEquationPlacement)
            {
                string falseEquationBeingGenerated = equationGeneration.GetComponent<generateEquations>().generateEquation(gradelevel);
                print("false equation before modification at index " + i + " is " + falseEquationBeingGenerated);
                string falseEquation = getEquation(falseEquationBeingGenerated);
                print("The false equation at index " + i + " is " + falseEquation);
                equations[i].text = falseEquation;
                int falseEquationAnswer = evaluateEquation(falseEquationBeingGenerated);
                ListModifiedDuringLevel.Remove(falseEquationAnswer);
                answerBankForSphereAnswersToBeRemoved[index] = falseEquationAnswer;
                index++;
            }
        }

        removePotentialCorrectAnswers();
        
    }

    public void printCurrentList()
    {
        string items = "";
        foreach (int number in ListModifiedDuringLevel)
        {
            items += (number + ", ");
        }
        FifthGradeAnswerList.Sort();
        print("List: " + items);
    }

    public void removePotentialCorrectAnswers()
    {
        for(int i=0;i<answerBankForSphereAnswersToBeRemoved.Length;i++)
        {
            ListModifiedDuringLevel.Remove(answerBankForSphereAnswersToBeRemoved[i]);
            ListModifiedDuringLevel.TrimExcess();
        }

        printCurrentList();

    }

    public void generateCorrectSphereAnswers(int indexOFCorrectSphereAnswer)
    {
        for(int i=0;i<spheres.Length;i++)
        {
            if(i!= indexOFCorrectSphereAnswer)
            {
                int answerToRemove = ListModifiedDuringLevel[(int)Random.Range(0, ListModifiedDuringLevel.Count)];
                spheres[i].text = answerToRemove + "";
                ListModifiedDuringLevel.Remove(answerToRemove);
                ListModifiedDuringLevel.TrimExcess();
            }
        }

        printCurrentList();
    }

    public void initiateGameOver()
    {
        gameOver = true;
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

    private void ShowText()
    {
        textOne.text = "1";
        textTwo.text = "2";
        textThree.text = "3";
        textFour.text = "4";
    }

    public void NextNumbers()//test function
    {
        showList++;
        ShowText();
    }
    public int evaluateEquation(string equation)
    {
        string numericalValue = "";
        string operatorSign = "";
        int firstOperand=0;
        int secondOperand=0;
        int answer = 0; ;
        for (int i = 0; i < equation.Length; i++)
        {
            if (equation[i].Equals('+') || equation[i].Equals('-') || equation[i].Equals('*') || equation[i].Equals('/'))
            {
                operatorSign = equation[i] + "";
                firstOperand=int.Parse(numericalValue);
                //operatorSign = ""+wholeEquation[i];
                numericalValue = "";
            }
            else if (equation[i].Equals('='))
            {
                secondOperand = int.Parse(numericalValue);
                numericalValue = "";
            }
            else
            {
                numericalValue += equation[i];
                //print("Right now numerical value is: " + numericalValue);
            }
        }

        switch(operatorSign)
        {
            case "+":
                answer = firstOperand + secondOperand;
                break;
            case "-":
                answer = firstOperand - secondOperand;
                break;
            case "*":
                answer = firstOperand * secondOperand;
                break;
            case "/":
                answer = firstOperand / secondOperand;
                break;
        }
        return answer;
    }
    public void checkEquation()
    {
        int check = 0;
        int number = 25;
        for(int i=0;i<4;i++)
        {
           
            if (equations[i].transform.childCount>0)
            {
                print("this is this object: " + equations[i].transform.GetChild(0).GetChild(0).gameObject.name);
                check=evaluateEquation(equations[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                //ExpressionEvaluator.Evaluate<int>(spheres[i].transform.GetChild(0).GetComponent<Text>().text, out number);
                number = int.Parse(spheres[i].text);
                if (check==number)
                {
                    restoreList();
                    Score += 100;
                    scoreText.text = "Score: " + Score;
                    currentProblem++;
                    if(currentProblem!=10)
                    {
                        generateProblemOnBoard();
                    }
                }
                else
                {
                    restoreList();
                }
            }
        }
    }

    public void restoreList()
    {
        for(int i=0;i<spheres.Length;i++)
        {
            ListModifiedDuringLevel.Add(int.Parse(spheres[i].text));
        }

        for(int i=0;i<answerBankForSphereAnswersToBeRemoved.Length;i++)
        {
            ListModifiedDuringLevel.Add(answerBankForSphereAnswersToBeRemoved[i]);
        }

        ListModifiedDuringLevel.Sort();
        printCurrentList();

    }

    public void checkIfGameOver()
    {
        if(currentProblem==10)
        {
            initiateGameOver();
        }
    }
}
