using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamiliarSystem : MonoBehaviour
{
    public delegate void DialogContainer(int i);  //declare the method signiture of type delegate
    public static DialogContainer PopDialog;      //declare the member variable(container) of type delegate that we just made

    public GameObject[] placeHolders;
    public static FamiliarSystem instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
