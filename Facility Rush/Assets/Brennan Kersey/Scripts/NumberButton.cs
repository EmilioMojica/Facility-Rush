using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberButton : MonoBehaviour
{
    [SerializeField] Number numberPrefab;

    private void OnMouseDown()
    {
        FindObjectOfType<MouseClick>().SetSelectedNumber(numberPrefab);
    }

}
