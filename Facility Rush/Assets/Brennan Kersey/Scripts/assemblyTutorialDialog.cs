using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class assemblyTutorialDialog : MonoBehaviour
{
    public string[] explanationDialog;
    private int dialogPoint;
    [SerializeField] private Text dialogText;
    [SerializeField] private tutorialAssembyManager manager;
    // Start is called before the first frame update
    void Start()
    {
        dialogPoint = 0;
        dialogText.text = explanationDialog[dialogPoint];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (dialogPoint == explanationDialog.Length-1)
            {
                manager.startAnimation();
                manager.choiceOnePipeOneNeedsToBePicked = true;
                this.enabled = false;
            }
            nextDialog();
            
        }
    }

    public void nextDialog()
    {
       
        dialogPoint++;
        if (dialogPoint <=2)
        {
            dialogText.text = explanationDialog[dialogPoint];
        }
        

        
    }
}
