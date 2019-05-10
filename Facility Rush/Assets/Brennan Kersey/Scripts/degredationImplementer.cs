using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class degredationImplementer : MonoBehaviour
{
   
    [SerializeField]private float gameTimer;
    [SerializeField]private Slider assemblySliderValue;
    [SerializeField] private Slider chutesSliderValue;
    [SerializeField] private Slider kartCuroSliderValue;
    [SerializeField] private Slider pipeGyroSliderValue;
    [SerializeField] private float rateOfDegradation;
    [SerializeField] private int secondsTillNextDegredation;

    [SerializeField] private int divisionNumber;
    private bool isAddingToBar;
    private GameObject[] degredationManagerHolder = new GameObject[2];
    [SerializeField] private float delayTime;
    [SerializeField] private scoreManager theScoreManger;
    private bool degredationStart;
    // Start is called before the first frame update
    public void unravelTime()
    {
        int currenthour = System.DateTime.Now.Hour;
        int currentminute = System.DateTime.Now.Minute;
        int currentday = System.DateTime.Now.Day;

        int currentmonth = System.DateTime.Now.Month;
        int currentyear = System.DateTime.Now.Year;



        int hourDifference = Mathf.Abs(currenthour - PlayerPrefs.GetInt("hour"));
        int minuteDifference = currentminute - PlayerPrefs.GetInt("minutes");
        if(minuteDifference<0)
        {
            hourDifference = 0;
        }
        int dayDifference = Mathf.Abs(currentday - PlayerPrefs.GetInt("day"));
        int monthDifference = Mathf.Abs(currentmonth - PlayerPrefs.GetInt("month"));
        int yearDifference = Mathf.Abs(currentyear - PlayerPrefs.GetInt("year"));

        int amountToSubtract = hourDifference + (dayDifference * 24) + (monthDifference * 24 * 30) + (yearDifference * 24 * 30 * 365);


        assemblySliderValue.value -= (float)(amountToSubtract *.01);
        chutesSliderValue.value -= (float)(amountToSubtract * .01);
        kartCuroSliderValue.value -= (float)(amountToSubtract * .01);
        pipeGyroSliderValue.value -= (float)(amountToSubtract * .01);
    }
    public void startDegrading()
    {
        degredationStart = true;
    }
    void Start()
    {
        degredationStart = false;
        string currentDateTime=System.DateTime.Now.ToString("HH:mm dd MMMM, yyyy");
        isAddingToBar = false;
        bool playedBefore = PlayerPrefs.HasKey("currentAssemblySliderValue");

        if(playedBefore==false)
        {
            assemblySliderValue.value=1f;
            chutesSliderValue.value=1f;
            kartCuroSliderValue.value= 1f;
            pipeGyroSliderValue.value = 1f;        
            saveCurrentStateOfBars();

            PlayerPrefs.SetInt("hour", System.DateTime.Now.Hour);
            PlayerPrefs.SetInt("minutes", System.DateTime.Now.Minute);
            PlayerPrefs.SetInt("day",System.DateTime.Now.Day);
            PlayerPrefs.SetInt("month", System.DateTime.Now.Month);
            PlayerPrefs.SetInt("year", System.DateTime.Now.Year);


        }
        else
        {
            assemblySliderValue.value = PlayerPrefs.GetFloat("currentAssemblySliderValue");
            chutesSliderValue.value = PlayerPrefs.GetFloat("currentChutesSliderValue");
            kartCuroSliderValue.value = PlayerPrefs.GetFloat("currentKartCuroSliderValue");
            pipeGyroSliderValue.value = PlayerPrefs.GetFloat("currentPipeGyroSliderValue");
            unravelTime();
            degredationManagerHolder = GameObject.FindGameObjectsWithTag("degredationManager");
            if (GameObject.FindGameObjectsWithTag("degredationManager").Length==2)
            {
                deleteExtraManager();
            }

            StartCoroutine(addAmountToBar());
        }
    }
    public void deleteExtraManager()
    {

        Destroy(degredationManagerHolder[1]);
    }

    public IEnumerator addAmountToBar()
    {
        isAddingToBar = true;
        int timesSectionIsAdded = 0;
        yield return new WaitForSeconds(delayTime);
        DegradationManager manager = degredationManagerHolder[0].GetComponent<DegradationManager>();
        if (manager.KartCuro == true)
        {
            while (timesSectionIsAdded != divisionNumber)
            {
                yield return new WaitForSeconds(delayTime);
                kartCuroSliderValue.value += (manager.aFill / divisionNumber);
                timesSectionIsAdded++;
            }
        }
        if (manager.assLine == true)
        {
            while (timesSectionIsAdded != divisionNumber)
            {
                yield return new WaitForSeconds(delayTime);
                assemblySliderValue.value += (manager.aFill / divisionNumber);
                timesSectionIsAdded++;
            }
        }
        if (manager.Chutes == true)
        {
            while (timesSectionIsAdded != divisionNumber)
            {
                yield return new WaitForSeconds(delayTime);
                chutesSliderValue.value += (manager.aFill / divisionNumber);
                timesSectionIsAdded++;
            }
        }
        if (manager.pipGyro == true)
        {
            while (timesSectionIsAdded != divisionNumber)
            {
                yield return new WaitForSeconds(delayTime);
                pipeGyroSliderValue.value += (manager.aFill / divisionNumber);
                timesSectionIsAdded++;
            }
        }
        isAddingToBar = false;
        degredationStart = true;
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAddingToBar==false)
        {
            gameTimer += Time.deltaTime;
            if (gameTimer > 3600 && degredationStart==true)
            {
                assemblySliderValue.value -= rateOfDegradation;
                chutesSliderValue.value -= rateOfDegradation;
                kartCuroSliderValue.value -= rateOfDegradation;
                pipeGyroSliderValue.value -= rateOfDegradation;
                gameTimer = 0.0f;


            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                restoreBarAmounts();
                deleteTutorialKeys();
                deleteValues();
            }
        }
    }
    public void restoreBarAmounts()
    {
        assemblySliderValue.value = 1.0f;
        chutesSliderValue.value = 1.0f;
        kartCuroSliderValue.value = 1.0f;
        pipeGyroSliderValue.value = 1.0f;
    }
    public void implementAdditionToBar(float amountToAdd, int indexOfGameRecentlyPlayed)
    {
        switch(indexOfGameRecentlyPlayed)
        {
            case 0:
                assemblySliderValue.value += amountToAdd;
                break;

            case 1:
                chutesSliderValue.value += amountToAdd;
                break;

            case 2:
                kartCuroSliderValue.value += amountToAdd;
                break;

            case 3:
                pipeGyroSliderValue.value += amountToAdd;
                break;

            default:
                break;
        }
    }

    public void saveCurrentStateOfBars()
    {
        PlayerPrefs.SetFloat("currentAssemblySliderValue",assemblySliderValue.value);
        PlayerPrefs.SetFloat("currentChutesSliderValue", chutesSliderValue.value);
        PlayerPrefs.SetFloat("currentKartCuroSliderValue", kartCuroSliderValue.value);
        PlayerPrefs.SetFloat("currentPipeGyroSliderValue", pipeGyroSliderValue.value);
        theScoreManger.saveScores();
    }

    public void deleteValues()
    {
        PlayerPrefs.DeleteKey("currentAssemblySliderValue");
        PlayerPrefs.DeleteKey("currentChutesSliderValue");
        PlayerPrefs.DeleteKey("currentKartCuroSliderValue");
        PlayerPrefs.DeleteKey("currentPipeGyroSliderValue");
    }

    public void deleteTutorialKeys()
    {
        PlayerPrefs.DeleteKey("AssemblyTutorialComplete");
        PlayerPrefs.DeleteKey("ChutesTutorialComplete");
        PlayerPrefs.DeleteKey("KartCuroTutorialComplete");
        PlayerPrefs.DeleteKey("PipeGyroTutorialComplete");

    }
}
