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
   // [SerializeField] private Text inventoryText;
    //[SerializeField] private Text answerText;
    private float totalNumber;
    private float neednumber;
    public GameObject kakuroGameManager;
    private kakuroGameManger manager;
    //private float[] allNumbers = new float[9];
    private int panelNumberToFillBoard;
    private Transform slotsToCheck;

    void Start()
    {
       manager = kakuroGameManager.GetComponent<kakuroGameManger>();
        //panelNumberToFillBoard = 4;
        //panelNumberToFillBoard = manager.getTotalNumberedPanels();
        print("Called from start panelNumberToFillBoard is "+ panelNumberToFillBoard);
        print("At start of inventory panelNumberToFilled is "+ panelNumberToFillBoard);
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
        //print("Hello");
        //Debug.Log("new Item!!!!");
        //throw new System.NotImplementedException();
        System.Text.StringBuilder builder = new System.Text.StringBuilder(); // initializtion of string builder 
        builder.Append(" - ");                                               // appending seperator
        float number = 0;                                                    // number used to represent sum as it is added
        int boxCount = 0;
        foreach (Transform sloTransform in slotsToCheck)
        {
            //print("Hello");
            GameObject item = sloTransform.GetComponent<Slot>().item;
            if (item)
            {

                boxCount++;    
                string str = string.Empty;
                str = item.name;
                int theNumber = int.Parse(str);
                Debug.Log(" Number equal to = " + theNumber);
                builder.Append(item.name);
                
                builder.Append(" - ");
                
                //neednumber = theNumber;
                number += theNumber;
                neednumber = number;
            }
            //Debug.Log("Total Number = " + totalNumber);
            

        }

        manager.updateSum();
       // print("The number of boxes for this is: "+ boxCount);
        if(boxCount==panelNumberToFillBoard)
        {
            print("Box count is "+ boxCount+" while panelNumbertoFillBoard is "+ panelNumberToFillBoard);
            manager.checkBoard();
        }
        //allNumbers[i] = 

        totalNumber = neednumber;
        //addNumber(neednumber);
        //totalNumber += neednumber;
        //inventoryText.text = builder.ToString();
        //answerText.text = Convert.ToString(totalNumber);
        
    }

    
}
namespace UnityEngine.EventSystems
{
    public interface IHasCHanged : IEventSystemHandler
    {
        void HasChanged();
    }
}