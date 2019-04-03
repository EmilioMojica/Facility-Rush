using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class tutorialAssembyManager : MonoBehaviour
{
    [SerializeField] private float maxTime;         // float variable representing the maximum amount of time a player can have
    [SerializeField] private Text gameTimerText;    // Text variable representing the game timer in the UI
    [SerializeField] private float gameTimer;       // float variable representing the amount of time the player has left on the clock
    public int gradelevel;                          // integer representing the grade level that the player selected

    public GameObject pipeOneChoice1;
    public GameObject pipeOneChoice2;

    public GameObject pipeThreeChoice1;
    public GameObject pipeThreeChoice2;

    public GameObject additionPanel;                // GameObject representing the addition panel that players can select to indicate the addition panel
    public GameObject subtractionPanel;             // Game Object representing the subtraction panel that the player can select to indicate the subtraction panel
    public GameObject multiplicationPanel;          // GameObject representing the multiplication panel that the player can select to indicate the multiplication panel
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
    private string operatorSign;
    private int answer;

    public Text showChoice1;
    public Text showChoice2;
    public Text showChoice3;

    public Text solution;
    private int positionInPipeOne;
    private int positionInPipeTwo;
    private int positionInPipeThree;

    //int[] badPipeNumbers;

    int badPipeNumber;
    public bool gameOver;

    int score;
    public Text scoreText;
    public Text feedbackText;

    public int numberAttempted;
    public int numberCorrect;
    public int numberCorrectSoFar;

    [SerializeField] private bool isToySpawned;
    [SerializeField] private GameObject theToy;

    public GameObject timer;
    // Use this for initialization
   // [SerializeField] private Animator[] assemblyLineAnimator;
    [SerializeField] private GameObject[] toyPartsForAnimator;
    //[SerializeField] private GameObject[] toyPartChoices;
    [SerializeField] private GameObject[] sampleToys;
    //[SerializeField] private Animator[] pipeAnimators;
    [SerializeField] private Transform[] toySpawnPoint;

   // [SerializeField] private Animator toyAnimator;

    [SerializeField] private GameObject toyHolder;

    [SerializeField] private GameObject correctToy;
    [SerializeField] private GameObject incorrectToy;

    [SerializeField] private GameObject createdToy;

    [SerializeField] private GameObject[] toyPartsInPipeOne;
    [SerializeField] private GameObject[] toyPartsInPipeTwo;
    [SerializeField] private GameObject[] toyPartsInPipeThree;

    [SerializeField] GameObject bottom;
    [SerializeField] GameObject middle;
    [SerializeField] GameObject top;

    public bool isAnimating;

    float pipe1DownTime;
    float pipe1UpTime;
    float pipe2DownTime;
    float pipe2UpTime;
    float pipe3DownTime;
    float pipe3UpTime;

    float toyConveyerBeltTransition1;
    float toyConveyerBeltTransition2;
    float toyConveyerBeltTransition3;
    float toyConveyerBeltResetPosition;
    [SerializeField] private Animator theGoldenGodAnimator;
    [SerializeField] private Animator handAnimator;
    [SerializeField] private assemblyTimer theTimer;

    private float pointTo4Delay;
    private float pointToPlusDelay;
    private float pointTo6Delay;
    private float pointToProgress;
    private float pointToEquationButton;
    private float pointTo3Delay;
    private float pointTo3OnRightSideDelay;
    private float pointToScore;
    private float pointToTimer;

    [SerializeField] private bool firstProblemSolved;
    [SerializeField] private bool secondProbelmSolved;

    [SerializeField] Text dialogText;

    public bool choiceOnePipeOneNeedsToBePicked;
    public bool choiceTwoPipeOneNeedsToBePicked;

    public bool choiceOnePipeTwoNeedsToBePicked;
   
    public bool choiceOnePipeThreeNeedsToBePicked;
    public bool choiceTwoPipeThreeNeedsToBePicked;

    IEnumerator newAnimationLoop(GameObject toyToInstantiate, Transform spawningPoint)
    {
    choiceOnePipeOneNeedsToBePicked=false;
    choiceTwoPipeOneNeedsToBePicked=false;

    choiceOnePipeTwoNeedsToBePicked=false;

    choiceOnePipeThreeNeedsToBePicked=false;
    choiceTwoPipeThreeNeedsToBePicked=false;
    isAnimating = true;
        //theTimer.setIsAnimating(true);
        theGoldenGodAnimator.SetInteger("nextTransition", 0);
        yield return new WaitForSeconds(pipe1DownTime);
        //Activate Audio
        int index = Random.Range(3, 5);
        //AudioManager.instance.soundAudioSource.clip = AudioManager.instance.soundClip[index];  //choose between drill or gear SFX
        //AudioManager.instance.soundAudioSource.Play();
        theToy = Instantiate(toyToInstantiate, spawningPoint.position, spawningPoint.rotation);
        theToy.SetActive(true);
        Transform spotOnBelt = toyHolder.transform;
        theToy.transform.parent = toyHolder.transform;
        theToy.transform.position = spotOnBelt.transform.position;
        //toyAnimator = theToy.GetComponent<Animator>();
        toyPartsForAnimator[0] = theToy.transform.GetChild(0).transform.GetChild(0).gameObject;
        toyPartsForAnimator[0].SetActive(true);
        toyPartsForAnimator[1] = theToy.transform.GetChild(0).transform.GetChild(1).gameObject;
        toyPartsForAnimator[1].SetActive(false);
        toyPartsForAnimator[2] = theToy.transform.GetChild(0).transform.GetChild(2).gameObject;
        toyPartsForAnimator[2].SetActive(false);
        theGoldenGodAnimator.SetInteger("nextTransition", 1);
        yield return new WaitForSeconds(pipe1UpTime);
        theGoldenGodAnimator.SetInteger("nextTransition", 2);
        yield return new WaitForSeconds(toyConveyerBeltTransition1);
        theGoldenGodAnimator.SetInteger("nextTransition", 3);
        yield return new WaitForSeconds(pipe2DownTime);
        //Activate Audio
        index = Random.Range(3, 5);
        //AudioManager.instance.soundAudioSource.clip = AudioManager.instance.soundClip[index];  //choose between drill or gear SFX
       // AudioManager.instance.soundAudioSource.Play();
        toyPartsForAnimator[1].SetActive(true);
        theGoldenGodAnimator.SetInteger("nextTransition", 4);
        yield return new WaitForSeconds(pipe2UpTime);
        theGoldenGodAnimator.SetInteger("nextTransition", 5);
        yield return new WaitForSeconds(toyConveyerBeltTransition2);
        theGoldenGodAnimator.SetInteger("nextTransition", 6);
        yield return new WaitForSeconds(pipe3DownTime);
        //Activate Audio
        index = Random.Range(3, 5);
       // AudioManager.instance.soundAudioSource.clip = AudioManager.instance.soundClip[index];  //choose between drill or gear SFX
       // AudioManager.instance.soundAudioSource.Play();
        toyPartsForAnimator[2].SetActive(true);
        theGoldenGodAnimator.SetInteger("nextTransition", 7);
        yield return new WaitForSeconds(pipe3UpTime);
        theGoldenGodAnimator.SetInteger("nextTransition", 8);
        yield return new WaitForSeconds(toyConveyerBeltTransition3);
        theToy.SetActive(false);
        Destroy(theToy);
        theGoldenGodAnimator.SetInteger("nextTransition", 9);
        yield return new WaitForSeconds(toyConveyerBeltResetPosition);
        theGoldenGodAnimator.SetInteger("nextTransition", -1);
        //
        deleteToyParts();
        //theTimer.setIsAnimating(false);
        isAnimating = false;
        nextEquation();
    }
    public IEnumerator anaiamteHand(int animationPoint)
    {
        switch(animationPoint)
        {

        }
        yield return null;
    }
    public void setChuteOneChoice(string choice)
    {
        chuteOneChoice = choice;
        //showChoice1.gameObject.SetActive(true);
    }

    public void setChuteTwoChoice(string choice)
    {
        chuteTwoChoice = choice;
    }

    public void setChuteThreeChoice(string choice)
    {
        chuteThreeChoice = choice;
    }

    public void componentChoice(int choiceIdentifier)
    {
        print("The component choice is being called.");
        switch (choiceIdentifier)
        {
            case 0:

                bottom = toyPartsInPipeOne[0];

                break;
            case 1:

                bottom = toyPartsInPipeOne[1];

                break;
            case 2:

                middle = toyPartsInPipeTwo[0];

                break;
            case 3:

                middle = toyPartsInPipeTwo[1];

                break;
            case 4:

                middle = toyPartsInPipeTwo[2];
                break;
            case 5:
                middle = toyPartsInPipeTwo[3];
                break;
            case 6:
                top = toyPartsInPipeThree[0];
                break;
            case 7:

                top = toyPartsInPipeThree[1];
                break;

        }
    }
    void Start()
    {
        // GameObject trial=Instantiate(sampleToys[0], createdToy.transform.position, createdToy.transform.rotation);
        // GameObject changedPart = trial.transform.GetChild(1).gameObject;
        //changedPart = sampleToys[1].transform.GetChild(1).gameObject;
        //trial.transform.parent = createdToy.transform;
        toyPartsInPipeOne = new GameObject[2];
        toyPartsInPipeThree = new GameObject[2];
        toyPartsInPipeTwo = new GameObject[4];
        pipe1DownTime = theGoldenGodAnimator.runtimeAnimatorController.animationClips[4].length;
        pipe1UpTime = theGoldenGodAnimator.runtimeAnimatorController.animationClips[5].length;
        pipe2DownTime = theGoldenGodAnimator.runtimeAnimatorController.animationClips[6].length;
        pipe2UpTime = theGoldenGodAnimator.runtimeAnimatorController.animationClips[7].length;
        // print("This is pipe 2 up time: "+ pipe2UpTime);
        pipe3DownTime = theGoldenGodAnimator.runtimeAnimatorController.animationClips[8].length;
        pipe3UpTime = theGoldenGodAnimator.runtimeAnimatorController.animationClips[9].length;

        toyConveyerBeltTransition1 = theGoldenGodAnimator.runtimeAnimatorController.animationClips[0].length;
        toyConveyerBeltTransition2 = theGoldenGodAnimator.runtimeAnimatorController.animationClips[1].length;
        toyConveyerBeltTransition3 = theGoldenGodAnimator.runtimeAnimatorController.animationClips[2].length;
        toyConveyerBeltResetPosition = theGoldenGodAnimator.runtimeAnimatorController.animationClips[3].length;

        isAnimating = false;
        isToySpawned = false;
        numberAttempted = 0;
        numberCorrect = 0;
        numberCorrectSoFar = 0;

        gameOver = false;
        feedbackText.gameObject.SetActive(false);
        score = 0;
        scoreText.text = "" + score;
        showChoice1.text = "";
        showChoice1.gameObject.SetActive(false);
        showChoice2.text = "";
        showChoice2.gameObject.SetActive(false);
        showChoice3.text = "";
        showChoice3.gameObject.SetActive(false);
       // gradelevel = PlayerPrefs.GetInt("grade");
       // print(gradelevel);
       // initiateProperMiddlePanel();
        //badPipeNumbers = new int[2];
        nextEquation();

    }
   
   
    public void nextEquation()
    {
        int indexOfCorrectToy = 0;
        int indexOfIncorrectToy = 1;
        correctToy = sampleToys[indexOfCorrectToy];
        incorrectToy = sampleToys[indexOfIncorrectToy];
        string equation = "4+6=10";
        // print(equation);

        //print(ExpressionEvaluator.Evaluate<int>("4"));
        breakdownEquation(equation);
        solution.text = answer + "";
    }
    public void determinePositionInPipe2()
    {
        switch (operatorSign)
        {
            case "+":
                positionInPipeTwo = 0;
                // print("the position for 2 is: " + positionInPipeTwo);
                break;
            case "-":
                positionInPipeTwo = 1;
                // print("the position for 2 is: " + positionInPipeTwo);
                break;
            case "*":
                positionInPipeTwo = 2;
                // print("the position for 2 is: "+ positionInPipeTwo);
                break;
            case "/":
                positionInPipeTwo = 3;
                // print("the position for 2 is: " + positionInPipeTwo);
                break;

            default:
                // print("Pipe 2 Poisition has not been established");
                break;
        }

        for (int i = 0; i < toyPartsInPipeTwo.Length; i++)
        {
            if (i != positionInPipeTwo)
            {
                toyPartsInPipeTwo[i] = incorrectToy.transform.GetChild(1).gameObject;
            }
        }
        toyPartsInPipeTwo[positionInPipeTwo] = correctToy.transform.GetChild(1).gameObject;
    }
    public void breakdownEquation(string wholeEquation)
    {
        string numericalValue = "";
        for (int i = 0; i < wholeEquation.Length; i++)
        {
            // print("wholeEquation[i] is " + wholeEquation[i]);
            if (wholeEquation[i].Equals('+') || wholeEquation[i].Equals('-') || wholeEquation[i].Equals('*') || wholeEquation[i].Equals('/'))
            {
                operatorSign = wholeEquation[i] + "";
                //ExpressionEvaluator.Evaluate<int>(numericalValue,out firstPart);
                firstPart = int.Parse(numericalValue);
                //operatorSign = ""+wholeEquation[i];
                numericalValue = "";
            }
            else if (wholeEquation[i].Equals('='))
            {
                //ExpressionEvaluator.Evaluate<int>(numericalValue,out secondPart);
                secondPart = int.Parse(numericalValue);
                numericalValue = "";
            }
            else
            {
                numericalValue += wholeEquation[i];
                //print("Right now numerical value is: " + numericalValue);
            }
        }
        //ExpressionEvaluator.Evaluate<int>(numericalValue,out answer);
        answer = int.Parse(numericalValue);
        positionInPipeOne = 0;
        positionInPipeThree = 1;
        determinePositionInPipe2();
        pipeOne[positionInPipeOne].text = firstPart + "";
        toyPartsInPipeOne[positionInPipeOne] = correctToy.transform.GetChild(0).gameObject;
        pipeThree[positionInPipeThree].text = secondPart + "";
        toyPartsInPipeThree[positionInPipeThree] = correctToy.transform.GetChild(2).gameObject;
        solution.text = answer + "";
        setNumberinPipe(firstPart, pipeOne, positionInPipeOne, toyPartsInPipeOne);
        setNumberinPipe(secondPart, pipeThree, positionInPipeThree, toyPartsInPipeThree);
    }

    public void setNumberinPipe(int numberInPipe, Text[] pipe, int positionInPipe, GameObject[] arrayForToyPartSelection)
    {
        // int k = 0;
        determineRandomNumbers(gradelevel, numberInPipe);
        for (int i = 0; i < 2; i++)
        {
            if (i != positionInPipe)
            {
                pipe[i].text = badPipeNumber + "";
                arrayForToyPartSelection[i] = incorrectToy.transform.GetChild(i).gameObject;
                //k++;
            }

        }
    }

    public void determineRandomNumbers(int gradeLevel, int doNotEqual)
    {
        badPipeNumber = 3;
    }
    // Update is called once per frame
    void Update()
    {
       
    }
    public void initiateGameOver()
    {
        gameOver = true;
        feedbackText.text = "GameOver";
    }

    public void createToy()
    {
        createdToy.transform.GetChild(0).transform.position = createdToy.transform.position;
        GameObject createdBottom = Instantiate(bottom, createdToy.transform.GetChild(0).position, createdToy.transform.GetChild(0).rotation);
        createdBottom.transform.parent = createdToy.transform.GetChild(0).transform;
        createdToy.transform.GetChild(0).transform.position = createdToy.transform.position;
        createdBottom.transform.position = correctToy.transform.GetChild(0).transform.position;
        GameObject createdMiddle = Instantiate(middle, createdToy.transform.GetChild(1).position, createdToy.transform.GetChild(1).rotation);
        createdMiddle.transform.parent = createdToy.transform.GetChild(0).transform;
        createdMiddle.transform.position = correctToy.transform.GetChild(1).transform.position;
        createdToy.transform.GetChild(0).transform.position = createdToy.transform.position;
        GameObject createdTop = Instantiate(top, createdToy.transform.GetChild(2).position, createdToy.transform.GetChild(2).rotation);
        createdTop.transform.parent = createdToy.transform.GetChild(0).transform;
        createdTop.transform.position = correctToy.transform.GetChild(2).transform.position;
        createdToy.transform.GetChild(0).transform.position = createdToy.transform.position;
        createdToy.transform.GetChild(0).transform.position = createdToy.transform.position;
        // createdToy.transform.GetChild(5).SetAsFirstSibling();
        //createdToy.transform.GetChild(5).SetAsFirstSibling();
        // createdToy.transform.GetChild(5).SetAsFirstSibling();

    }
    public void deleteToyParts()
    {
        Destroy(createdToy.transform.GetChild(0).transform.GetChild(0).gameObject);
        Destroy(createdToy.transform.GetChild(0).transform.GetChild(1).gameObject);
        Destroy(createdToy.transform.GetChild(0).transform.GetChild(2).gameObject);
    }
    public int evaluateEquation(string equation)
    {
        // print("This is equation at evaluateEquation: " + equation);
        string numericalValue = "";
        string operatorSign = "";
        int firstOperand = 0;
        int secondOperand = 0;
        int answer = 0; ;
        for (int i = 0; i < equation.Length; i++)
        {
            if (equation[i].Equals('+') || equation[i].Equals('-') || equation[i].Equals('*') || equation[i].Equals('/'))
            {
                operatorSign = equation[i] + "";
                firstOperand = int.Parse(numericalValue);
                //operatorSign = ""+wholeEquation[i];
                numericalValue = "";
            }
            else if (i == equation.Length - 1)
            {
                numericalValue += equation[i];
                secondOperand = int.Parse(numericalValue);
                // print("This is second operand at evaluateEquation: " + secondOperand);
                numericalValue = "";
            }
            else
            {
                numericalValue += equation[i];
                //print("Right now numerical value is: " + numericalValue);
            }
        }

        switch (operatorSign)
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
    public void checkequation()
    {
        //createToy();
        if (isAnimating == false)
        {

            if(firstProblemSolved==false)
            {
                StartCoroutine(switchANimationPhase(5));
                firstProblemSolved = true;
            }
            else if(firstProblemSolved==true && secondProbelmSolved==false)
            {
                dialogText.text = "Incorrect answers make incorect toys.";
                secondProbelmSolved = true;
                Invoke("scorePointer", 3);
            }
            createToy();
            string playerEquation = chuteOneChoice + chuteTwoChoice + chuteThreeChoice;
            feedbackText.gameObject.SetActive(true);
            int temp;
            //ExpressionEvaluator.Evaluate<int>(playerEquation, out temp);
            temp = evaluateEquation(playerEquation);
            if (temp == answer && isAnimating == false)
            {
                print("Correct");
                //StartCoroutine(spawnObjectAnimation(toySpawnPoint[0],createdToy));
                StartCoroutine(newAnimationLoop(createdToy, toySpawnPoint[0]));
                score += 100;
                PlayerPrefs.SetInt("recentAssemblyHighScore", score);
                scoreText.text = "" + score;
                feedbackText.text = "Correct";
                numberCorrect++;
                numberCorrectSoFar++;
                numberAttempted++;
                if (numberCorrectSoFar == 3)
                {
                    timer.GetComponent<assemblyTimer>().addTime();
                    numberCorrectSoFar = 0;
                }

                isToySpawned = false;
                // nextEquation();
            }
            else
            {
                // print("Incorrect");
                feedbackText.text = "Incorrect";
                //StartCoroutine(spawnObjectAnimation(toySpawnPoint[0], createdToy));
                StartCoroutine(newAnimationLoop(createdToy, toySpawnPoint[0]));
                numberAttempted++;
                nextEquation();

                isToySpawned = false;
                nextEquation();
            }

            showChoice1.text = "";
            showChoice2.text = "";
            showChoice3.text = "";
            showChoice1.gameObject.SetActive(false);
            showChoice2.gameObject.SetActive(false);
            showChoice3.gameObject.SetActive(false);
            //nextEquation();
        }
    }

    IEnumerator switchANimationPhase(int indexOfPhase)
    {
        switch(indexOfPhase)
        {
            case -1:
                handAnimator.SetInteger("nextTransition", -1);
                break;

            case 0:
                handAnimator.SetInteger("nextTransition", 0);
                break;

            case 1:
                choiceOnePipeTwoNeedsToBePicked = true;
                handAnimator.SetInteger("nextTransition", 1);
                break;

            case 2:
                choiceTwoPipeThreeNeedsToBePicked = true;
                handAnimator.SetInteger("nextTransition", 2);
                //yield return new WaitForSeconds(3);
                //handAnimator.SetInteger("nextTransition", 3);
                break;

            case 3:
                handAnimator.SetInteger("nextTransition", 3);
                yield return new WaitForSeconds(2f);
                handAnimator.SetInteger("nextTransition", 4);
                dialogText.text = "Now click on \"check equation\"";
                break;

            case 4:
                handAnimator.SetInteger("nextTransition", 4);
                yield return new WaitForSeconds(.5f);
                handAnimator.SetInteger("nextTransition", 5);
                dialogText.text="Now click on \"check equation\"";
                handAnimator.SetInteger("nextTransition", 6);
                break;

            case 5:
                dialogText.text = "Correct answers assemble correct toys";
                yield return new WaitForSeconds(3);
                pipeOneChoice1.SetActive(false);
                pipeOneChoice2.SetActive(true);
                pipeThreeChoice2.SetActive(false);
                pipeThreeChoice1.SetActive(true);
                dialogText.text = "Now let's show an incorrect solution";
                yield return new WaitForSeconds(2);
                handAnimator.SetInteger("nextTransition", 5);
                dialogText.text = "Click on the 3 panel";
                choiceTwoPipeOneNeedsToBePicked = true;
                break;

            case 6:
                choiceOnePipeTwoNeedsToBePicked = true;
                handAnimator.SetInteger("nextTransition", 6);
                break;

            case 7:
                choiceOnePipeThreeNeedsToBePicked = true;
                handAnimator.SetInteger("nextTransition", 7);
                break;
            case 8:
                handAnimator.SetInteger("nextTransition", 8);
                break;
            case 9:
                dialogText.text = "Correct answers increase score";
                handAnimator.SetInteger("nextTransition", 9);
                yield return new WaitForSeconds(3);
                handAnimator.SetInteger("nextTransition", 10);
                dialogText.text = "When timer is at 0:00 the game is over";
                yield return new WaitForSeconds(2);
                GameOverPanel.SetActive(true);
                break;
            case 10:
                new WaitForSeconds(5);
                SceneManager.LoadScene("Ford_Test");
                handAnimator.SetInteger("nextTransition", 10);
                break;
        }

        yield return null;
    }

    public void startAnimation()
    {
        handAnimator.gameObject.SetActive(true);
        StartCoroutine(switchANimationPhase(0));
        dialogText.text = "Let's begin by clicking the 4 panel";
    }

    public void click4panel()
    {
        choiceOnePipeOneNeedsToBePicked = false;
        showChoice1.gameObject.SetActive(true);
        StartCoroutine(switchANimationPhase(1));
        dialogText.text = "Great Now click the plus panel panel";
    }

    public void clickPlusPanel()
    {
        if (firstProblemSolved == false)
        {
            choiceOnePipeTwoNeedsToBePicked = false;
            showChoice2.gameObject.SetActive(true);
            StartCoroutine(switchANimationPhase(2));
            dialogText.text = "Great Now click the six panel";
        }
        else
        {
            choiceOnePipeTwoNeedsToBePicked = false;
            showChoice2.gameObject.SetActive(true);
            StartCoroutine(switchANimationPhase(7));
            dialogText.text = "Great Now click the other 3 panel";
        }
    }

    public void clickSixPanel()
    {
        choiceTwoPipeThreeNeedsToBePicked = false;
        showChoice3.gameObject.SetActive(true);
        StartCoroutine(switchANimationPhase(3));
        dialogText.text = "Notice the built equation";
    }

    public void clickLeft3()
    {
        choiceTwoPipeOneNeedsToBePicked = false;
        showChoice1.gameObject.SetActive(true);
        StartCoroutine(switchANimationPhase(6));
        dialogText.text = "Great Now click the plus panel";
    }

    public void clickRight3()
    {
        showChoice3.gameObject.SetActive(true);
        StartCoroutine(switchANimationPhase(8));
        dialogText.text = "Great Now click the 3 panel";
    }

    public void scorePointer()
    {
        StartCoroutine(switchANimationPhase(9));
    }
}
