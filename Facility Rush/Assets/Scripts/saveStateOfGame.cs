using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class saveStateOfGame : MonoBehaviour
{
    public Image healthImage;
    public Image healthImage2;
    public Image healthImage3;
    public Image healthImage4;

    public float waitTime=30.0f;

    public float healthAmount1;
    public float healthAmount2;
    public float healthAmount3;
    public float healthAmount4;

    public Text healthbarOne;
    public Text healthbarTwo;
    public Text healthbarThree;
    public Text healthbarFour;

    public Text assenmblyHighscore;
    public Text kakuroHighscore;
    public Text chutesHighscore;
    public Text pipeGyroHighscore;

    public int assemblyHighScore;
    public int kakuroHighScore;
    public int chutesHighScore;
    public int pipeGyroHighScore;

    public int assemblyCurrentHighScore;
    public int kakuroCurrentHighScore;
    public int chutesCurrentHighScore;
    public int pipeGyroCurrentHighScore;

    // Use this for initialization
    void Start ()
    {
        checkForHighScore();
        if (PlayerPrefs.HasKey("healthOne") && PlayerPrefs.HasKey("healthTwo") && PlayerPrefs.HasKey("healthThree") && PlayerPrefs.HasKey("healthFour"))
        {
            print("prefs exist");
            healthImage.fillAmount = PlayerPrefs.GetFloat("healthOne");
            healthImage2.fillAmount = PlayerPrefs.GetFloat("healthTwo");
            healthImage3.fillAmount = PlayerPrefs.GetFloat("healthThree");
            healthImage4.fillAmount = PlayerPrefs.GetFloat("healthFour");

            healthbarOne.text = (int)(healthImage.fillAmount * 100) + "/" + 100 + "";
            healthbarTwo.text = (int)(healthImage2.fillAmount * 100) + "/" + 100 + ""; 
            healthbarThree.text = (int)(healthImage3.fillAmount * 100) + "/" + 100 + "";
            healthbarFour.text= (int)(healthImage4.fillAmount * 100) + "/" + 100 + "";


        }
        else
        {
            print("No prefs exist");
            healthAmount1 = 100f;
            healthAmount2 = 100f;
            healthAmount3 = 100f;
            healthAmount4 = 100f;

            healthbarOne.text = (int)(healthImage.fillAmount*100) + "/" +100 + "";
            healthbarTwo.text = (int)(healthImage2.fillAmount *100) +"/"+ 100 + "";
            healthbarThree.text = (int)(healthImage3.fillAmount*100) + "/" + 100 + "";
            healthbarFour.text = (int)(healthImage4.fillAmount * 100) + "/" + 100 + "";

        }
        
	}
	public void checkForHighScore()
    {
        if(PlayerPrefs.HasKey("assemblyHighScore")&& PlayerPrefs.HasKey("chutesHighScore")&& PlayerPrefs.HasKey("pipeGyroHighScore")&& PlayerPrefs.HasKey("kakuroHighScore"))
        {
            changeHighscore("assemblyHighScore", PlayerPrefs.GetInt("recentAssemblyHighScore"));
            changeHighscore("kakuroHighScore", PlayerPrefs.GetInt("recentKakuroHighScore"));
            changeHighscore("chutesHighScore", PlayerPrefs.GetInt("recentChutesHighScore"));
            changeHighscore("pipeGyroHighScore", PlayerPrefs.GetInt("recentPipeGyroHighScore"));

            assemblyHighScore= PlayerPrefs.GetInt("assemblyHighScore");
            kakuroHighScore= PlayerPrefs.GetInt("kakuroHighScore");
            chutesHighScore = PlayerPrefs.GetInt("chutesHighScore");
            pipeGyroHighScore = PlayerPrefs.GetInt("pipeGyroHighScore");


            assenmblyHighscore.text="Score: "+assemblyHighScore+"";
            kakuroHighscore.text = "Score: " + kakuroHighScore + "";
            chutesHighscore.text = "Score: " + chutesHighScore + "";
            pipeGyroHighscore.text = "Score: " + pipeGyroHighScore + "";

            
        }

        else
        {
            PlayerPrefs.SetInt("assemblyHighScore", 0);
            PlayerPrefs.SetInt("kakuroHighScore", 0);
            PlayerPrefs.SetInt("chutesHighScore", 0);
            PlayerPrefs.SetInt("pipeGyroHighScore", 0);

            PlayerPrefs.SetInt("recentAssemblyHighScore", 0);
            PlayerPrefs.SetInt("recentKakuroHighScore", 0);
            PlayerPrefs.SetInt("recentChutesHighScore", 0);
            PlayerPrefs.SetInt("recentPipeGyroHighScore", 0);

            assenmblyHighscore.text = PlayerPrefs.GetInt("assemblyHighScore") + "";
            kakuroHighscore.text = PlayerPrefs.GetInt("kakuroHighScore") + "";
            chutesHighscore.text = PlayerPrefs.GetInt("chutesHighScore") + "";
            pipeGyroHighscore.text = PlayerPrefs.GetInt("pipeGyroHighScore") + "";
        }


    }
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            healthImage.fillAmount -= .1f;
            healthAmount1 -= 10f;
            healthbarOne.text = (int)(healthImage.fillAmount * 100) + "/" + 100 + "";
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            healthImage2.fillAmount -= .1f;
            healthAmount2 -= 10f;
            healthbarTwo.text = (int)(healthImage2.fillAmount * 100) + "/" + 100 + "";
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            healthImage3.fillAmount -= .1f;
            healthAmount3 -= 10f;
            healthbarThree.text = (int)(healthImage3.fillAmount * 100) + "/" + 100 + "";
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            healthImage4.fillAmount -= .1f;
            healthAmount4 -= 10f;
            healthbarFour.text = (int)(healthImage4.fillAmount * 100) + "/" + 100 + "";
        }
    }


    public void SaveGame()
    {
        print("Saved");
        PlayerPrefs.SetFloat("healthOne", healthImage.fillAmount);
        PlayerPrefs.SetFloat("healthTwo", healthImage2.fillAmount);
        PlayerPrefs.SetFloat("healthThree", healthImage3.fillAmount);
        PlayerPrefs.SetFloat("healthFour", healthImage4.fillAmount);
        PlayerPrefs.Save();
    }

    public void Reset()
    {
        PlayerPrefs.SetFloat("healthOne", 1f);
        PlayerPrefs.SetFloat("healthTwo", 1f);
        PlayerPrefs.SetFloat("healthThree", 1f);
        PlayerPrefs.SetFloat("healthFour",1f);
    }


    public void changeHighscore(string key, int newScore)
    {
        int currentHighScore = PlayerPrefs.GetInt(key);

        if (currentHighScore<newScore)
        {
            PlayerPrefs.SetInt(key, newScore);
        }


    }

}
