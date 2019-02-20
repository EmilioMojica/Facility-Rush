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
        anim.SetTrigger("SceneTransition");
        //SceneManager.LoadScene("Level Select");
        Invoke("TransitionToLevelSelect", 1);
    }
    public void OnClick1stGrade()
    {
        anim.SetTrigger("SceneTransition");
        //SceneManager.LoadScene("Level Select");
        Invoke("TransitionToLevelSelect", 1);
    }
    public void OnClick2ndGrade()
    {
        anim.SetTrigger("SceneTransition");
        //SceneManager.LoadScene("Level Select");
        Invoke("TransitionToLevelSelect", 1);
    }
    public void OnClick3rdGrade()
    {
        anim.SetTrigger("SceneTransition");
        //SceneManager.LoadScene("Level Select");
        Invoke("TransitionToLevelSelect", 1);
    }
    public void OnClick4thGrade()
    {
        anim.SetTrigger("SceneTransition");
        //SceneManager.LoadScene("Level Select");
        Invoke("TransitionToLevelSelect", 1);
    }
    public void OnClick5thGrade()
    {
        anim.SetTrigger("SceneTransition");
        //SceneManager.LoadScene("Level Select");
        Invoke("TransitionToLevelSelect", 1);
    }
    public void OnClickBack()
    {
        SceneManager.LoadScene("Ford_Test");
    }

    public void TransitionToLevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }
}
