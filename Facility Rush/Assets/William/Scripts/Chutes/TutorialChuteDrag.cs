using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialChuteDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector3 startPosition;
    public Vector3 backPosition;
    public Vector3 positionone;

    public int ID;

    public Transform startParent;

    public static GameObject itemBeingDragged;

    private Vector3 initial;

    public void Awake()
    {
        positionone = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        initial = this.transform.position;
        itemBeingDragged = gameObject;
        backPosition = transform.position;
        startPosition = transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        AudioManager.instance.soundAudioSource.clip = AudioManager.instance.soundClip[0];
        AudioManager.instance.soundAudioSource.Play();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 currentPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, initial.z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(currentPosition);
        this.transform.position = worldPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        itemBeingDragged = null;

        transform.position = startPosition;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        AudioManager.instance.soundAudioSource.clip = AudioManager.instance.soundClip[1];
        AudioManager.instance.soundAudioSource.Play();
    }


    public void MoveBack()
    {

        transform.SetParent(startParent);
        transform.position = positionone;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

    }
}
