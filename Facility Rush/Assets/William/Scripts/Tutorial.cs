using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public string[] dialog;
    public string[] Conditions;

    private Text text;

    private void Start()
    {
        FamiliarSystem.PopDialog(0);

    }
    void Update()
    {
        Next();
    }
    private void OnEnable()
    {
        text = GetComponent<Text>();
        FamiliarSystem.PopDialog += ChangeText;
    }

    private void OnDisable()
    {
        FamiliarSystem.PopDialog -= ChangeText;
    }

    void ChangeText(int index)
    {
        text.text = dialog[index];
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
