using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TutorialPipeSlot : MonoBehaviour, IDropHandler
{
    public int correctID; // 每個slot都有他自己獨一無二的ID (知道誰才是他要的真正答案)
    public int tubeNumber;// 每個tube都有自己的ID，之後當box放進去之後會根據ID來決定要播哪一個動畫

    public Animator QuestonsCanvasAnim;

    public Text scoreText;

    public ParticleSystem[] particles;

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

    public void OnDrop(PointerEventData eventData)
    {
        if (!item)
        {
            TutorialPipeDrag.itemBeingDragged.transform.SetParent(transform);
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
        }

        if (TutorialPipeDrag.itemBeingDragged.GetComponent<TutorialPipeDrag>().ID == correctID)
        {
            StartCoroutine("WaitCorrectAnimation", 0);
            StartCoroutine("BoxMoveBack", 0.5f);

            GameObject.FindObjectOfType<TutorialPipe>().CorrectResultDialogue();


        }
        else    // Put into the wrong slot, plays corresponding wrong animation
        {
            switch (tubeNumber)
            {
                case 1:
                    StartCoroutine("WaitWrongAnimation", 4);
                    StartCoroutine("BoxMoveBack", 0.5f);
                    GameObject.FindObjectOfType<TutorialPipe>().FalseResultDialogue();


                    break;
                case 2:
                    StartCoroutine("WaitWrongAnimation", 5);
                    StartCoroutine("BoxMoveBack", 0.5f);
                    GameObject.FindObjectOfType<TutorialPipe>().FalseResultDialogue();

                    break;
                case 3:
                    StartCoroutine("WaitWrongAnimation", 6);
                    StartCoroutine("BoxMoveBack", 0.5f);
                    GameObject.FindObjectOfType<TutorialPipe>().FalseResultDialogue();

                    break;
                case 4:
                    StartCoroutine("WaitWrongAnimation", 7);
                    StartCoroutine("BoxMoveBack", 0.5f);
                    GameObject.FindObjectOfType<TutorialPipe>().FalseResultDialogue();

                    break;
            }
        }
    }

    IEnumerator WaitCorrectAnimation(int i)
    {
        QuestonsCanvasAnim.SetInteger("indexBeingActedOn", i);
        yield return new WaitForSeconds(0.4f);
        QuestonsCanvasAnim.SetInteger("indexBeingActedOn", 10);
        yield return new WaitForSeconds(0.4f); //.3f
        QuestonsCanvasAnim.SetInteger("indexBeingActedOn", -1);
    }
    IEnumerator WaitWrongAnimation(int i)
    {
        QuestonsCanvasAnim.SetInteger("indexBeingActedOn", i);
        yield return new WaitForSeconds(0.3f);
        QuestonsCanvasAnim.SetInteger("indexBeingActedOn", 10);
        yield return new WaitForSeconds(0.3f); //.3f
        QuestonsCanvasAnim.SetInteger("indexBeingActedOn", -1);
    }

    IEnumerator BoxMoveBack(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.GetComponentInChildren<TutorialPipeDrag>().MoveBack();

    }
}
