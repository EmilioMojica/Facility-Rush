using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class kickBackToMainMenu : MonoBehaviour
{
    [SerializeField] private float timeTillExit;
    [SerializeField] private PauseGame toMainMenu;
    [SerializeField] private bool clickSubmitted;
    // Start is called before the first frame update
    void Start()
    {
        clickSubmitted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && clickSubmitted==false)
        {
            StopAllCoroutines();
            //toMainMenu.MainMenu();
            clickSubmitted = true;
            if(SceneManager.GetActiveScene().name.Contains("Assembly"))
            {
                toMainMenu.replayAssemblyLine();
            }
            else if(SceneManager.GetActiveScene().name.Contains("cartcuro"))
            {
                toMainMenu.replayKartcuro();
            }
            else if(SceneManager.GetActiveScene().name.Contains("Chutes"))
            {
                toMainMenu.replayChutes();
            }
            else if(SceneManager.GetActiveScene().name.Contains("PipeGyro"))
            {
                toMainMenu.replayPipeGyro();
            }
        }
    }

    public void activateAutoKick(int whichGame)
    {
        StartCoroutine(kickAfterFive(whichGame));
    }

    IEnumerator kickAfterFive(int gameID)
    {
        yield return new WaitForSeconds(timeTillExit);
        switch(gameID)
        {
            case 0:
                toMainMenu.replayAssemblyLine();
                break;
            case 1:
                toMainMenu.replayKartcuro();
                break;
            case 2:
                toMainMenu.replayChutes();
                break;
            case 3:
                toMainMenu.replayPipeGyro();
                break;
        }
    }
}
