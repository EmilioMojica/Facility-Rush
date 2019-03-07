using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMusic(AudioClip clip)
    {

        AudioManager.instance.audioSource.clip = clip;
        AudioManager.instance.audioSource.Play();
        Debug.Log("Uses static way");
    }
}
