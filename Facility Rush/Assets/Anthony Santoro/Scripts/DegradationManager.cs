using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DegradationManager : MonoBehaviour
{
    public float aAttempted;
    public float aCorrect;

    public float aFill;
    public float maxFill = 15f;
    private Scene currentScene;

    public bool KartCuro = false, assLine = false, Chutes = false, pipGyro = false;
    [SerializeField] private int currentlyPlayedGameScore;

    // Start is called before the first frame update

    public void gameHasBeenPlayed(int indexOfGame)
    {
        switch(indexOfGame)
        {
            case 0:
                pipGyro = true;
                break;
            case 1:
                assLine = true;
                break;
            case 2:
                Chutes = true;
                break;
            case 3:
                KartCuro = true;
                break;
        }
    }

    public int GetScoreOfRecentlyPlayedGame()
    {
        return currentlyPlayedGameScore;
    }

    public void setScoreOfRecentPlayedGame(int scoreValueToSet)
    {
        currentlyPlayedGameScore = scoreValueToSet;
    }
    void Start()
    {
        currentlyPlayedGameScore = 0;
        //Get gamemode that was played

        //Get the amount of questions answered in a session
        //Get the amount of quetions answered correctly in a session
        DontDestroyOnLoad(this.gameObject);
        currentScene = SceneManager.GetActiveScene();
        if(currentScene.name.Equals("Level Select"))
        {
            print("We are in Level Select");
        }
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    public void cartkuroCalculation()
    {
        if (aAttempted == 0)
        {
            Debug.Log("78787887878787878778878877878787");
            return;
        }
            if (aAttempted < 5)
        {        
            aFill = (aCorrect / aAttempted) / 5;
        }
        else
        {
            aFill = aCorrect / aAttempted;
        }

        print("aFill is equal to: " + aFill);
    }

    public void assemblyCalulate()
    {
        if (aAttempted == 0)
        {
            Debug.Log("78787887878787878778878877878787");
            return;
        }
        if (aAttempted < 10)
        {
            aFill = (aCorrect / aAttempted) / 10;
        }
        else
        {
            aFill = aCorrect / aAttempted;
        }
        if(float.IsNaN(aFill))
        {
            aFill = 0;
        }
        print("aFill is equal to: " + aFill);
    }

    public void chuteCalculate()
    {
        if (aAttempted == 0)
        {
            Debug.Log("78787887878787878778878877878787");
            return;
        }
        if (aAttempted < 10)
        {
            aFill = (aCorrect / aAttempted) / 10;
        }
        else
        {
            aFill = aCorrect / aAttempted;
        }
        print("aFill is equal to: " + aFill);
    }

    public void pipeCalculate()
    {
        if (aAttempted == 0)
        {
            Debug.Log("78787887878787878778878877878787");
            return;
        }
        aFill = aCorrect / aAttempted;
        print("aFill is equal to: " + aFill);
    }

    public void setAllBoolSToFalse()
    {
        KartCuro = false;
        assLine = false;
        Chutes = false; 
        pipGyro = false;
    }
}
