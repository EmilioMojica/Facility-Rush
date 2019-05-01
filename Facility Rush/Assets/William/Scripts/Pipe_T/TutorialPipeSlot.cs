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

    public GameObject sphereToAnimate;

    private float correctSphereMoveForwardTime;
    private float correctSphereMoveBackwardTime;

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
            AudioManager.instance.soundAudioSource2.clip = AudioManager.instance.soundClip[7];  //choose vacuum correct SFX
            AudioManager.instance.soundAudioSource2.Play();

            StartCoroutine("WaitCorrectAnimation", 0);
            StartCoroutine("BoxMoveBack", 0.5f);

            GameObject.FindObjectOfType<TutorialPipe>().CorrectResultDialogue();

            StartCoroutine(PlayParticle(0.85f));
        }
        else    // Put into the wrong slot, plays corresponding wrong animation
        {
            AudioManager.instance.soundAudioSource2.clip = AudioManager.instance.soundClip[8];  //choose vacuum wrong SFX
            AudioManager.instance.soundAudioSource2.Play();

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
        correctSphereMoveForwardTime = QuestonsCanvasAnim.runtimeAnimatorController.animationClips[1].length;
        correctSphereMoveBackwardTime = QuestonsCanvasAnim.runtimeAnimatorController.animationClips[0].length;
        QuestonsCanvasAnim.SetInteger("indexBeingActedOn", i);
        yield return new WaitForSeconds(correctSphereMoveForwardTime);//.4f
        sphereToAnimate.SetActive(false);
        QuestonsCanvasAnim.SetInteger("indexBeingActedOn", 10);
        yield return new WaitForSeconds(correctSphereMoveBackwardTime);//.3f
        //sphereToAnimate.SetActive(true);
        QuestonsCanvasAnim.SetInteger("indexBeingActedOn", -1);
        yield return new WaitForSeconds(.1f);
        sphereToAnimate.SetActive(true);
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

    IEnumerator PlayParticle(float time)
    {
        yield return new WaitForSeconds(time);

        InvokeRepeating("ParticleEffect", 0, 1.5f);
        //TODO: 回到Menu 之後要 CancelInvoke
    }

    void ParticleEffect()
    {
        int index = Random.Range(0, particles.Length);

        AudioManager.instance.soundAudioSource2.clip = AudioManager.instance.soundClip[9];
        AudioManager.instance.soundAudioSource2.Play();

        print("index: " + index);
        print("particles.Length: " + particles.Length);
        particles[index].Play();
    }
}
