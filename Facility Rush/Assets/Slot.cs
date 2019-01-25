using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour , IDropHandler{

    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }

            return null;
        }
    }

    public int slot;

    public void OnDrop(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        if (!item)
        {
            DragHandler.itemBeingDragged.transform.SetParent(transform);
            ExecuteEvents.ExecuteHierarchy<IHasCHanged>(gameObject, null, (x, y) => x.HasChanged());
            Calculate.instance.Addup(slot);
            //new script

        }
    }
}
