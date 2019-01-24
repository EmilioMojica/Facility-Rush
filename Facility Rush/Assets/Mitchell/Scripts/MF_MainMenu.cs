using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MF_MainMenu : MonoBehaviour
{
    public static string PreviousScene = "";

    void start()
    {
        PreviousScene = SceneManager.GetActiveScene().name;
    }

    public void OnClickBegin()
    {
        PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Grade Selection");
    }

    public void OnClickOptions()
    {
        PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Options Menu"); 
    }

    public void OnClickExitOptions()
    {
        SceneManager.LoadScene(PreviousScene);
    }
}
