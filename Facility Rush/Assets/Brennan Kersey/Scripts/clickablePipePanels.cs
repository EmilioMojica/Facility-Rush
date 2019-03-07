using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickablePipePanels : MonoBehaviour
{
    public GameObject manager;
    private assemblyManager assembler;
   // [SerializeField] private GameObject[] sampleToys;
   // [SerializeField] private Animator[] pipeAnimators;
   // [SerializeField] private Transform[] toySpawnPoint;
    public void obtainPanelValue()
    {
        assembler = manager.GetComponent<assemblyManager>();
        string objectName = gameObject.name;
        print("htis is object name " + objectName);
        string panelValue = gameObject.GetComponentInChildren<Text>().text;
        if (assembler.isAnimating == false)
        {
            if (objectName.Contains("Chute 1"))
            {
                print("Chute 1 has a value of " + panelValue);
                manager.GetComponent<assemblyManager>().setChuteOneChoice(panelValue);
                if (objectName.Contains("choice 1"))
                {
                    assembler.componentChoice(0);
                }

                if (objectName.Contains("choice 2"))
                {
                    assembler.componentChoice(1);
                }

                //StartCoroutine(spawnObjectANimation(pipeAnimators[0],toySpawnPoint[0],sampleToys[0]));
                assembler.showChoice1.text = "" + panelValue;
            }
            else if (objectName.Contains("Chute 2"))
            {
                print("Chute b2 has a value of " + panelValue);
                manager.GetComponent<assemblyManager>().setChuteTwoChoice(panelValue);
                //StartCoroutine(spawnObjectANimation(pipeAnimators[1], toySpawnPoint[1], sampleToys[1]));
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
                print("Chute 3 has a value of " + panelValue);
                manager.GetComponent<assemblyManager>().setChuteThreeChoice(panelValue);
                //StartCoroutine(spawnObjectANimation(pipeAnimators[2], toySpawnPoint[2], sampleToys[2]));
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
