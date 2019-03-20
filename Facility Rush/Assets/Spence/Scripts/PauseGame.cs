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
}
