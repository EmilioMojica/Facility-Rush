using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MF_MainMenu : MonoBehaviour
{
    private Animator anim;
    public static string PreviousScene = "";
    public GameObject options;
    private PauseGame Pause;
    private PauseBool pb;

    void Start()
    {
        anim = GetComponent<Animator>();
        PreviousScene = SceneManager.GetActiveScene().name;
        Pause = FindObjectOfType<PauseGame>();
        pb = FindObjectOfType<PauseBool>();

        if (pb.backToMenu)
        {
            anim.Play("UI Test Animation 2 Idle");
        }
    }

    public void TurnOnOptions()
    {
        options.SetActive(true);
    }

    public void OnClickBegin()
    {
        PreviousScene = SceneManager.GetActiveScene().name;
        anim.SetTrigger("MainMenuSceneTransition");
        //Invoke("TransitionToGradeSelection", 1);
    }

    public void OnClickOptions()
    {
        anim.SetTrigger("OpenOptions");
        //PreviousScene = SceneManager.GetActiveScene().name;
        //anim.SetTrigger("MainMenuSceneTransition");
        //Invoke("TransitionToOptions", 1);
        //if(OptionsPanel.activeSelf == true)
        //{
        //    return;
        //}
        //else if(OptionsPanel.activeSelf == false)
        //{
        //    OptionsPanel.SetActive(true);
        //}
    }

    public void OnClickExitOptions()
    {
        anim.SetTrigger("CloseOptions");
        //OptionsPanel.SetActive(false);
    }

    public void IGOnClickOptions()
    {
        anim.SetTrigger("IGOpenOptions");
    }

    public void IGOnClickExitOptions()
    {
        anim.SetTrigger("IGCloseOptions");
    }

    public void ScoreBoardOpen()
    {
        anim.SetTrigger("ScoreOpen");
    }

    public void ScoreBoardClose()
    {
        anim.SetTrigger("ScoreClose");
    }

    //public void TransitionToGradeSelection()
    //{
    //    SceneManager.LoadScene("Grade Selection");
    //}

    //public void TransitionToOptions()
    //{
    //    SceneManager.LoadScene("Options Menu");
    //}
}
