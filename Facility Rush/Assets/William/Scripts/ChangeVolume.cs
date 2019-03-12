using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private float music;

    [SerializeField] private Slider soundSlider;
    [SerializeField] private float sound;

    [SerializeField] private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        musicSlider.value = PlayerPrefs.GetFloat("music");
        audioSource.volume = musicSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void valueChange()  // Called in the inspector of slider built-in OnValueChange
    {
        audioSource.volume = musicSlider.value;
        music = audioSource.volume;
        PlayerPrefs.SetFloat("music", music);
    }
}
