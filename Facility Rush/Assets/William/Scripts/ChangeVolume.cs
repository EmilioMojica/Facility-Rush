using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private float volumeValue;

    [SerializeField] private Slider soundSlider;
    [SerializeField] private float sound;

    [SerializeField] private AudioSource audioSource;


    private void Awake()
    {
        //musicSlider = GameObject.FindGameObjectWithTag("musicSlider").GetComponent<Slider>(); //無法找到 因為一開始是deactivate的
        //soundSlider = GameObject.FindGameObjectWithTag("soundSlider").GetComponent<Slider>();

        musicSlider = GameObject.FindObjectOfType<SliderHolder>().sliers[0];
        soundSlider = GameObject.FindObjectOfType<SliderHolder>().sliers[1];
    }

    // Start is called before the first frame update
    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("music");
        AudioManager.instance.musicAudioSource.volume = musicSlider.value;
    }

    public void ValueChange()   // Called in the inspector of slider built-in OnValueChange
    {
        AudioManager.instance.musicAudioSource.volume = musicSlider.value;
        volumeValue = AudioManager.instance.musicAudioSource.volume;
        PlayerPrefs.SetFloat("music", volumeValue);
    }

}
