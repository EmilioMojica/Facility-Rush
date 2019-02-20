using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour
{
    [SerializeField] GameObject optionsPanel;

    void Start()
    {
        optionsPanel.SetActive(false);
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene("Grade Selection");
    }

    public void OnClickOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void OnClickBackOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void OnClickCaCuro()
    {
        SceneManager.LoadScene("cacuro");
    }

    public void OnClickAssemblyLine()
    {
        SceneManager.LoadScene("Assembly Line");
    }

    public void OnClickChutes()
    {
        Debug.Log("button clicked");
        SceneManager.LoadScene("Chutes");
    }

    public void OnClickPipeGyro()
    {
        SceneManager.LoadScene("Pipes 1");
    }
}
