
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class LevelChanger : MonoBehaviour
{
    public Animator animator;

    public AudioClip closeClip;
    public AudioClip openClip;
    private AudioSource audioSource;
    public Button[] thisbutton; 
    private int levelToLoad;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("Close");
        audioSource.PlayOneShot(closeClip, 0.5F);
        thisbutton = FindObjectsOfType<Button>();
        Debug.Log("This button ="+ thisbutton);
        for (int i = 0; i < thisbutton.Length; i++)
        {
            thisbutton[i].interactable = false;
        }
        //thisbutton.GetComponent<Button>().interactable = false;
    }

    public void OnFadeComplete()
    {
        print("On Fade Complete called");
        print("The value of levelToLoad: " + levelToLoad);
        switch(levelToLoad)
        {
            case 1:
                bool hasDoneAssemblyTutorial= PlayerPrefs.HasKey("AssemblyTutorialComplete");
                print("The value of hasDoneAssemblyTutorial: "+ hasDoneAssemblyTutorial);
                if(!hasDoneAssemblyTutorial)
                {
                    //PlayerPrefs.SetString("AssemblyTutorialComplete", "true");
                    levelToLoad = 5;
                    print("the if condition worked");
                    SceneManager.LoadScene(levelToLoad);
                }
                break;
            case 2:
                bool hasDoneKartcuroTutorial = PlayerPrefs.HasKey("KartCuroTutorialComplete");
                if (hasDoneKartcuroTutorial == false)
                {
                    PlayerPrefs.SetString("KartCuroTutorialComplete", "true");
                    levelToLoad = 6;
                    SceneManager.LoadScene(levelToLoad);
                }
                break;
            case 3:
                bool hasDoneChutesTutorial = PlayerPrefs.HasKey("ChutesTutorialComplete");
                if (hasDoneChutesTutorial == false)
                {
                    PlayerPrefs.SetString("ChutesTutorialComplete", "true");
                    levelToLoad = 7;
                    SceneManager.LoadScene(levelToLoad);
                }
                break;
            case 4:
                bool hasDonePipeGyroTutorial = PlayerPrefs.HasKey("PipeGyroTutorialComplete");
                if (hasDonePipeGyroTutorial == false)
                {
                    PlayerPrefs.SetString("PipeGyroTutorialComplete", "true");
                    levelToLoad = 8;
                    SceneManager.LoadScene(levelToLoad);
                }
                break;
        }
        SceneManager.LoadScene(levelToLoad);
        animator.SetTrigger("Open");
        audioSource.PlayOneShot(openClip, 0.7F);
    }
}