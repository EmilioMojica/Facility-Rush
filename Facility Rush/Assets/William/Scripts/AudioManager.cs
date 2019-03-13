using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script goal: this Audio Manager holds all of the audio clips and call it in other scripts whenever it meets the situation */
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] audioArray;      //把所有audio source都放進去 不然用GetComponent<>()去找只會一直找到第一個AudioSource

    public AudioSource musicAudioSource;  //負責處理 music的audioSource
    public AudioSource soundAudioSource;  //負責處理 box pick up /drop down /drill /gear noise SF的audioSource
    public AudioSource soundAudioSource2; //負責處理 box slide SF的audioSource
    public AudioSource soundAudioSource3; //負責處理 steam noise SF的audioSource

    public AudioClip[] musicClip;
    public AudioClip[] soundClip;

    [Range (5, 180)]
    public float steamNoise;
    public float time;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioArray = GetComponents<AudioSource>();  //把身上四個audioSource都裝在這個陣列裡面

        musicAudioSource = audioArray[0]; //再從這個陣列去取得第一個audioSource
        soundAudioSource = audioArray[1]; //從這個陣列取得第二個audioSource
        soundAudioSource2 = audioArray[2];
        soundAudioSource3 = audioArray[3];
    }

    private void Start()
    {
        musicAudioSource.clip = musicClip[0];
        musicAudioSource.Play();

        soundAudioSource.clip = soundClip[0];  //Set box pick up /drop down /drill /gear noise SFX to the audio source
        soundAudioSource2.clip = soundClip[2]; //Set box slide SFX
        soundAudioSource3.clip = soundClip[6]; //Set steam noise SFX

        steamNoise = 60f;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > steamNoise)
        {
            soundAudioSource3.Play();
            time = 0;
            steamNoise = Random.Range(60, 180);
        }
    }
}
