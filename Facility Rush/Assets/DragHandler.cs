using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;

    public static GameObject itemBeingDragged;

    private Transform startParent;
   
    public void OnBeginDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        itemBeingDragged = null;
        //Debug.Log("start parent" + startParent);
        /*if (transform.parent != startParent)
        {
            //Debug.Log("start parent"+ startParent);
            transform.position = startPosition;
        }
        */
        transform.position = startPosition;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
      
    }
}
