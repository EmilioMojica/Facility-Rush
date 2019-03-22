using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialChuteSlot : MonoBehaviour, IDropHandler
{
    public int correctID; // 每個slot都有他自己獨一無二的ID (知道誰才是他要的真正答案)
    public int tubeNumber;// 每個tube都有自己的ID，之後當box放進去之後會根據ID來決定要播哪一個動畫

    public Animator[] tube;
    public Animator putAnswer;

    public ParticleSystem particle;

    public GameObject item  // 屬性，專門看這個slot下面有沒有子物件，若有的話一定是這個slot的子物件。沒有的話則return null
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

    public void OnDrop(PointerEventData eventData)  //東西放下去的時候會自動呼叫
    {
        if (!item)  // 若沒子物件，則現在馬上設定拖拉的物件變成這個slot的子物件
        {
            TutorialChuteDrag.itemBeingDragged.transform.SetParent(transform);
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
        }

        if (TutorialChuteDrag.itemBeingDragged.GetComponent<TutorialChuteDrag>().ID == correctID) //比對看看放上來的box是否是自己在等的正確ID(答案)，是的話往下執行綠色動畫
        {
            tube[3].SetBool("Correct1", true);
            putAnswer.SetBool("NumberMove", true);

            StartCoroutine(WaitCorrectAnimation(0.5f));
            StartCoroutine(BoxMoveBack(0.75f));

            GameObject.FindObjectOfType<TutorialChutes>().NextDialogue();

            StartCoroutine(PlayParticle(0.85f));


        }
        else    //不是的話執行紅色的動畫
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

    IEnumerator PlayParticle(float time)
    {
        yield return new WaitForSeconds(time);
        particle.Play();

    }
}
