using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PipesManager : MonoBehaviour
{
    [SerializeField] private bool isAnimating;
    [SerializeField] private Animator pipeGyroAnimator;
    [SerializeField] private Animator pipeMovementAnimator;
    [SerializeField] private GameObject[] spheresBeingAnimated;
    [SerializeField] private Text problemNumber;
    public Text scoreText;
    public int Score;
   // private bool answerInBank;
    public Text textOne;
    public Text textTwo;
    public Text textThree;
    public Text textFour;

    public Text[] equationsOnTheGameBoard;
    public Text[] spheres;

    public List <string>equationsToCheckForSimilarities = new List <string>();
    public List<int> equationsAnswersToCheckForSimilarities = new List<int>();

    [SerializeField] private GameObject[] answerSlots = new GameObject[4];
    [SerializeField] private GameObject[] questionSlots = new GameObject[4];
    public int[] answerBankForSphereAnswersToBeRemoved = new int[3];

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

    public int currentProblem;

    [SerializeField] private GameObject equationGeneration; 
    // Start is called before the first frame update
    public int gradelevel;

    private int problemGenerated;
    private int answer;

    private int locationOfWhatToReturn=-1;
    private bool checkingAnswer;

    private int numberCorrect;
    private float delayTime;
    public void setAppropriateListForGradeLevelsKthrough5()
    {
        //print("The list has been made");
        switch(gradelevel)
        {
            case 0:
                ListModifiedDuringLevel = KindergartenAnswerList;
                break;
            case 1:
                ListModifiedDuringLevel = FirstGradeAnswerList;
                break;
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
        //questionSlots[0].GetComponent<GridLayoutGroup>().enabled = false;
        // print("String equivalence test: " + "9+1".Equals("9+1") );
        numberCorrect = 0;
        delayTime= pipeMovementAnimator.runtimeAnimatorController.animationClips[0].length;
        float animationTime=pipeGyroAnimator.runtimeAnimatorController.animationClips[0].length;
        print("The length of the animation is: "+ animationTime);
        string clipName = pipeGyroAnimator.runtimeAnimatorController.animationClips[3].name;
        print("The name of the animation is: " + clipName);
        isAnimating = false;
        checkingAnswer = false;
        print((int)1.95f);
        gradelevel = PlayerPrefs.GetInt("grade");
        setAppropriateListForGradeLevelsKthrough5();
        
        scoreText.text = "10000";
        currentProblem = 0;
        generateProblems();
        printStuff();
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
            //gameTimer += Time.deltaTime;
            //if(gameTimer>1)
            //{
            //    Score -= 10;
            //    gameTimer = 0.0f;
            //    scoreText.text = "" + Score;
            //}
            //if(Score==0)
            //{
            //    initiateGameOver();
            //}

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
            // checkIfAnswered();
            if (checkingAnswer==false)
            {
                //checkingAnswer = true;
                checkEquation();
                //checkingAnswer = false;
            }
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
               // print("extracted equation is " + finalExtraction);
                extractedEquation = "";
            }
            else
            {
                extractedEquation += solvedEquation[i];
            }
        }
        return finalExtraction;
    }

    public string reverseOfProblem(string equationToReverse)
    {
        string reversedEquation = "";
        string stringSoFar = "";
        int numberOnLeftSideOfOperator = 0;
        string operatorSign = "";
        int numberOnRightSideOfOperator = 0;

        for(int i=0;i<equationToReverse.Length;i++)
        {
            if (equationToReverse[i].Equals('+') || equationToReverse[i].Equals('-') || equationToReverse[i].Equals('*') || equationToReverse[i].Equals('/'))
            {
                numberOnLeftSideOfOperator = int.Parse(stringSoFar);
            }
            else if(i==(equationToReverse.Length-1))
            {
                stringSoFar += equationToReverse[i];
                numberOnRightSideOfOperator = int.Parse(stringSoFar);
            }
            else
            {
                stringSoFar += equationToReverse[i];
            }
        }


        return reversedEquation;
    }
    public void generateProblemOnBoard()
    {
        //print("generateProblemONBoard is called");
        problemNumber.text = "Problem " + (currentProblem + 1) + " out of 10";
        ListModifiedDuringLevel.Remove(correctAnswers[currentProblem]);
        int indexOfCorrectAnswerPlacement = (int)Random.Range(0, 4);
        //int indexofCorrectSphereAnswer = (int)Random.Range(0, 4);
        //int indexofCorrectEquationPlacement= (int)Random.Range(0, 4);

        equationsOnTheGameBoard[indexOfCorrectAnswerPlacement].text = correctProblems[currentProblem];
        spheres[indexOfCorrectAnswerPlacement].text = correctAnswers[currentProblem]+"";

        // equationsToCheckForSimilarities.Add(correctProblems[currentProblem]);
        equationsAnswersToCheckForSimilarities.Add(correctAnswers[currentProblem]);

        int index = 0;
        while (index!=3)
        {
            string falseEquationBeingGenerated = equationGeneration.GetComponent<generateEquations>().generateEquation(gradelevel);
            //print("false equation before modification at index " + i + " is " + falseEquationBeingGenerated);
            string falseEquation = getEquation(falseEquationBeingGenerated);
            string reversedEquation = reverseOfProblem(falseEquation);
            int falseEquationAnswer = evaluateEquation(falseEquation);
            if (!equationsAnswersToCheckForSimilarities.Contains(falseEquationAnswer))
            {
                printList();
                //print("The false equation at index " + i + " is " + falseEquation);
                //equationsOnTheGameBoard[i].text = falseEquation;
                //int falseEquationAnswer = getEquationAnswer(falseEquationBeingGenerated);
                ListModifiedDuringLevel.Remove(falseEquationAnswer);
                answerBankForSphereAnswersToBeRemoved[index] = falseEquationAnswer;
                equationsToCheckForSimilarities.Add(falseEquation);
                equationsAnswersToCheckForSimilarities.Add(falseEquationAnswer);
                index++;
            }

        }

        print("This is index after the while loop: " + index);
        index = 0;
        for(int i=0;i<4;i++)
        {
            if(i!= indexOfCorrectAnswerPlacement)//indexofCorrectEquationPlacement)
            {
                print("This is index at i " + ":"  + index);
                equationsOnTheGameBoard[i].text = equationsToCheckForSimilarities[index];
                index++;
            }
        }
        equationsToCheckForSimilarities.Clear();
        equationsAnswersToCheckForSimilarities.Clear();
        removePotentialCorrectAnswers();
        generateCorrectSphereAnswers(indexOfCorrectAnswerPlacement);
        //indexofCorrectSphereAnswer);
        fixVerticalText(equationsOnTheGameBoard[2].text, 2);
        fixVerticalText(equationsOnTheGameBoard[3].text, 3);

    }
    public void fixVerticalText(string equationToBreakdown,int indexOfEquation)
    {
        string numberOnLeftSideOfOperator = "";
        string operatorOfEquation = "";
        string numberOnRightSideOfOperator = "";
        string stringSoFar = "";
        for (int i = 0; i < equationToBreakdown.Length; i++)
        {
            if (equationToBreakdown[i].Equals('+') || equationToBreakdown[i].Equals('-') || equationToBreakdown[i].Equals('*') || equationToBreakdown[i].Equals('/'))
            {
                numberOnLeftSideOfOperator = stringSoFar;
                operatorOfEquation=equationToBreakdown[i]+"";
                stringSoFar = "";
            }
            else if (i == (equationToBreakdown.Length - 1))
            {
                stringSoFar += equationToBreakdown[i];
                numberOnRightSideOfOperator = stringSoFar;
            }
            else
            {
                stringSoFar += equationToBreakdown[i];
            }

            equationsOnTheGameBoard[indexOfEquation].text = numberOnLeftSideOfOperator + "\n" + operatorOfEquation + "\n" + numberOnRightSideOfOperator;
        }
    }
    public void printList()
    {
        int itemNumber = 0;
        foreach( string equation in equationsToCheckForSimilarities)
        {
            print("The equation at " + itemNumber + "in equationsToCheckForSimilarities is " + equation);
            itemNumber++;
        }
    }
    public int getEquationAnswer(string equation)
    {
        int toReturn = 0;
        string numericalValue = "";
        for(int i=equation.Length-1;i > 0 ; i--)
        {
            if(equation[i].Equals('='))
            {
                toReturn = int.Parse(numericalValue);
            }
            else
            {
                numericalValue += equation[i];
            }
        }
        return toReturn;
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
        calculateRestoration();
    }

    public void calculateRestoration()
    {
        DegradationManager degredationManager = GameObject.FindGameObjectWithTag("degredationManager").GetComponent<DegradationManager>();
        degredationManager.aAttempted = 10;
        degredationManager.aCorrect = numberCorrect;
        degredationManager.pipeCalculate();
        degredationManager.gameHasBeenPlayed(0);
        checkForNewHighScore();
        //degredationManager.setScoreOfRecentPlayedGame(Score);
    }
    public void checkForNewHighScore()
    {
        int currentHighScore = PlayerPrefs.GetInt("pipeGyroHighScore");
        if (Score > currentHighScore)
        {
            PlayerPrefs.SetInt("pipeGyroHighScore", Score);
        }
    }
    public int evaluateEquation(string equation)
    {
        print("This is equation at evaluateEquation: " + equation);
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
            else if (i==equation.Length-1)
            {
                numericalValue += equation[i];
                secondOperand = int.Parse(numericalValue);
                print("This is second operand at evaluateEquation: " + secondOperand);
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
        int numberOfSlotsFilled = 0;
        for(int i=0;i<4;i++)
        {
            if(answerSlots[i].transform.childCount>0)
            {
                numberOfSlotsFilled++;
            }
        }
        //if(numberOfSlotsFilled>1)
        //{
        //    restoreChoices();
        //}
        if (gameOver == false && numberOfSlotsFilled==1 && isAnimating==false)
        {
            checkingAnswer = true;
            int check = 0;
            int number = 25;
            for (int i = 0; i < 4; i++)
            {

                if (answerSlots[i].transform.childCount > 0)
                {
                    locationOfWhatToReturn = i;
                    print("this is this object: " + answerSlots[i].transform.GetChild(0).GetChild(0).gameObject.name);
                    check = evaluateEquation(answerSlots[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
                    //ExpressionEvaluator.Evaluate<int>(spheres[i].transform.GetChild(0).GetComponent<Text>().text, out number);
                    number = int.Parse(spheres[i].text);
                    if (check == number)
                    {
                        numberCorrect++;
                        restoreList();
                        Score += 100;
                        scoreText.text = "" + Score;
                        currentProblem++;
                        //restoreChoices();
                        print("this is i: " + i);
                        StartCoroutine(animateSpheres(i));
                    }
                    else
                    {
                        //StartCoroutine(animatePipeFall(i));
                        Score -= 1000;
                        scoreText.text = "" + Score; 
                        restoreList();
                        restoreChoices();
                        //checkingAnswer = false;
                    }
                }
            }
        }
        checkingAnswer = false;
    }
    IEnumerator animatePipeFall(int indexOfPipeSection)
    {
        print("Animation beginning");
        int indexOfPipeToBeAnimated = 5 + pipeToBeAnimated();
        print("Value of indexOfPipe: "+ indexOfPipeToBeAnimated);
        switch(indexOfPipeSection)
        {
            case 0:
                pipeMovementAnimator.SetInteger("pipeAction", 0);
                switch(indexOfPipeToBeAnimated)
                {
                    case 5:
                        pipeMovementAnimator.SetInteger("pipeAction", 5);
                        pipeMovementAnimator.SetInteger("pipeAction", -2);
                        yield return new WaitForSeconds(delayTime);
                        break;
                    case 6:
                        pipeMovementAnimator.SetInteger("pipeAction", 6);
                        pipeMovementAnimator.SetInteger("pipeAction", -2);
                        yield return new WaitForSeconds(delayTime);
                        break;
                    case 7:
                        pipeMovementAnimator.SetInteger("pipeAction", 7);
                        pipeMovementAnimator.SetInteger("pipeAction", -2);
                        yield return new WaitForSeconds(delayTime);
                        break;
                    case 8:
                        pipeMovementAnimator.SetInteger("pipeAction", 8);
                        pipeMovementAnimator.SetInteger("pipeAction", -2);
                        yield return new WaitForSeconds(delayTime);
                        break;
                }
                break;
            case 1:
                pipeMovementAnimator.SetInteger("pipeAction", 1);
                switch (indexOfPipeToBeAnimated)
                {
                    case 5:
                        pipeMovementAnimator.SetInteger("pipeAction", 5);
                        pipeMovementAnimator.SetInteger("pipeAction", -2);
                        yield return new WaitForSeconds(delayTime);
                        break;
                    case 6:
                        pipeMovementAnimator.SetInteger("pipeAction", 6);
                        pipeMovementAnimator.SetInteger("pipeAction", -2);
                        yield return new WaitForSeconds(delayTime);
                        break;
                    case 7:
                        pipeMovementAnimator.SetInteger("pipeAction", 7);
                        pipeMovementAnimator.SetInteger("pipeAction", -2);
                        yield return new WaitForSeconds(delayTime);
                        break;
                    case 8:
                        pipeMovementAnimator.SetInteger("pipeAction", 8);
                        pipeMovementAnimator.SetInteger("pipeAction", -2);
                        yield return new WaitForSeconds(delayTime);
                        break;
                }
                break;
            case 2:
                pipeMovementAnimator.SetInteger("pipeAction", 2);
                switch (indexOfPipeToBeAnimated)
                {
                    case 5:
                        pipeMovementAnimator.SetInteger("pipeAction", 5);
                        pipeMovementAnimator.SetInteger("pipeAction", -2);
                        yield return new WaitForSeconds(delayTime);
                        break;
                    case 6:
                        pipeMovementAnimator.SetInteger("pipeAction", 6);
                        pipeMovementAnimator.SetInteger("pipeAction", -2);
                        yield return new WaitForSeconds(delayTime);
                        break;
                    case 7:
                        pipeMovementAnimator.SetInteger("pipeAction", 7);
                        pipeMovementAnimator.SetInteger("pipeAction", -2);
                        yield return new WaitForSeconds(delayTime);
                        break;
                    case 8:
                        pipeMovementAnimator.SetInteger("pipeAction", 8);
                        pipeMovementAnimator.SetInteger("pipeAction", -2);
                        yield return new WaitForSeconds(delayTime);
                        break;
                }
                break;
            case 3:
                pipeMovementAnimator.SetInteger("pipeAction", 3);
                switch (indexOfPipeToBeAnimated)
                {
                    case 5:
                        pipeMovementAnimator.SetInteger("pipeAction", 5);
                        pipeMovementAnimator.SetInteger("pipeAction", -2);
                        yield return new WaitForSeconds(delayTime);
                        break;
                    case 6:
                        pipeMovementAnimator.SetInteger("pipeAction", 6);
                        pipeMovementAnimator.SetInteger("pipeAction", -2);
                        yield return new WaitForSeconds(delayTime);
                        break;
                    case 7:
                        pipeMovementAnimator.SetInteger("pipeAction", 7);
                        pipeMovementAnimator.SetInteger("pipeAction", -2);
                        yield return new WaitForSeconds(delayTime);
                        break;
                    case 8:
                        pipeMovementAnimator.SetInteger("pipeAction", 8);
                        pipeMovementAnimator.SetInteger("pipeAction", -2);
                        yield return new WaitForSeconds(delayTime);
                        break;
                }
                break;
            default:
                print("That's a zoinks from me dog");
                break;
        }
        pipeMovementAnimator.SetInteger("pipeAction", -1);
        yield return null;
    }
    public int pipeToBeAnimated()
    {
        int toReturn = -20;
        for (int i = 0; i < answerSlots.Length; i++)
        {
            if (answerSlots[i].transform.childCount == 1)
            {
                // answerSlots[i].transform.GetChild(0).parent = questionSlots[int.Parse(answerSlots[i].transform.GetChild(0).gameObject.name)].transform;
                //answerSlots[i].transform.GetChild(0).transform.SetParent(questionSlots[int.Parse(answerSlots[i].transform.GetChild(0).gameObject.name)].transform, false);
                toReturn = int.Parse(answerSlots[i].transform.GetChild(0).gameObject.name);
            }
        }
        return toReturn;
    }
    IEnumerator animateSpheres(int indexOfSphere)
    {
        isAnimating = true;
        float animationTime = pipeGyroAnimator.runtimeAnimatorController.animationClips[0].length;
        pipeGyroAnimator.SetInteger("indexBeingActedOn",indexOfSphere);
        //yield return new WaitForSecondsRealtime(1);
        yield return new WaitForSeconds(animationTime);
        pipeGyroAnimator.SetInteger("indexBeingActedOn",10);
        spheresBeingAnimated[indexOfSphere].SetActive(false);
        //yield return new WaitForSecondsRealtime(1);
        yield return new WaitForSeconds(animationTime); //.3f
        pipeGyroAnimator.SetInteger("indexBeingActedOn",-1);
        //yield return new WaitForSecondsRealtime(1);
        yield return new WaitForSeconds(animationTime);
        spheresBeingAnimated[indexOfSphere].SetActive(true);
        restoreChoices();
        if (currentProblem == 10)
        {
            initiateGameOver();
        }
        else
        {
            generateProblemOnBoard();
        }
        isAnimating = false;
        checkingAnswer = false;
        yield return null;
    }
    
    public void restoreChoices()
    {
        for(int i=0;i<questionSlots.Length;i++)
        {
            if (answerSlots[i].transform.childCount == 1)
            {
                // answerSlots[i].transform.GetChild(0).parent = questionSlots[int.Parse(answerSlots[i].transform.GetChild(0).gameObject.name)].transform;
                if (i == 2 || i == 3)
                {
                    print("This is the object being reset: " + answerSlots[i].transform.GetChild(0).gameObject);
                    answerSlots[i].transform.GetChild(0).transform.localScale = new Vector3(1f, 1f, 1f);
                }
                answerSlots[i].transform.GetChild(0).transform.SetParent(questionSlots[int.Parse(answerSlots[i].transform.GetChild(0).gameObject.name)].transform, false);
                
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

    

   
}

