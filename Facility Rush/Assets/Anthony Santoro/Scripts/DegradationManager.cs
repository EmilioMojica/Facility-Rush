﻿using System.Collections;
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

    void Start()
    {
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

    public void chuteCalculate()
    {
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
        aFill = aCorrect / aAttempted;
        print("aFill is equal to: " + aFill);
    }
}
