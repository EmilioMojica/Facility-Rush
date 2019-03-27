using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPipe : MonoBehaviour
{
    public string[] dialog;
    public string[] Conditions;

    private Text bubbleText;
    public int index;

    public Animator anim;
    public RectTransform myRecTransform;
    public RectTransform[] flasingPos;

    public Image[] image;

    void Start()
    {
        TutorialSystem.PopDialog(index);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (index < 4)
            {
                NextDialogue();  // 完成bubble text上的東西後才呼叫
            }
        }
    }

    private void OnEnable()
    {
        bubbleText = GetComponent<Text>();
        TutorialSystem.PopDialog += ChangeText;
    }

    private void OnDisable()
    {
        TutorialSystem.PopDialog -= ChangeText;
    }

    void ChangeText(int index)
    {
        bubbleText.text = dialog[index];
    }

    public void NextDialogue()
    {
        if (index < dialog.Length - 1)
        {
            index++;

            if (index == 4)
            {
                myRecTransform.localPosition = flasingPos[0].localPosition;


                anim.SetBool("IsFlashing_Pipe", true);

                StartCoroutine("WaitFingerAnimation");
            }
            else
            {
                myRecTransform.localPosition = new Vector3(500, 100, 0);
                Debug.Log("in else");
                anim.SetBool("IsFlashing_Pipe", false);
                
            }
            TutorialSystem.PopDialog(index);
        }
        else if (index == dialog.Length - 1)
        {
            index = 0;
            TutorialSystem.PopDialog(index);

        }


    }

    public void FalseResultDialogue()
    {
        if (index < dialog.Length - 1)
        {
            index = 7;
            TutorialSystem.PopDialog(index);
        }
        else if (index > dialog.Length - 1)
        {
            index = 0;
            TutorialSystem.PopDialog(index);
        }
    }

    public void CorrectResultDialogue()
    {
        if (index <= dialog.Length - 1)
        {
            index = 6;
            TutorialSystem.PopDialog(index);

            foreach (Image j in image)
            {
                j.raycastTarget = false;
            }

            anim.SetBool("IsFlashing_Pipe", false);
        }
        else if (index > dialog.Length - 1)
        {
            index = 0;
            TutorialSystem.PopDialog(index);

        }
    }

    IEnumerator WaitFingerAnimation()
    {
        yield return new WaitForSeconds(3f);

        foreach (Image i in image)
        {
            i.raycastTarget = true;
        }
    }
}
