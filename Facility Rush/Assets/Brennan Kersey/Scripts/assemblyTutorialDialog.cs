using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class assemblyTutorialDialog : MonoBehaviour
{
    public string[] explanationDialog;
    public int dialogPoint;
    [SerializeField] private Text dialogText;
    [SerializeField] private tutorialAssembyManager manager;
    public bool haltCheck;
    public bool pastPhaseOne;
    // Start is called before the first frame update
    void Start()
    {
        dialogPoint = 0;
        dialogText.text = explanationDialog[dialogPoint];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && haltCheck==false)
        {
            if (dialogPoint == 1 && pastPhaseOne==true)
            {
                manager.startAnimation();
                manager.choiceOnePipeOneNeedsToBePicked = true;
                haltCheck = true;
            }

            if(dialogPoint == 2)
            {
                manager.switchToProblemProgress();
                haltCheck = true;
            }
            if (haltCheck != true)
            {
                nextDialog();
            }
            
        }
    }

    public void nextDialog()
    {
       
        dialogPoint++;
        if (dialogPoint <=explanationDialog.Length-1)
        {
            dialogText.text = explanationDialog[dialogPoint];
        }
        

        
    }
}
