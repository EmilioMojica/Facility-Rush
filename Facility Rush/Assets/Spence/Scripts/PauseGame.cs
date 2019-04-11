using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject optionsPanel;

    private LevelChanger lvl;

    private PauseBool pb;

    // Start is called before the first frame update
    void Start()
    {
        optionsPanel.SetActive(false);
        lvl = FindObjectOfType<LevelChanger>();
        pb = FindObjectOfType<PauseBool>();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void MainMenu()
    {
        Debug.Log("MainMenu pressed");
        pb.backToMenu = true;
        Time.timeScale = 1f;
        lvl.FadeToLevel(0);
    }

    public void Options()
    {
        optionsPanel.SetActive(true);
    }

    public void OptionsBack()
    {
        optionsPanel.SetActive(false);
    }

    public void replayAssemblyLine()
    {
        lvl.FadeToLevel(3);
    }

    public void replayKartcuro()
    {
        lvl.FadeToLevel(4);
    }

    public void replayChutes()
    {
        Debug.Log("replayChutes pressed");

        lvl.FadeToLevel(5);
    }

    public void replayPipeGyro()
    {
        lvl.FadeToLevel(6);
    }
}
