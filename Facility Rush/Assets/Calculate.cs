using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculate : MonoBehaviour {
    public static Calculate instance;

    private void Awake()
    {
        instance = this;
    }

    public Item slot01;
    public Item slot02;

    //public Recipe[] recipes;

    public Transform resultsParent;
    public GameObject itemPrefab;
    public GameObject ghostItemPrefab;

    private List<Item> knownItems;

    private void Start()
    {
        //recipes = Resources.LoadAll<Recipe>("Recipes");
    }

    public void Addup( int slot)
    {
        if (slot == 1)
        {
            //slot01 = item;
        }else if (slot == 2)
        {
           // slot02 = item;
        }
        
    }

    public void UpdateResult()
    {
        ClearPreviousResult();
        
    }

    void ClearPreviousResult()
    {
        foreach (Transform child in resultsParent)
        {
            Destroy(child.gameObject);
        }
    }
}
