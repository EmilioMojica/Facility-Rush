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
        pastPhaseOne = false;
        dialogPoint = 0;
        dialogText.text = explanationDialog[dialogPoint];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && haltCheck==false)
        {
            if (dialogPoint == 1 && pastPhaseOne==false)
            {
                manager.startAnimation();
                manager.choiceOnePipeOneNeedsToBePicked = true;
                haltCheck = true;
                pastPhaseOne = true;
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
        if (dialogPoint == 2)
        {
            manager.switchToProblemProgress();
            haltCheck = true;
        }
        else if(dialogPoint ==4)
        {
            manager.handAnimator.GetComponent<Image>().enabled = true;
            manager.activateTheNextThreePanel();
            haltCheck = true;
        }
        else if(dialogPoint==5)
        {
            manager.handAnimator.GetComponent<Image>().enabled = true;
            manager.scorePointer();
        }
        else if(dialogPoint==6)
        {
            manager.pointToTimerElement();
        }
        else if(dialogPoint==7)
        {
            this.gameObject.GetComponent<Image>().enabled = false;
            manager.gameOverStart();
        }
        if (dialogPoint <=explanationDialog.Length-1)
        {
            dialogText.text = explanationDialog[dialogPoint];
        }
        

        
    }
}
