
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


    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("Close");

        AudioManager.instance.soundAudioSource2.clip = closeClip;
        AudioManager.instance.soundAudioSource2.PlayOneShot(closeClip, 0.5f);
        thisbutton = FindObjectsOfType<Button>();
        for (int i = 0; i < thisbutton.Length; i++)
        {
            thisbutton[i].interactable = false;
        }
    }

    public void OnFadeComplete() // Called in Animation Event "Close" clip
    {
        switch(levelToLoad)
        {
            case 1:
                bool hasDoneAssemblyTutorial= PlayerPrefs.HasKey("AssemblyTutorialComplete");
                if(!hasDoneAssemblyTutorial)
                {
                    levelToLoad = 5;
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

        AudioManager.instance.soundAudioSource2.clip = openClip;
        AudioManager.instance.soundAudioSource2.PlayOneShot(openClip, 0.7f);
    }
}