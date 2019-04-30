using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialPipe : MonoBehaviour
{
    [SerializeField] private string[] dialog;
    [SerializeField] private int index;

    [SerializeField] private Animator anim;
    [SerializeField] private Animator pipeAnim;

    [SerializeField] private Image[] image;

    [SerializeField] private RectTransform[] bubblePos;
    [SerializeField] private RectTransform bubbleImage;
    [SerializeField] private GameObject pipeContainer;

    [SerializeField] private Text scoreText, timerText, progressText;
    [SerializeField] private GameObject PausePanel;

    private Text bubbleText;
    public bool boolean;
    public LevelChanger lc;
    public int pauseMenuLayer;


    private void OnEnable()
    {
        bubbleText = GetComponent<Text>();
        TutorialSystem.PopDialog += ChangeText;
    }
    private void OnDisable()
    {
        TutorialSystem.PopDialog -= ChangeText;
    }

    void Start()
    {
        TutorialSystem.PopDialog(index);
        lc = GameObject.FindObjectOfType<LevelChanger>();
        boolean = true;
        pauseMenuLayer = LayerMask.GetMask("PauseButton");
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hitPause = Physics.Raycast(ray, Mathf.Infinity, pauseMenuLayer);
        Debug.Log("hitPause: " + hitPause);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue);
        if (Input.GetMouseButtonDown(0) && !hitPause && !PausePanel.activeInHierarchy)  //TODO: need to change to touchpad?
        {
            if (index < 5)
            {
                NextDialogue();  // 完成bubble text上的東西後才呼叫
            }
            else if (index >= 7 && index < 12)
            {
                image[0].GetComponentInChildren<Text>().text = "1+1";
                image[1].GetComponentInChildren<Text>().text = "4+1";
                image[2].GetComponentInChildren<Text>().text = "6\n+\n3";
                image[3].GetComponentInChildren<Text>().text = "3\n+\n4";
                image[4].GetComponentInChildren<Text>().text = "5";
                image[5].GetComponentInChildren<Text>().text = "10";
                image[6].GetComponentInChildren<Text>().text = "1";
                image[7].GetComponentInChildren<Text>().text = "8";

                NextDialogue();
            }

            if (index == 12)
            {
                StartCoroutine(KickPlayerOut(5)); //Kick the player out after 5 sec

               // StartCoroutine(ChangeBool());

                if (Input.GetMouseButtonDown(0) && boolean)
                {
                    boolean = false;
                    if (lc)
                    {
                        lc.FadeToLevel(4);
                    }
                    else
                    {
                        SceneManager.LoadScene(4);
                    }
                }
            }
        }
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

            switch (index)
            {
                case 2:
                    bubbleImage.localPosition = bubblePos[0].localPosition;


                    break;
                case 4:
                    pipeAnim.SetBool("IsGlowing", true);

                    break;

                case 5:
                    bubbleImage.localPosition = bubblePos[1].localPosition;
                    bubbleImage.sizeDelta = new Vector2(334.4f, 83.7f);

                    pipeAnim.SetBool("IsGlowing", false);

                    anim.SetBool("IsFlashing_Pipe", true);
                    StartCoroutine("WaitFingerAnimation");
                    break;

                case 9:
                    bubbleImage.localPosition = bubblePos[0].localPosition;
                    bubbleImage.sizeDelta = new Vector2(334.4f, 138.22f);

                    break;

                case 10:
                    //pipeContainer.SetActive(false);

                    //indicator finger pops out
                    anim.SetBool("PipeScore", true);
                    StartCoroutine("WaitScoreAdded");
                    break;

                case 11:
                    anim.SetBool("PipeScore", false);
                    anim.SetBool("PipeTimer", true);

                    StartCoroutine("WaitTimerDecrease");
                    Debug.Log("indexy == 10");
                    break;

                default:
                    Debug.Log("default");
                    anim.SetBool("IsFlashing_Pipe", false);
                    break;
            }

            TutorialSystem.PopDialog(index);
        }
    }

    public void FalseResultDialogue()
    {
        if (index < dialog.Length - 1)
        {
            index = 6;
            TutorialSystem.PopDialog(index);

            progressText.text = "Try again!";
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
            index = 7;
            TutorialSystem.PopDialog(index);

            progressText.text = "Great job!";

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

    IEnumerator WaitScoreAdded()
    {
        yield return new WaitForSeconds(1.5f);

        scoreText.text = "200";
    }

    IEnumerator WaitTimerDecrease()
    {
        yield return new WaitForSeconds(1.5f);

        timerText.text = "0:00";
    }

    IEnumerator KickPlayerOut(int time)
    {
        Debug.Log("start counting down");
        yield return new WaitForSeconds(time);

        if (lc)
        {
            lc.FadeToLevel(4);
        }
        else
        {
            SceneManager.LoadScene(4);
        }
    }

    IEnumerator ChangeBool()
    {
        yield return null;

        boolean = true;
    }
}
