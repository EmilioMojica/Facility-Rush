﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialChutes : MonoBehaviour
{
    public string[] dialog;
    public string[] Conditions;

    private Text bubbleText;
    public int index;

    public Animator anim;
    public RectTransform[] flasingPos;
    public Transform bubbleImage;

    public Image[] image;
    public Text time;

    void Start()
    {
        TutorialSystem.PopDialog(index);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (index < 3)
            {
                NextDialogue();  // 完成bubble text上的東西後才呼叫
            }
            else if (index >= 5)
            {
                NextDialogue();
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

                foreach (Image i in image)
                {
                    i.raycastTarget = true;
                }

                anim.SetBool("IsFlashing", true);   
            }

            if (index == 6)
            {
                anim.SetBool("IsScoreFlashing", true);
            }

            if (index == 7)
            {
                anim.SetBool("IsScoreFlashing", false);
                anim.SetBool("IsTimeFlashing", true);

                bubbleImage.localPosition = flasingPos[0].localPosition;
            }

            if (index == 8)
            {
                time.text = "0:00";
            }

            TutorialSystem.PopDialog(index);
        }

    }

    public void FalseResultDialogue()
    {
        if (index < dialog.Length - 1)
        {
            index = 4;
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
            index = 5;
            TutorialSystem.PopDialog(index);

            foreach (Image j in image)
            {
                j.raycastTarget = false;
            }

            anim.SetBool("IsFlashing", false);
        }
        else if (index > dialog.Length - 1)
        {
            index = 0;
            TutorialSystem.PopDialog(index);
        }
    }
}
