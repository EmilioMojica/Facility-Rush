using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MF_MainMenu : MonoBehaviour
{
    private Animator anim;
    public static string PreviousScene = "";
    public GameObject OptionsPanel;
    void Start()
    {
        OptionsPanel.SetActive(false);
        anim = GetComponent<Animator>();
        PreviousScene = SceneManager.GetActiveScene().name;
    }

    public void OnClickBegin()
    {
        PreviousScene = SceneManager.GetActiveScene().name;
        anim.SetTrigger("MainMenuSceneTransition");
        //Invoke("TransitionToGradeSelection", 1);
    }

    public void OnClickOptions()
    {
        PreviousScene = SceneManager.GetActiveScene().name;
        //anim.SetTrigger("MainMenuSceneTransition");
        //Invoke("TransitionToOptions", 1);
        if(OptionsPanel.activeSelf == true)
        {
            return;
        }
        else if(OptionsPanel.activeSelf == false)
        {
            OptionsPanel.SetActive(true);
        }
    }

    public void OnClickExitOptions()
    {
        OptionsPanel.SetActive(false);
    }

    public void TransitionToGradeSelection()
    {
        SceneManager.LoadScene("Grade Selection");
    }

    public void TransitionToOptions()
    {
        SceneManager.LoadScene("Options Menu");
    }
}
