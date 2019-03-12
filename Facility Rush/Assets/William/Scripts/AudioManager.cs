using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script goal: this Audio Manager holds all of the audio clips and call it in other scripts whenever it meets the situation */
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] audioArray;
    public AudioSource musicAudioSource;  //負責處理music的audioSource
    public AudioSource soundAudioSource;  //負責處理SF的audioSource
    public AudioSource soundAudioSource2; //負責處理其他SF的audioSource
    public AudioSource soundAudioSource3; //負責處理其他SF的audioSource

    public AudioClip[] musicClip;
    public AudioClip[] soundClip;

    [Range (60, 180)]
    public float steamNoise;
    public float time;

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

        audioArray = GetComponents<AudioSource>();  //把身上三個audioSource都裝在這個陣列裡面

        musicAudioSource = audioArray[0]; //再從這個陣列去取得第一個audioSource
        soundAudioSource = audioArray[1]; //從這個陣列取得第二個audioSource
        soundAudioSource2 = audioArray[2];

        //new
        soundAudioSource3 = audioArray[3];

        musicAudioSource.clip = musicClip[0];
        musicAudioSource.Play();

        soundAudioSource.clip = soundClip[0]; //box pick up SFX

        soundAudioSource2.clip = soundClip[2]; //box slide SFX

        //new
        soundAudioSource3.clip = soundClip[6]; //box slide SFX
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
            Debug.Log("steamNoise is now: " + steamNoise);
            time = 0;
            steamNoise = Random.Range(60, 180);
        }
    }

}
