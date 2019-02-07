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
    private int answer;

    public Text showChoice1;
    public Text showChoice2;
    public Text showChoice3;

    public Text solution;
    private int positionInPipeOne;
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
    private GameObject theToy;

    public GameObject timer;
    // Use this for initialization
    [SerializeField] private Animator [] assemblyLineAnimator;
    [SerializeField] private GameObject[] toyParts;
    [SerializeField] private GameObject[] sampleToys;
    [SerializeField] private Animator[] pipeAnimators;
    [SerializeField] private Transform[] toySpawnPoint;

    [SerializeField] private Animator toyAnimator;

    [SerializeField] private GameObject toyHolder;

    [SerializeField] private GameObject correctToy;
    [SerializeField] private GameObject incorrectToy;

    [SerializeField]private GameObject createdToy;

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
            toyParts[0] = theToy.transform.GetChild(0).gameObject;
            toyParts[0].SetActive(true);
            toyParts[1]= theToy.transform.GetChild(1).gameObject;
            toyParts[1].SetActive(false);
            toyParts[2]= theToy.transform.GetChild(2).gameObject;
            toyParts[2].SetActive(false);

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

    IEnumerator spawnObjectAnimation(Transform spawnPoint)
    {
        print("spawnig intitialized");
        StartCoroutine(Animation(pipeAnimators[0], spawnPoint,sampleToys[0],toyParts[0]));
        yield return new WaitForSecondsRealtime(2);
        toyAnimator.SetInteger("position", 1);
        yield return new WaitForSecondsRealtime(1);
        StartCoroutine(Animation(pipeAnimators[1], spawnPoint, sampleToys[0],toyParts[1]));
        yield return new WaitForSecondsRealtime(2);
        toyAnimator.SetInteger("position", 2);
        yield return new WaitForSecondsRealtime(1);
        StartCoroutine(Animation(pipeAnimators[2], spawnPoint, sampleToys[0],toyParts[2]));
        yield return new WaitForSecondsRealtime(2);
        toyAnimator.SetInteger("position", 3);
        yield return new WaitForSeconds(1);
        toyAnimator.SetInteger("position", 4);
        Destroy(theToy);
        yield return new WaitForSeconds(1);
        toyAnimator.SetInteger("position", 0);
        yield return null;
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
    void Start ()
    {
        isToySpawned = false;
        numberAttempted=0;
        numberCorrect=0;
        numberCorrectSoFar=0;
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
        pipeOne[positionInPipeOne].text = firstPart + "";
        pipeThree[positionInPipeThree].text = secondPart + "";

        solution.text = answer + "";

        setNumberinPipe(firstPart, pipeOne,positionInPipeOne);
        setNumberinPipe(secondPart, pipeThree,positionInPipeThree);
    }

    public void setNumberinPipe(int numberInPipe,Text [] pipe,int positionInPipe)
    {
       // int k = 0;
        determineRandomNumbers(gradelevel, numberInPipe);
        for(int i =0;i<2;i++)
        {
            if(i!=positionInPipe)
            {
                pipe[i].text=badPipeNumber+"";
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

    public void checkequation()
    {
        string playerEquation = chuteOneChoice + chuteTwoChoice + chuteThreeChoice;
        feedbackText.gameObject.SetActive(true);
        int temp;
        ExpressionEvaluator.Evaluate<int>(playerEquation, out temp);
        if (temp==answer)
        {
            print("Correct");
            StartCoroutine(spawnObjectAnimation(toySpawnPoint[0]));
            score += 100;
            PlayerPrefs.SetInt("recentAssemblyHighScore", score);
            scoreText.text = "Score: " + score;
            feedbackText.text = "Correct";
            numberCorrect++;
            numberCorrectSoFar++;
            numberAttempted++;
            if(numberCorrectSoFar==3)
            {
                timer.GetComponent<assemblyTimer>().addTime();
                numberCorrectSoFar = 0;
            }
            
            isToySpawned = false;
        }
        else
        {
            print("Incorrect");
            feedbackText.text = "Incorrect";
            numberAttempted++;
        }
        showChoice1.text = "";
        showChoice2.text = "";
        showChoice3.text = "";
        nextEquation();
    }

    
}
