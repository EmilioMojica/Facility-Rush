using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script goal: OnClick() needs to get hold of a gameObject to function, but AudioManager is a cross scene gameObject, so we can't hold it in other
   scenes. Therefore I can't put ChangeMusic method in Audio Manager. To fix that, this script puts the ChangeMusic method in it. Whoever wants to call 
   this function just attached this script on that gameObject and hold it in OnClick()  

   Also, put all of the activate and deactivate audio methods in this script */

public class CatchAudio : MonoBehaviour
{
    public void ChangeMusic(AudioClip MusiceClip)  //Changes to the corresponding music whenever we get into another scene (Called in "MainMenu Button" in Assembly Line scene)
    {
        AudioManager.instance.musicAudioSource.clip = MusiceClip;
        AudioManager.instance.musicAudioSource.Play();
    }

    public void ActivateHumNoise(AudioClip SFXclip)
    {
        AudioManager.instance.soundAudioSource2.clip = SFXclip;
        AudioManager.instance.soundAudioSource2.loop = true;
        AudioManager.instance.soundAudioSource2.Play();
    }

    public void StopHumNoise(AudioClip SFXclip)
    {
        AudioManager.instance.soundAudioSource2.loop = false;
        AudioManager.instance.soundAudioSource2.Stop();
    }
}
