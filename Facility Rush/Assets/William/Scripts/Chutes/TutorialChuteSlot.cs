using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialChuteSlot : MonoBehaviour, IDropHandler
{
    public int correctID;
    public int tubeNumber;

    public Animator[] tube;
    public Animator putAnswer;

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
        if (!item)
        {
            TutorialChuteDrag.itemBeingDragged.transform.SetParent(transform);
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
        }

        if (TutorialChuteDrag.itemBeingDragged.GetComponent<TutorialChuteDrag>().ID == correctID)
        {
            tube[3].SetBool("Correct1", true);
            putAnswer.SetBool("NumberMove", true);

            StartCoroutine(WaitCorrectAnimation(0.5f));
            StartCoroutine(BoxMoveBack(0.75f));

            GameObject.FindObjectOfType<TutorialChutes>().NextDialogue();
        }
        else
        {
            switch (tubeNumber)
            {
                case 1:
                    tube[0].SetBool("Wrong1", true);
                    putAnswer.SetBool("NumberMove", true);
                    GameObject.FindObjectOfType<TutorialChutes>().NextDialogue2(5);  //顯示出Opps, almost there

                    StartCoroutine(WaitWrongAnimation(0.5f, 0));

                    StartCoroutine(BoxMoveBack(0.75f));

                    break;
                case 2:
                    tube[1].SetBool("Wrong1", true);
                    putAnswer.SetBool("NumberMove", true);
                    GameObject.FindObjectOfType<TutorialChutes>().NextDialogue2(5);

                    StartCoroutine(WaitWrongAnimation(0.5f, 1));

                    StartCoroutine(BoxMoveBack(0.75f));

                    break;
                case 3:
                    tube[2].SetBool("Wrong1", true);
                    putAnswer.SetBool("NumberMove", true);
                    GameObject.FindObjectOfType<TutorialChutes>().NextDialogue2(5);

                    StartCoroutine(WaitWrongAnimation(0.5f, 2));

                    StartCoroutine(BoxMoveBack(0.75f));

                    break;
                case 4:
                    tube[3].SetBool("Wrong1", true);
                    putAnswer.SetBool("NumberMove", true);
                    GameObject.FindObjectOfType<TutorialChutes>().NextDialogue2(5);  

                    StartCoroutine(WaitWrongAnimation(0.5f, 3));

                    StartCoroutine(BoxMoveBack(0.75f));

                    break;
            }
        }
    }

    IEnumerator WaitWrongAnimation(float time, int i)
    {
        yield return new WaitForSeconds(time);
        tube[i].SetBool("Wrong1", false);
        putAnswer.SetBool("NumberMove", false);
        putAnswer.SetBool("NumberBack", true);
    }

    IEnumerator WaitCorrectAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        tube[3].SetBool("Correct1", false);
        putAnswer.SetBool("NumberMove", false);
        putAnswer.SetBool("NumberBack", true);
    }

    IEnumerator BoxMoveBack(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.GetComponentInChildren<TutorialChuteDrag>().MoveBack();
    }
}
