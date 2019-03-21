using UnityEditor.StyleSheets;
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
        SceneManager.LoadScene(levelToLoad);
        animator.SetTrigger("Open");
        audioSource.PlayOneShot(openClip, 0.7F);
    }
}