using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

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

    [SerializeField]private bool isToySpawned;
    [SerializeField]private GameObject theToy;

    public GameObject timer;
    // Use this for initialization
    [SerializeField] private Animator [] assemblyLineAnimator;
    [SerializeField] private GameObject[] toyPartsForAnimator;
    [SerializeField] private GameObject[] toyPartChoices;
    [SerializeField] private GameObject[] sampleToys;
    [SerializeField] private Animator[] pipeAnimators;
    [SerializeField] private Transform[] toySpawnPoint;

    [SerializeField] private Animator toyAnimator;

    [SerializeField] private GameObject toyHolder;

    [SerializeField] private GameObject correctToy;
    [SerializeField] private GameObject incorrectToy;

    [SerializeField]private GameObject createdToy;

    [SerializeField] private GameObject[] toyPartsInPipeOne;
    [SerializeField] private GameObject[] toyPartsInPipeTwo;
    [SerializeField] private GameObject[] toyPartsInPipeThree;

   [SerializeField] GameObject bottom;
   [SerializeField] GameObject middle;
   [SerializeField] GameObject top;

    bool isAnimating;

    IEnumerator Animation(Animator anime, Transform spawnPoint, GameObject toyPart,GameObject partToInstantiate)
    {
        anime.SetBool("choiceMade", true);
        yield return new WaitForSecondsRealtime(1);
        if (isToySpawned==false)
        {
            print("I'm about to instantiate");
            theToy=Instantiate(toyPart, spawnPoint.position, spawnPoint.rotation);
            theToy.transform.parent = toyHolder.transform;
            //toyAnimator = theToy.GetComponent<Animator>();
            toyPartsForAnimator[0] = theToy.transform.GetChild(0).gameObject;
            toyPartsForAnimator[0].SetActive(true);
            toyPartsForAnimator[1]= theToy.transform.GetChild(1).gameObject;
            toyPartsForAnimator[1].SetActive(false);
            toyPartsForAnimator[2]= theToy.transform.GetChild(2).gameObject;
            toyPartsForAnimator[2].SetActive(false);

            print("this the child of the toy instantiated: " + theToy.transform.GetChild(0).gameObject.name);
            isToySpawned = true;
            
        }
        if(partToInstantiate!=null)
        {
            print("I'M REAL");
            partToInstantiate.SetActive(true);
        }
        anime.SetBool("toySpawned", true);
        anime.SetBool("choiceMade", false);
        yield return new WaitForSecondsRealtime(1);
        anime.SetBool("toySpawned", false);
        anime.SetBool("isFinish", true);
        yield return new WaitForSecondsRealtime(1);
        anime.SetBool("isFinish", false);
    }

    IEnumerator spawnObjectAnimation(Transform spawnPoint,GameObject toyToSpawn)
    {
        isAnimating = true;
        print("spawnig intitialized");
        StartCoroutine(Animation(pipeAnimators[0], spawnPoint,toyToSpawn, toyPartsForAnimator[0]));
        yield return new WaitForSecondsRealtime(2);
        toyAnimator.SetInteger("position", 1);
        yield return new WaitForSecondsRealtime(1);
        StartCoroutine(Animation(pipeAnimators[1], spawnPoint, toyToSpawn, toyPartsForAnimator[1]));
        yield return new WaitForSecondsRealtime(2);
        toyAnimator.SetInteger("position", 2);
        yield return new WaitForSecondsRealtime(1);
        StartCoroutine(Animation(pipeAnimators[2], spawnPoint, toyToSpawn, toyPartsForAnimator[2]));
        yield return new WaitForSecondsRealtime(2);
        toyAnimator.SetInteger("position", 3);
        yield return new WaitForSeconds(1);
        toyAnimator.SetInteger("position", 4);
        Destroy(theToy);
        yield return new WaitForSeconds(1);
        toyAnimator.SetInteger("position", 0);
        yield return new WaitForSecondsRealtime(1);
        //if()
        //nextEquation();
        deleteToyParts();
        isAnimating = false;
        //nextEquation();
    }

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

    public void componentChoice(int choiceIdentifier)
    {
      switch(choiceIdentifier)
        {
            case 0:
                //if(bottom!=null)
                //{
                //    Destroy(bottom);
                //}
                bottom = toyPartsInPipeOne[0];
                //bottom  = Instantiate(toyPartsInPipeOne[0].transform.gameObject, createdToy.transform.GetChild(0).position, createdToy.transform.GetChild(0).rotation);
                break;
            case 1:
                //if (bottom != null)
                //{
                //    Destroy(bottom);
                //}
                bottom = toyPartsInPipeOne[1];
                //bottom = Instantiate(toyPartsInPipeOne[1].transform.gameObject, createdToy.transform.GetChild(0).position, createdToy.transform.GetChild(0).rotation);
                break;
            case 2:
                //if (middle != null)
                //{
                //    Destroy(bottom);
                //}
                middle = toyPartsInPipeTwo[0];
                //middle = Instantiate(toyPartsInPipeTwo[0].transform.gameObject, createdToy.transform.GetChild(1).position, createdToy.transform.GetChild(1).rotation);
                break;
            case 3:
                //if (middle != null)
                //{
                //    Destroy(bottom);
                //}
                middle = toyPartsInPipeTwo[1];
                //middle = Instantiate(toyPartsInPipeTwo[1].transform.gameObject, createdToy.transform.GetChild(1).position, createdToy.transform.GetChild(1).rotation);
                break;
            case 4:
                //if (middle != null)
                //{
                //    Destroy(bottom);
                //}
                middle = toyPartsInPipeTwo[2];
                //middle = Instantiate(toyPartsInPipeTwo[2].transform.gameObject, createdToy.transform.GetChild(1).position, createdToy.transform.GetChild(1).rotation);
                break;
            case 5:
                //if (middle != null)
                //{
                //    Destroy(bottom);
                //}
                middle = toyPartsInPipeTwo[3];
                //middle = Instantiate(toyPartsInPipeTwo[3].transform.gameObject, createdToy.transform.GetChild(1).position, createdToy.transform.GetChild(1).rotation);
                break;
            case 6:
                //if (top != null)
                //{
                //    Destroy(bottom);
                //}
                top = toyPartsInPipeThree[0];
                //top = Instantiate(toyPartsInPipeThree[0].transform.gameObject, createdToy.transform.GetChild(2).position, createdToy.transform.GetChild(2).rotation);
                break;
            case 7:
                //if (top != null)
                //{
                //    Destroy(bottom);
                //}
                top = toyPartsInPipeThree[1];
                //top = Instantiate(toyPartsInPipeThree[1].transform.gameObject, createdToy.transform.GetChild(2).position, createdToy.transform.GetChild(2).rotation);
                break;

        }
    }
    void Start ()
    {
        toyPartsInPipeOne = new GameObject[2];
        toyPartsInPipeThree = new GameObject[2];
        toyPartsInPipeTwo = new GameObject[4];
        isAnimating = false;
        isToySpawned = false;
        numberAttempted=0;
        numberCorrect=0;
        numberCorrectSoFar=0;
        //sampleToys[0].transform.GetChild(0).transform.parent = createdToy.transform;
        //sampleToys[1].transform.GetChild(1).transform.parent = createdToy.transform;
        //sampleToys[0].transform.GetChild(2).transform.parent = createdToy.transform;
       // bottom=Instantiate(sampleToys[0].transform.GetChild(0).gameObject, createdToy.transform.GetChild(0).position, createdToy.transform.GetChild(0).rotation);
       // middle= Instantiate(sampleToys[1].transform.GetChild(1).gameObject, createdToy.transform.GetChild(1).position, createdToy.transform.GetChild(1).rotation);
        //top= Instantiate(sampleToys[0].transform.GetChild(2).gameObject, createdToy.transform.GetChild(2).position, createdToy.transform.GetChild(2).rotation);
       // bottom.transform.parent = createdToy.transform;
       // middle.transform.parent = createdToy.transform;
       // top.transform.parent = createdToy.transform;
        gameOver = false;
        feedbackText.gameObject.SetActive(false);
        score = 0;
        scoreText.text = "Score: " + score;
        showChoice1.text = "";
        showChoice2.text = "";
        showChoice3.text = "";
        //gradelevel = PlayerPrefs.GetInt("grade");
        print(gradelevel);
        initiateProperMiddlePanel();
        //badPipeNumbers = new int[2];
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
                //divisionPanel.SetActive(true);
                break;
            case 4:
                //additionPanel.SetActive(true);
                //subtractionPanel.SetActive(true);
                multiplicationPanel.SetActive(true);
                divisionPanel.SetActive(true);
                break;
            case 5:
                //additionPanel.SetActive(true);
                //subtractionPanel.SetActive(true);
                multiplicationPanel.SetActive(true);
                divisionPanel.SetActive(true);
                break;
        }
    }
    public void nextEquation()
    {
        int indexOfCorrectToy = Random.Range(0, sampleToys.Length);
        int indexOfIncorrectToy = Random.Range(0, sampleToys.Length);
            while(indexOfCorrectToy==indexOfIncorrectToy)
            {
            indexOfIncorrectToy = Random.Range(0, sampleToys.Length);
            }
        correctToy = sampleToys[indexOfCorrectToy];
        incorrectToy = sampleToys[indexOfIncorrectToy];
        string equation=equationGenerator.GetComponent<generateEquations>().generateEquation(gradelevel);
        print(equation);

        //print(ExpressionEvaluator.Evaluate<int>("4"));
        breakdownEquation(equation);
        solution.text = answer + "";
    }
    public void determinePositionInPipe2()
    {
        switch(operatorSign)
        {
            case "+":
                positionInPipeTwo = 0;
                print("the position for 2 is: " + positionInPipeTwo);
                break;
            case "-":
                positionInPipeTwo = 1;
                print("the position for 2 is: " + positionInPipeTwo);
                break;
            case "*":
                positionInPipeTwo = 2;
                print("the position for 2 is: "+ positionInPipeTwo);
                break;
            case "/":
                positionInPipeTwo = 3;
                print("the position for 2 is: " + positionInPipeTwo);
                break;

            default:
                print("Pipe 2 Poisition has not been established");
                break;
        }

        for(int i=0;i<toyPartsInPipeTwo.Length;i++)
        {
            if(i!=positionInPipeTwo)
            {
                toyPartsInPipeTwo[i] = incorrectToy.transform.GetChild(1).gameObject;
            }
        }
        toyPartsInPipeTwo[positionInPipeTwo] = correctToy.transform.GetChild(1).gameObject;
    }
    public void breakdownEquation(string wholeEquation)
    {
        string numericalValue="";
        for(int i=0; i<wholeEquation.Length;i++)
        {
           // print("wholeEquation[i] is " + wholeEquation[i]);
            if(wholeEquation[i].Equals('+')|| wholeEquation[i].Equals('-')|| wholeEquation[i].Equals('*')|| wholeEquation[i].Equals('/'))
            {
                operatorSign = wholeEquation[i]+"";
                ExpressionEvaluator.Evaluate<int>(numericalValue,out firstPart);
                //operatorSign = ""+wholeEquation[i];
                numericalValue = "";
            }
            else if(wholeEquation[i].Equals('='))
            {
                ExpressionEvaluator.Evaluate<int>(numericalValue,out secondPart);
                numericalValue = "";
            }
            else
            {
                numericalValue += wholeEquation[i];
                //print("Right now numerical value is: " + numericalValue);
            }
        }
        ExpressionEvaluator.Evaluate<int>(numericalValue,out answer);
        positionInPipeOne = Random.Range(0, 2);
        positionInPipeThree= Random.Range(0, 2);
        determinePositionInPipe2();
        pipeOne[positionInPipeOne].text = firstPart + "";
        toyPartsInPipeOne[positionInPipeOne] = correctToy.transform.GetChild(0).gameObject;
        pipeThree[positionInPipeThree].text = secondPart + "";
        toyPartsInPipeThree[positionInPipeThree] = correctToy.transform.GetChild(2).gameObject;
        solution.text = answer + "";
        setNumberinPipe(firstPart, pipeOne,positionInPipeOne,toyPartsInPipeOne);
        setNumberinPipe(secondPart, pipeThree,positionInPipeThree,toyPartsInPipeThree);
    }
    
    public void setNumberinPipe(int numberInPipe,Text [] pipe,int positionInPipe, GameObject[] arrayForToyPartSelection)
    {
       // int k = 0;
        determineRandomNumbers(gradelevel, numberInPipe);
        for(int i =0;i<2;i++)
        {
            if(i!=positionInPipe)
            {
                pipe[i].text=badPipeNumber+"";
                arrayForToyPartSelection[i] = incorrectToy.transform.GetChild(i).gameObject;
                //k++;
            }
            
        }
    }

    public void determineRandomNumbers(int gradeLevel, int doNotEqual)
    {
        int firstGenerated=0;
        //int secondGenerated=0;
        switch(gradeLevel)
        {
            case 0:
                do
                {
                    firstGenerated= Random.Range(1, 11);
                    
                } while (firstGenerated == doNotEqual);
                badPipeNumber = firstGenerated;
                break;
            case 1:
                do
                {
                    firstGenerated = Random.Range(1, 21);
                    
                } while (firstGenerated == doNotEqual);
                badPipeNumber= firstGenerated;
                break;
            case 2:
                do
                {
                    firstGenerated = Random.Range(1, 101);
                } while (firstGenerated == doNotEqual);
                badPipeNumber= firstGenerated;
                break;
            case 3:
                do
                {
                    firstGenerated = Random.Range(1, 101);
                } while (firstGenerated == doNotEqual);
                badPipeNumber= firstGenerated;
                break;
            case 4:
                do
                {
                    firstGenerated = Random.Range(1, 101);
                } while (firstGenerated == doNotEqual);
                badPipeNumber= firstGenerated;
                break;
            case 5:
                do
                {
                    firstGenerated = Random.Range(1, 101);
                } while (firstGenerated == doNotEqual);
                badPipeNumber= firstGenerated;
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
        feedbackText.text = "GameOver";
    }
    public void createToy()
    {
        GameObject createdBottom=Instantiate(bottom, createdToy.transform.GetChild(0).position, createdToy.transform.GetChild(0).rotation);
        createdBottom.transform.parent = createdToy.transform;
        GameObject createdMiddle=Instantiate(middle, createdToy.transform.GetChild(1).position, createdToy.transform.GetChild(1).rotation);
        createdMiddle.transform.parent = createdToy.transform;
        GameObject createdTop=Instantiate(top, createdToy.transform.GetChild(2).position, createdToy.transform.GetChild(2).rotation);
        createdTop.transform.parent = createdToy.transform;
        createdToy.transform.GetChild(5).SetAsFirstSibling();
        createdToy.transform.GetChild(5).SetAsFirstSibling();
        createdToy.transform.GetChild(5).SetAsFirstSibling();

    }
    public void deleteToyParts()
    {
        Destroy(createdToy.transform.GetChild(0).gameObject);
        Destroy(createdToy.transform.GetChild(1).gameObject);
        Destroy(createdToy.transform.GetChild(2).gameObject);
    }
    public void checkequation()
    {
        //createToy();
        if (isAnimating==false)
        {
            createToy();
            string playerEquation = chuteOneChoice + chuteTwoChoice + chuteThreeChoice;
            feedbackText.gameObject.SetActive(true);
            int temp;
            ExpressionEvaluator.Evaluate<int>(playerEquation, out temp);
            if (temp == answer && isAnimating == false)
            {
                print("Correct");
                StartCoroutine(spawnObjectAnimation(toySpawnPoint[0],createdToy));
                score += 100;
                PlayerPrefs.SetInt("recentAssemblyHighScore", score);
                scoreText.text = "Score: " + score;
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
                nextEquation();
            }
            else
            {
                print("Incorrect");
                feedbackText.text = "Incorrect";
                StartCoroutine(spawnObjectAnimation(toySpawnPoint[0], createdToy));
                numberAttempted++;
                nextEquation();

                isToySpawned = false;
                nextEquation();
            }
            
            showChoice1.text = "";
            showChoice2.text = "";
            showChoice3.text = "";
            //nextEquation();
        }
    }

    
}
