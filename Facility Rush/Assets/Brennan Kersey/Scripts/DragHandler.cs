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

        AudioManager.instance.soundAudioSource.clip = AudioManager.instance.soundClip[0];
        AudioManager.instance.soundAudioSource.Play();
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

        AudioManager.instance.soundAudioSource.clip = AudioManager.instance.soundClip[1];
        AudioManager.instance.soundAudioSource.Play();
    }
}
