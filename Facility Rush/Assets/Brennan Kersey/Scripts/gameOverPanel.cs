using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Author: Brennan Kersey
 * Spript Purpose: 
 */
public class gameOverPanel : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] fireworkPoints;
    [SerializeField] private GameObject congratulationsText; // Text field for congratulating the player after the level if complete
    [SerializeField] private GameObject newHighScoreText;     // Text field for indicating that the player has achieved a new high score
    [SerializeField] private Text scoreOnGameOverPanel; // Text field for displaying the score that the player received 
    [SerializeField] private GameObject congratulationsUIElement;
    [SerializeField] private GameObject goodJobUIElement;

    // Start is called before the first frame update
    void Start()
    {
        //fireworkPoints[0].Play();
        StartCoroutine(playRandomParticles());
    }

    // Update is called once per frame
    void Update()
    {
        //fireworkPoints[0].Play();
    }
    public void crossCheckScores(int gamePlayed, int recentHighScore) // method used to analyze the score the player received and congratulate the player
    {                                                                   //if a new highscore is achieved by turning on the text varaible to congratulate them
        scoreOnGameOverPanel.text += recentHighScore.ToString();            // adds the text for the player score to the player score display variable

        switch(gamePlayed)      // A switch case used to determine which game was played
        {
            case 0:
                int currentAssemblyLineHighScore = PlayerPrefs.GetInt("assemblyHighScore"); // an integer that represents the current high score for "Assembly Line" minigame
                scoreOnGameOverPanel.text = "" + recentHighScore;
                if(recentHighScore>currentAssemblyLineHighScore) // if statement that checks to see if the Assembly Line highscore is lower than what the player got
                {
                    activateNewHighScoreFeedback(); // activate the Game Object for the "congratulations on new high score" text
                }
                else
                {
                    goodJobUIElement.SetActive(true);
                }
                break;

            case 1:
                int currentKartcuroHighScore = PlayerPrefs.GetInt("kakuroHighScore"); // an integer that represents the current high score for "Kartcuro" minigame
                scoreOnGameOverPanel.text = "" + recentHighScore;
                if (recentHighScore > currentKartcuroHighScore) // if statement that checks to see if the Kartcuro highscore is lower than what the player got
                {
                    activateNewHighScoreFeedback(); // activate the Game Object for the "congratulations on new high score" text
                }
                else
                {
                    goodJobUIElement.SetActive(true);
                }
                break;

            case 2:
                int currentChutesHighScore = PlayerPrefs.GetInt("chutesHighScore"); // an integer that represents the current high score for "Chutes" minigame
                scoreOnGameOverPanel.text = "" + recentHighScore;
                print("This is the currentChutesHighScore: "+ currentChutesHighScore);
                if (recentHighScore > currentChutesHighScore) // if statement that checks to see if the Chutes highscore is lower than what the player got
                {
                    activateNewHighScoreFeedback(); // activate the Game Object for the "congratulations on new high score" text
                }
                else
                {
                    goodJobUIElement.SetActive(true);
                }
                break;
            case 3:
                int currentPipeGyroHighScore = PlayerPrefs.GetInt("pipeGyroHighScore"); // an integer that represents the current high score for "Pipe Gyro" minigame
                scoreOnGameOverPanel.text = "" + recentHighScore;
                if (recentHighScore > currentPipeGyroHighScore) // if statement that checks to see if the Pipe Gyro highscore is lower than what the player got
                {
                    activateNewHighScoreFeedback(); // activate the Game Object for the "congratulations on new high score" text
                }
                else
                {
                    goodJobUIElement.SetActive(true);
                }
                break;
        }
    }
    public void activateNewHighScoreFeedback() // method used to activate the "congratulations on new high score" text
    {
        newHighScoreText.SetActive(true); // set the game object that holds the text to true so that it is visible.
        congratulationsText.SetActive(true);
    }

    IEnumerator playRandomParticles()
    {
        int index = Random.Range(0, 6);
        while (this.gameObject.activeInHierarchy==true)
        {
            int nextIndex = Random.Range(0, 6);
            fireworkPoints[index].gameObject.SetActive(true);
            AudioManager.instance.soundAudioSource2.clip = AudioManager.instance.soundClip[9];
            AudioManager.instance.soundAudioSource2.Play();
            fireworkPoints[index].Play();
            yield return new WaitForSeconds(1.5f);
           // fireworkPoints[index].gameObject.SetActive(false);
            if(index==nextIndex)
            {
                nextIndex += 1;
                if(nextIndex==6)
                {
                    nextIndex = 0;
                }
            }
            index = nextIndex;
            yield return null;
        }
    }
}
