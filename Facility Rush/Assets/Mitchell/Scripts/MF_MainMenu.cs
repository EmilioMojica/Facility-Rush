using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MF_MainMenu : MonoBehaviour
{
    private Animator anim;
    public static string PreviousScene = "";
    public GameObject options;
    private PauseGame Pause;
    private PauseBool pb;
    private Button[] thisbutton;
    private GameObject[] pS;
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
        DisableButtons();
    }

    public void OnClickBegin()
    {
        PreviousScene = SceneManager.GetActiveScene().name;
        anim.SetTrigger("MainMenuSceneTransition");
        DisableButtons();
    }

    public void OnClickOptions()
    {
        anim.SetTrigger("OpenOptions");
        DisableButtons();
    }

    public void OnClickExitOptions()
    {
        anim.SetTrigger("CloseOptions");
        DisableButtons();
    }

    public void IGOnClickOptions()
    {
        anim.SetTrigger("IGOpenOptions");
        DisableButtons();
    }

    public void IGOnClickExitOptions()
    {
        anim.SetTrigger("IGCloseOptions");
        DisableButtons();
    }

    public void ScoreBoardOpen()
    {
        anim.SetTrigger("ScoreOpen");
        DisableButtons();
    }

    public void ScoreBoardClose()
    {
        anim.SetTrigger("ScoreClose");
        DisableButtons();
    }

    public void DisableButtons()
    {
        thisbutton = FindObjectsOfType<Button>();
        Debug.Log("This button =" + thisbutton);
        for (int i = 0; i < thisbutton.Length; i++)
        {
            thisbutton[i].interactable = false;
        }
        Invoke("CallButtonBack",0.5f);
    }

    private void CallButtonBack()
    {
        for (int i = 0; i < thisbutton.Length; i++)
        {
            thisbutton[i].interactable = true;
        }
    }

    public void TutorialBoardOpen()
    {
        anim.SetTrigger("TutorialOpen");
        DisableButtons();
    }

    public void TutorialBoardClose()
    {
        anim.SetTrigger("TutorialClose");
        DisableButtons();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            pS = GameObject.FindGameObjectsWithTag("Particle");
            for (int p = 0; p < pS.Length; p++)
            {
                pS[p].SetActive(false);
            }
        }
    }
}
