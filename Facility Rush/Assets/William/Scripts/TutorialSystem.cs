using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSystem : MonoBehaviour
{
    public delegate void TutorialEventHandler(int i);  //declare the method signiture of type delegate
    public static TutorialEventHandler PopDialog;      //declare the member variable(container) of type delegate that we just made

    public GameObject[] placeHolders;
    public static TutorialSystem instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

}
