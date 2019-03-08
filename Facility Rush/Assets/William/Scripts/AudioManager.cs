using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] audioArray;
    public AudioSource musicAudioSource; //負責處理music的audioSource
    public AudioSource soundAudioSource; //負責處理SF的audioSource
    public AudioSource soundAudioSource2; //負責處理SF的audioSource


    public AudioClip[] musicClip;
    public AudioClip[] soundClip;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioArray = GetComponents<AudioSource>();  //把身上兩個audioSource都裝在這個陣列裡面

        musicAudioSource = audioArray[0]; //再從這個陣列去取得第一個audioSource
        soundAudioSource = audioArray[1]; //從這個陣列取得第二個audioSource
        soundAudioSource2 = audioArray[2];

        musicAudioSource.clip = musicClip[0];
        musicAudioSource.Play();

        soundAudioSource.clip = soundClip[0];

        soundAudioSource2.clip = soundClip[2];


        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            soundAudioSource.Play();
        }
    }

}
