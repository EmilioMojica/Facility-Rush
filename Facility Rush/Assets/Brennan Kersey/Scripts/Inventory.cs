using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IHasCHanged
{
    private int gradeLevel;
    // Use this for initialization
    //https://www.youtube.com/watch?v=c47QYgsJrWc
    [SerializeField] private Transform slots;
    [SerializeField] private Transform slotsTwoByTwo;

    private float totalNumber;
    private float neednumber;
    public GameObject kakuroGameManager;
    private kakuroGameManger manager;

    private int panelNumberToFillBoard;
    private Transform slotsToCheck;

    void Start()
    {
       manager = kakuroGameManager.GetComponent<kakuroGameManger>();

        gradeLevel = PlayerPrefs.GetInt("grade");
      if(gradeLevel==0 || gradeLevel==1)
        {
            slotsToCheck = slotsTwoByTwo;
            panelNumberToFillBoard = 4;
        }
        else
        {
            slotsToCheck = slots;
            panelNumberToFillBoard = 9;
        }
        HasChanged();
    }


    public void HasChanged()        // method used to update string function containing numbers in row and other things
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder(); // initializtion of string builder 
        builder.Append(" - ");                                               // appending seperator
        float number = 0;                                                    // number used to represent sum as it is added
        int boxCount = 0;
        foreach (Transform sloTransform in slotsToCheck)
        {
            GameObject item = sloTransform.GetComponent<Slot>().item;
            if (item)
            {

                boxCount++;    
                string str = string.Empty;
                str = item.name;
                int theNumber = int.Parse(str);
                builder.Append(item.name);
                
                builder.Append(" - ");
                
                number += theNumber;
                neednumber = number;
            }
        }

        manager.updateSum();
        if(boxCount==panelNumberToFillBoard)
        {
            manager.checkBoard();
        }

        totalNumber = neednumber;
    }

    
}
namespace UnityEngine.EventSystems
{
    public interface IHasCHanged : IEventSystemHandler
    {
        void HasChanged();
    }
}