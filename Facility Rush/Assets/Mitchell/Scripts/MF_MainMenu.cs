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

    void OnClickBegin()
    {
        PreviousScene = SceneManager.GetActiveScene().name;
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
