using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GradeSelectMenu : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnClickKindergarten()
    {
        //anim.ResetTrigger("MainMenuSceneTransition");
        anim.SetTrigger("GradeSelectSceneTransition");
        //SceneManager.LoadScene("Level Select");
        //Invoke("TransitionToLevelSelect", 1);
    }
    public void OnClick1stGrade()
    {
        //anim.ResetTrigger("MainMenuSceneTransition");
        anim.SetTrigger("GradeSelectSceneTransition");
        //SceneManager.LoadScene("Level Select");
        //Invoke("TransitionToLevelSelect", 1);
    }
    public void OnClick2ndGrade()
    {
        //anim.ResetTrigger("MainMenuSceneTransition");
        anim.SetTrigger("GradeSelectSceneTransition");
        //SceneManager.LoadScene("Level Select");
        //Invoke("TransitionToLevelSelect", 1);
    }
    public void OnClick3rdGrade()
    {
        //anim.ResetTrigger("MainMenuSceneTransition");
        anim.SetTrigger("GradeSelectSceneTransition");
        //SceneManager.LoadScene("Level Select");
        //Invoke("TransitionToLevelSelect", 1);
    }
    public void OnClick4thGrade()
    {
        //anim.ResetTrigger("MainMenuSceneTransition");
        anim.SetTrigger("GradeSelectSceneTransition");
        //SceneManager.LoadScene("Level Select");
        //Invoke("TransitionToLevelSelect", 1);
    }
    public void OnClick5thGrade()
    {
        //anim.ResetTrigger("MainMenuSceneTransition");
        anim.SetTrigger("GradeSelectSceneTransition");
        //SceneManager.LoadScene("Level Select");
        //Invoke("TransitionToLevelSelect", 1);
    }
    public void OnClickBack()
    {
        //anim.ResetTrigger("MainMenuSceneTransition");
        anim.SetTrigger("BackToMainMenuSceneTransition");
        //SceneManager.LoadScene("Ford_Test");
    }

    public void OnBackToGradeSelect()
    {
        anim.SetTrigger("BackToGradeSelectSceneTransition");
    }

    public void TransitionToLevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }
}
