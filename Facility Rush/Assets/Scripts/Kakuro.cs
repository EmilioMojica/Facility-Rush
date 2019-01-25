using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kakuro : MonoBehaviour {
   public Text[] indices=new Text[9];
    public Text sumOne;
    public Text sumTwo;
    private int firstSum;
    private int secondSum;
    int[] kakuroBoardKindergartenThroughFirst = new int[] { 1, 2, 3, 4 };
    int[] kakuroBoardSecondThroughFifth = new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    int[] numbersLeft = new int[6];
    int[] lastNumber= new int[3];
    public int grade;
    // Use this for initialization

    public int getFirstSum()
    {
        return firstSum;
    }

    public int getSecondSum()
    {
        return secondSum;
    }
    void Start ()
    {
        print("Start is called");
        //firstSum = (int)Random.Range(6, 25);  //(int)Random.Range(6, 25)
        //secondSum = (int)Random.Range(6, 25);
        //sumOne.text = firstSum + "";
        //sumTwo.text = secondSum + "";
        //generateBoard();

        //calculateSums();
    }

    
	
	// Update is called once per frame
	void Update () {
		
	}

    public void generateBoard()
    {
        print(kakuroBoardSecondThroughFifth[8]);
        //print((int)Random.Range(0, 10));
        int n = 9;
        while (n > 1)
        {
            int index = (int)Random.Range(0, 9);
            //print("Index is: " + index);
            int temp = kakuroBoardSecondThroughFifth[n - 1];
            kakuroBoardSecondThroughFifth[n - 1] = kakuroBoardSecondThroughFifth[index];
            kakuroBoardSecondThroughFifth[index] = temp;
            n--;
        }

        sumOne.text = (kakuroBoardSecondThroughFifth[0] + kakuroBoardSecondThroughFifth[1] + kakuroBoardSecondThroughFifth[2]) + "";

        sumTwo.text= (kakuroBoardSecondThroughFifth[3] + kakuroBoardSecondThroughFifth[4] + kakuroBoardSecondThroughFifth[5]) + "";

        for (int i = 0; i < 9; i++)
        {
            indices[i].text= kakuroBoardSecondThroughFifth[i]+"";
        }


    }

    public void generateBoardKindergartenThroughFirst()
    {
        int n = 4;
        while(n>1)
        {
            int index = (int)Random.Range(0, 4);
            int temp = kakuroBoardKindergartenThroughFirst[n - 1];
            kakuroBoardKindergartenThroughFirst[n - 1] = kakuroBoardKindergartenThroughFirst[index];
            kakuroBoardKindergartenThroughFirst[index] = temp;
            n--;
        }

        firstSum = kakuroBoardKindergartenThroughFirst[0] + kakuroBoardKindergartenThroughFirst[1];
        secondSum = kakuroBoardKindergartenThroughFirst[2] + kakuroBoardKindergartenThroughFirst[3];

        indices[0].text = kakuroBoardKindergartenThroughFirst[0]+"";
        indices[1].text = kakuroBoardKindergartenThroughFirst[1] + "";
        indices[3].text = kakuroBoardKindergartenThroughFirst[2] + "";
        indices[4].text = kakuroBoardKindergartenThroughFirst[3] + "";

        sumOne.text = firstSum + "";
        sumTwo.text = secondSum + "";
    }



    public void calculateSumsSecondThroughFifth()
    {
        bool isPossible = false;
        while (isPossible == false)
        {
             firstSum = (int)Random.Range(6, 25);  //(int)Random.Range(6, 25)
             secondSum = (int)Random.Range(6, 25);
            if (checkifPossible(firstSum, secondSum))
            {
                sumOne.text = firstSum + "";
                sumTwo.text = secondSum + "";
                isPossible = true;
            }
        }
       
    }

    public bool checkifPossible(int sumOne, int sumTwo)
    {
        bool foundSumOne = false;
        int firstNum = 0;
        int secondNum = 0;
        int thirdNum = 0;

        for (int i = 1; i < 10; i++)
        {
            for (int j = 2; j < 10; j++)
            {
                for (int k = 3; k < 10; k++)
                {
                    if (i + j + k == sumOne && i != j && i != k && j != k)
                    {
                        firstNum = i;
                        secondNum = j;
                        thirdNum = k;

                        //indices[0].text = firstNum + "";
                        //indices[1].text = secondNum + "";
                        //indices[2].text = thirdNum + "";

                        //print("the three numbers for firstSum are: " + firstNum + ", " + secondNum + ", " + thirdNum);
                        makeArrayOfLeftOverNumbersThreeByThree(firstNum, secondNum, thirdNum);
                        foundSumOne = true;
                        bool foundSumTwo = findSecondSum(firstNum, secondNum, thirdNum, sumTwo, numbersLeft);

                        if(foundSumOne==true && foundSumTwo==true)
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    public int[] makeArrayOfLeftOverNumbersThreeByThree(int firstNum,int secondNum, int thirdNum)
    {
        int index = 0;
        for (int i = 0; i < 9; i++)
        {
            //print("i is " + i);
            if (kakuroBoardSecondThroughFifth[i] != firstNum && kakuroBoardSecondThroughFifth[i] != secondNum && kakuroBoardSecondThroughFifth[i] != thirdNum)
            {
                //print("index is " + index + " and kakuroBoard["+i+"] is "+ kakuroBoard[i]);
                numbersLeft[index] = kakuroBoardSecondThroughFifth[i];
                index++;
            }
        }
        return numbersLeft;
    }

    public void lastThree(int fourthNum,int fifthNum, int sixthNum)
    {
        int index = 0;
        for(int i=0;i<6;i++)
        {
            if(numbersLeft[i]!= fourthNum && numbersLeft[i] != fifthNum && numbersLeft[i] != sixthNum)
            {
                lastNumber[index] = numbersLeft[i];
                index++;
            }
        }

       // indices[6].text = lastNumber[0]+"";
       // indices[7].text = lastNumber[1] + "";
       // indices[8].text = lastNumber[2] + "";

    }

    public bool findSecondSum(int firstNum, int secondNum,int thirdNum, int secondSum, int [] numbersLeft)
    {
        int fourthNum = 0;
        int fifthNum = 0;
        int sixthNum = 0;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 1; j < 6; j++)
            {
                for (int k = 2; k < 6; k++)
                {
                    if (numbersLeft[i] + numbersLeft[j] + numbersLeft[k] == secondSum && numbersLeft[i]!= numbersLeft[j] && numbersLeft[i]!= numbersLeft[k]&& numbersLeft[j]!=numbersLeft[k])
                    {
                        fourthNum = numbersLeft[i];
                        fifthNum = numbersLeft[j];
                        sixthNum = numbersLeft[k];

                       // indices[3].text = fourthNum + "";
                       // indices[4].text = fifthNum + "";
                       // indices[5].text = sixthNum + "";

                        lastThree(fourthNum, fifthNum, sixthNum);

                       // print("the three numbers for secondSum are: " + fourthNum + ", " + fifthNum + ", " + sixthNum);
                        return true;
                    }
                }
            }
        }
        return false;
    }
}


