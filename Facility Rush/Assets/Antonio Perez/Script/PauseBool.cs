using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBool : MonoBehaviour
{
    [HideInInspector] public bool backToMenu;

    private void Start()
    {
        backToMenu = false;
    }
}
