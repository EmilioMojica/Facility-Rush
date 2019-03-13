using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private float musicValue;  // stores the volume value which it's corresponding audio source has

    [SerializeField] private Slider soundSlider;
    [SerializeField] private float soundValue;  // stores the volume value which it's corresponding audio source has

    [SerializeField] private AudioSource audioSource;


    private void Awake()
    {
        musicSlider = GameObject.FindObjectOfType<SliderHolder>().sliders[0];  //取得slider的控制權
        soundSlider = GameObject.FindObjectOfType<SliderHolder>().sliders[1];
    }

    // Start is called before the first frame update
    void Start()  //目標: 讓每個場景中的slider根據PlayerPrefs的值去自動調整
    {
        musicSlider.value = PlayerPrefs.GetFloat("music");
        //TODO: soundSlider 
        //AudioManager.instance.musicAudioSource.volume = musicSlider.value;  //根據slider的位置來更改真正的audio source聲音
        //TODO: soundSlider 

    }

    public void ValueChange()   // Called in the inspector of slider built-in OnValueChange
    {
        AudioManager.instance.musicAudioSource.volume = musicSlider.value;  //改變slider的音量大小會真正影響到audio source的音量大小
        musicValue = AudioManager.instance.musicAudioSource.volume;         
        PlayerPrefs.SetFloat("music", musicValue);                          //並把這個改變的值存進PlayerPrefs以供日後讀檔需要
    }

}
