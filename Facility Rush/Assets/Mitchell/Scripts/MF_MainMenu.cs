using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MF_MainMenu : MonoBehaviour
{
    public static string PreviousScene = "";
    public GameObject backButton;

    private Animator anim;
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

    public void TurnOnOptions()  //Called in "Close Credits" animation clip's last frame
    {

        DisableButtons();
    }

    public void OnClickBegin()
    {
        PreviousScene = SceneManager.GetActiveScene().name;
        anim.SetTrigger("MainMenuSceneTransition");

        DisableButtons();
    }

    public void OnClickCredits()
    {
        anim.SetTrigger("OpenCredits");

        DisableButtons();
    }

    public void OnClickCloseCredits()
    {
        Debug.Log("~~~~~~~~~~~");

        anim.SetTrigger("CloseCredits");

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
        backButton.SetActive(false);

        DisableButtons();
    }

    public void IGOnClickExitOptions()
    {
        anim.SetTrigger("IGCloseOptions");
        backButton.SetActive(true);

        DisableButtons();
    }

    public void ScoreBoardOpen()
    {
        anim.SetTrigger("ScoreOpen");
        backButton.SetActive(false);

        DisableButtons();
    }

    public void ScoreBoardClose()
    {
        anim.SetTrigger("ScoreClose");
        backButton.SetActive(true);

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
        Invoke("CallButtonBack",1f);
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
        backButton.SetActive(false);

        DisableButtons();
    }

    public void TutorialBoardClose()
    {
        anim.SetTrigger("TutorialClose");
        backButton.SetActive(true);

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

    public void gowithMouseCursor()
    {
        if (Input.GetMouseButton(0))
        {
            transform.position = Input.mousePosition;
        }
    }
}
