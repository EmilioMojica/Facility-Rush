using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickablePipePanels : MonoBehaviour
{
    public GameObject manager;
    private assemblyManager assembler;
    [SerializeField] private GameObject[] sampleToys;
    [SerializeField] private Animator[] pipeAnimators;
    [SerializeField] private Transform[] toySpawnPoint;
    public void obtainPanelValue()
    {
        assembler = manager.GetComponent<assemblyManager>();
        string objectName = gameObject.name;
        print("htis is object name " + objectName);
        string panelValue = gameObject.GetComponentInChildren<Text>().text;

        if (objectName.Contains("Chute 1"))
        {
            print("Chute 1 has a value of " + panelValue);
            manager.GetComponent<assemblyManager>().setChuteOneChoice(panelValue);
            StartCoroutine(spawnObjectANimation(pipeAnimators[0],toySpawnPoint[0],sampleToys[0]));
            assembler.showChoice1.text = "" + panelValue;
        }
        else if (objectName.Contains("Chute 2"))
        {
            print("Chute b2 has a value of " + panelValue);
            manager.GetComponent<assemblyManager>().setChuteTwoChoice(panelValue);
            StartCoroutine(spawnObjectANimation(pipeAnimators[1], toySpawnPoint[1], sampleToys[1]));
            assembler.showChoice2.text = "" + panelValue;
        }
        else if (objectName.Contains("Chute 3"))
        {
            print("Chute 3 has a value of " + panelValue);
            manager.GetComponent<assemblyManager>().setChuteThreeChoice(panelValue);
            StartCoroutine(spawnObjectANimation(pipeAnimators[2], toySpawnPoint[2], sampleToys[2]));
            assembler.showChoice3.text = "" + panelValue;
        }
    }
    IEnumerator spawnObjectANimation(Animator anime, Transform spawnPoint, GameObject toyPart)
    {
        anime.SetBool("choiceMade", true);
        yield return new WaitForSecondsRealtime(1.3f);
        Instantiate(toyPart, spawnPoint.position, spawnPoint.rotation);
        anime.SetBool("toySpawned", true);
        anime.SetBool("choiceMade", false);
        yield return new WaitForSecondsRealtime(1);
        anime.SetBool("toySpawned", false);
        anime.SetBool("isFinish", true);
        yield return new WaitForSecondsRealtime(1);
        anime.SetBool("isFinish", false);
       
    }
}
