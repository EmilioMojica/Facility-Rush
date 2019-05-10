using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickablePipePanels : MonoBehaviour
{
    public GameObject manager;
    private assemblyManager assembler;

    public void obtainPanelValue()
    {
        assembler = manager.GetComponent<assemblyManager>();
        string objectName = gameObject.name;
        string panelValue = gameObject.GetComponentInChildren<Text>().text;
        if (assembler.isAnimating == false && assembler.isInteractable==true)
        {
            if (objectName.Contains("Chute 1"))
            {
                manager.GetComponent<assemblyManager>().setChuteOneChoice(panelValue);
                if (objectName.Contains("choice 1"))
                {
                    assembler.componentChoice(0);
                }

                if (objectName.Contains("choice 2"))
                {
                    assembler.componentChoice(1);
                }

                assembler.showChoice1.text = "" + panelValue;
            }
            else if (objectName.Contains("Chute 2"))
            {
                manager.GetComponent<assemblyManager>().setChuteTwoChoice(panelValue);

                if (objectName.Contains("choice 1"))
                {
                    assembler.componentChoice(2);
                }

                if (objectName.Contains("choice 2"))
                {
                    assembler.componentChoice(3);
                }

                if (objectName.Contains("choice 3"))
                {
                    assembler.componentChoice(4);
                }

                if (objectName.Contains("choice 4"))
                {
                    assembler.componentChoice(5);
                }
                assembler.showChoice2.text = "" + panelValue;
            }
            else if (objectName.Contains("Chute 3"))
            {
                manager.GetComponent<assemblyManager>().setChuteThreeChoice(panelValue);

                if (objectName.Contains("choice 1"))
                {
                    assembler.componentChoice(6);
                }

                if (objectName.Contains("choice 2"))
                {
                    assembler.componentChoice(7);
                }
                assembler.showChoice3.text = "" + panelValue;
            }
        }
    }
}
