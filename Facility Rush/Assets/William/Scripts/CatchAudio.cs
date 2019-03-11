using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script goal: OnClick() needs to get hold of a gameObject to function, but AudioManager is a cross scene gameObject, so we can't hold it in this
   scene. Therefore I can't put ChangeMusic method in Audio Manager. To fix that, this script puts the ChangeMusic method in it. Whoever wants to call 
   this function just put this script on that gameObject and hold it in OnClick()  */
public class CatchAudio : MonoBehaviour
{
    public void ChangeMusic(AudioClip clip)
    {
        AudioManager.instance.musicAudioSource.clip = clip;
        AudioManager.instance.musicAudioSource.Play();
    }
}
