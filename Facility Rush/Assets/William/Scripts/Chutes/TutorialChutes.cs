using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialChutes : MonoBehaviour
{
    public string[] dialog;
    public string[] Conditions;

    private Text bubbleText;
    public int index;
    public int count;

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
            if (count < 3)
            {
                NextDialogue();  // 完成bubble text上的東西後才呼叫
                count++;
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

            if (index == 3)
            {
                myRecTransform.localPosition = flasingPos[0].localPosition;

                foreach (Image i in image)
                {
                    i.raycastTarget = true;
                }

                anim.SetBool("IsFlashing", true);   
            }
            else
            {
                myRecTransform.localPosition = new Vector3(500, 100, 0);

                anim.SetBool("IsFlashing", false);

            }
            TutorialSystem.PopDialog(index);
        }
        else if(index == dialog.Length - 1)
        {
            index = 0;
            TutorialSystem.PopDialog(index);

        }


    }

    public void NextDialogue2(int i)
    {
        if (index < dialog.Length - 1)
        {
            Debug.Log("你失敗了");

            TutorialSystem.PopDialog(i);
        }
        else if (index == dialog.Length - 1)
        {
            index = 0;
            TutorialSystem.PopDialog(index);

        }


    }

}
