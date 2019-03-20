using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssemblyTutorial : MonoBehaviour
{
    public string[] dialog;
    public string[] Conditions;

    private Text bubbleText;
    public int index;

    void Start()
    {
        TutorialSystem.PopDialog(index);

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NextDialogue();
            
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

    void NextDialogue()
    {
        if (index < dialog.Length - 1)
        {
            index++;

            TutorialSystem.PopDialog(index);
        }
        else
        {
            Debug.Log("Index is out of range");
        }

    }
    void Next()
    {
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                this.gameObject.SetActive(false);
            }
        }

        /*if (Input.GetMouseButtonDown(0))
        {
            this.gameObject.SetActive(false);
        }*/
    }

}
