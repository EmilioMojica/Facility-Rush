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

        if (objectName.Contains("Chute 1"))
        {
            print("Chute 1 has a value of " + panelValue);
            manager.GetComponent<assemblyManager>().setChuteOneChoice(panelValue);
            assembler.showChoice1.text = "" + panelValue;
        }
        else if (objectName.Contains("Chute 2"))
        {
            print("Chute b2 has a value of " + panelValue);
            manager.GetComponent<assemblyManager>().setChuteTwoChoice(panelValue);
            assembler.showChoice2.text = "" + panelValue;
        }
        else if (objectName.Contains("Chute 3"))
        {
            print("Chute 3 has a value of " + panelValue);
            manager.GetComponent<assemblyManager>().setChuteThreeChoice(panelValue);
            assembler.showChoice3.text = "" + panelValue;
        }
    }

}
