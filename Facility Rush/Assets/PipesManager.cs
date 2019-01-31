using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipesManager : MonoBehaviour
{
    public Text textOne;
    public Text textTwo;
    public Text textThree;
    public Text textFour;
    private List<int> listOne = new List<int>();
    private List<int> listTwo = new List<int>();
    private List<int> listThree = new List<int>();
    private List<int> listFour = new List<int>();

    private int showList;
    // Start is called before the first frame update
    void Start()
    {
        showList = 0;
        for (int i = 0; i < 10; i++)
        {
            listOne.Add(Random.Range(0,10));
            listTwo.Add(Random.Range(0, 10));
            listThree.Add(Random.Range(0, 10));
            listFour.Add(Random.Range(0, 10));
        }
        ShowText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowText()
    {
        textOne.text = listOne[showList].ToString();
        textTwo.text = listTwo[showList].ToString();
        textThree.text = listThree[showList].ToString();
        textFour.text = listFour[showList].ToString();
    }

    public void NextNumbers()//test function
    {
        showList++;
        ShowText();
    }


}
