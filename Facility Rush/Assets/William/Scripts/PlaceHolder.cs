using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolder : MonoBehaviour
{
    public DegradationManager degradationManager;
    public LevelChanger levelChanger;

    // Start is called before the first frame update
    void Start()
    {
        degradationManager = GameObject.FindObjectOfType<DegradationManager>();
        levelChanger = GameObject.FindObjectOfType<LevelChanger>();
    }

    public void CallsetAllBoolSToFalse()
    {
        degradationManager.setAllBoolSToFalse();
    }

    public void CallFadeToLevel(int levelIndex)
    {
        levelChanger.FadeToLevel(levelIndex);
    }
}
