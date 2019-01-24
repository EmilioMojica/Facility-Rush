using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour
{
    [SerializeField] GameObject optionsCanvas;

    void Start()
    {
        optionsCanvas.SetActive(false);
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene("Grade Selection");
    }

    public void OnClickOptions()
    {
        optionsCanvas.SetActive(true);
    }

    public void OnClickBackOptions()
    {
        optionsCanvas.SetActive(false);
    }
}
