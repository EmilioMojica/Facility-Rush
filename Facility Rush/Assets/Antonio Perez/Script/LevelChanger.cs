using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class LevelChanger : MonoBehaviour
{
    public Animator animator;

    public AudioClip closeClip;
    public AudioClip openClip;
    private AudioSource audioSource;

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
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
        animator.SetTrigger("Open");
        audioSource.PlayOneShot(openClip, 0.7F);
    }
}