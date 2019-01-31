﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChutesDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector3 startPosition;
    public Vector3 backPosition;
    public Vector3 positionone;

    //public RectTransform rt;
    public Transform startParent;

    public static GameObject itemBeingDragged;

    public void Awake()
    {
        positionone = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        backPosition = transform.position;
        startPosition = transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {

        transform.position = Input.mousePosition;
  
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        itemBeingDragged = null;
     
        transform.position = startPosition;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

    }


    public void MoveBack()
    {
      
        transform.SetParent(startParent);
        transform.position = positionone;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        
    }

  
}

