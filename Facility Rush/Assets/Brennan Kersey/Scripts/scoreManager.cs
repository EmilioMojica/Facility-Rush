using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour
{
    private int assemblyScore;
    private int kakuroScore;
    private int chutesScore;
    private int pipeGyroScore;
    private int combinationScore;

    [SerializeField] private Text assemblyScoreText;
    [SerializeField] private Text kakuroScoreText;
    [SerializeField] private Text chutesScoreText;
    [SerializeField] private Text pipeGyroScoreText;
    [SerializeField] private Text combinationScoreText;

    [SerializeField]private DegradationManager manager;

    [SerializeField]GameObject scorePanel;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("degredationManager").GetComponent<DegradationManager>();
        print(manager.gameObject.name);
        bool playedBefore = PlayerPrefs.HasKey("assemblyHighScore");

        if(playedBefore==false)
        {
            assemblyScore = 0;
            kakuroScore = 0;
            chutesScore = 0;
            pipeGyroScore = 0;
            combinationScore = 0;

            PlayerPrefs.SetInt("assemblyHighScore",assemblyScore);
            PlayerPrefs.SetInt("kakuroHighScore",kakuroScore);
            PlayerPrefs.SetInt("chutesHighScore",chutesScore);
            PlayerPrefs.SetInt("pipeGyroHighScore",pipeGyroScore);
            PlayerPrefs.SetInt("combinationScore", combinationScore);

            assemblyScoreText.text = assemblyScore + "";
            kakuroScoreText.text = kakuroScore + "";
            chutesScoreText.text = chutesScore + "";
            pipeGyroScoreText.text = pipeGyroScore + "";
            combinationScoreText.text = combinationScore + "";
        }
        else
        {
            assemblyScore = PlayerPrefs.GetInt("assemblyHighScore");
            print("this is assembly line's current highscore"+ assemblyScore);
            kakuroScore = PlayerPrefs.GetInt("kakuroHighScore");
            print("this is kartkuro line's current highscore" + kakuroScore);
            chutesScore = PlayerPrefs.GetInt("chutesHighScore");
            print("this is chutes line's current highscore" + chutesScore);
            pipeGyroScore = PlayerPrefs.GetInt("pipeGyroHighScore");
            print("this is combination's current highscore" + kakuroScore);
            combinationScore = PlayerPrefs.GetInt("combinationScore");

            assemblyScoreText.text = assemblyScore + "";
            kakuroScoreText.text = kakuroScore + "";
            chutesScoreText.text = chutesScore + "";
            pipeGyroScoreText.text = pipeGyroScore + "";
            combinationScoreText.text = (assemblyScore+kakuroScore+chutesScore+pipeGyroScore) + "";
        }
    }


    public void activateOrDeactivate(int stateOfScoreboard)
    {
        switch(stateOfScoreboard)
        {
            case 0:
                scorePanel.SetActive(false);
                break;
            case 1:
                scorePanel.SetActive(true);
                break;
        }
    }
    public void redoKeys()
    {
        PlayerPrefs.DeleteKey("assemblyHighScore");
        PlayerPrefs.DeleteKey("kakuroHighScore");
        PlayerPrefs.DeleteKey("chutesHighScore");
        PlayerPrefs.DeleteKey("pipeGyroHighScore");
        PlayerPrefs.DeleteKey("combinationScore");
    }

    public void saveScores()
    {
        PlayerPrefs.SetInt("assemblyHighScore", assemblyScore);
        PlayerPrefs.SetInt("kakuroHighScore", kakuroScore);
        print("This the current value of kakuro's high score being called from the score manager: " + kakuroScore);
        PlayerPrefs.SetInt("chutesHighScore", chutesScore);
        PlayerPrefs.SetInt("pipeGyroHighScore", pipeGyroScore);
        PlayerPrefs.SetInt("combinationScore", combinationScore);
        PlayerPrefs.Save();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            redoKeys();
        }
    }

    public void settheCurrentHighScore(int indexOfGamePlayed)
    {
        int scoreFromManager = 0;
        switch(indexOfGamePlayed)
        {
            case 0:
                scoreFromManager = manager.GetScoreOfRecentlyPlayedGame();
                if (scoreFromManager>assemblyScore)
                {
                    assemblyScore=scoreFromManager;
                    assemblyScoreText.text = assemblyScore + "";
                }
                break;

            case 1:
                scoreFromManager = manager.GetScoreOfRecentlyPlayedGame();
                if (scoreFromManager > chutesScore)
                {
                    chutesScore = scoreFromManager;
                    chutesScoreText.text = chutesScore + "";
                }
                break;

            case 2:
                scoreFromManager = manager.GetScoreOfRecentlyPlayedGame();
                if (scoreFromManager > kakuroScore)
                {
                    kakuroScore = scoreFromManager;
                    kakuroScoreText.text = kakuroScore + "";
                }
                break;

            case 3:
                scoreFromManager = manager.GetScoreOfRecentlyPlayedGame();
                if (scoreFromManager > pipeGyroScore)
                {
                    pipeGyroScore = scoreFromManager;
                    pipeGyroScoreText.text = pipeGyroScore + "";
                }
                
                break;

            default:
                print("No addition to bar");
                break;
        }


    }
}
