using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TutorialChutes : MonoBehaviour
{
    public string[] dialog;

    private Text bubbleText;
    private bool boolean;

    public int index;

    public Animator anim;  //drag & drop in the inspector
    public RectTransform[] flasingPos; //drag & drop in the inspector
    public Transform bubbleImage; //drag & drop in the inspector

    public Image[] image;
    public Text time;

    public LevelChanger levelChanger;

    void Start()
    {
        TutorialSystem.PopDialog(index);
        levelChanger = GameObject.FindObjectOfType<LevelChanger>();
        boolean = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  //TODO: May change to finger touch instead of mouse click
        {
            if (index < 3)
            {
                NextDialogue();  // 完成bubble text上的東西後才呼叫
            }
            else if (index >= 5 && index < 9)
            {
                NextDialogue();
            }

            if(index == 9)
            {
                StartCoroutine(KickPlayerOut(5)); //Kick the player out after 5 sec

                //StartCoroutine(ChangeBool());

                if (Input.GetMouseButtonDown(0) && boolean)
                {
                    boolean = false;
                    if (levelChanger)
                    {
                        Debug.Log("index is 7");
                        levelChanger.FadeToLevel(3);
                    }
                    else
                    {
                        Debug.Log("index is 7");

                        SceneManager.LoadScene(3);
                    }
                }
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

                bubbleImage.localPosition = flasingPos[0].localPosition;
            }

            if (index == 7)
            {
                anim.SetBool("IsScoreFlashing", false);
                anim.SetBool("IsTimeFlashing", true);
            }

            if (index == 8)
            {
                time.text = "0:00";
            }

            TutorialSystem.PopDialog(index);
        }

    }

    public void FalseResultDialogue()  // Called in TutorialChuteSlot script
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

    public void CorrectResultDialogue()  // Called in TutorialChuteSlot script
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

    IEnumerator KickPlayerOut(int time)
    {
        Debug.Log("start counting down");
        yield return new WaitForSeconds(time);

        if (levelChanger)
        {
            levelChanger.FadeToLevel(3);
        }
        else
        {
            SceneManager.LoadScene(3);
        }
    }

    IEnumerator ChangeBool()
    {
        yield return null;

        boolean = true;
    }
}
