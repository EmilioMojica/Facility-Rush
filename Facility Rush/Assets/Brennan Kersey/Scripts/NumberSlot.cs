using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NumberSlot : MonoBehaviour, IDropHandler
{

    public GameObject numberObj;
    public int slot;

    public void OnDrop(PointerEventData eventData)
    {
       // Debug.Log("On drop");

    }
	void Update () {
		
	}
}
