using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    Text goal;
    int Sum;

    public int a, b, c;

	// Use this for initialization
	void Start ()
    {
        goal = GetComponentInChildren<Text>();
        GenerateSum();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (a != 0 && b != 0 && c != 0)
        {
            CheckSum();
        }
	}

    void CheckSum()
    {
        if (Sum == a+b+c)
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("The Answer is wrong!");
        }
    }

    void GenerateSum()
    {
        int sum = Random.Range(6, 25);
        goal.text = sum.ToString();
        Sum = sum;
    }

    public void SetVariable(Number number)
    {
        if (a == 0)
        {
            a = number.yourNumber;
        }
        else if (b == 0)
        {
            b = number.yourNumber;
        }
        else if(c == 0)
        {
            c = number.yourNumber;
        }
    }
}
