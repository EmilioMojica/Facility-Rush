using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/*
 * Author Brennan J.C Kersey
 * 
 * Scrit Purpose: To generate an equation based on the grade level that is passed to it, the generated equation is generated and returned to the caller as a string with the intention
 * that the string will return to the manager and be parsed appropriately by the manager.
 * 
 * Equation will be of form (first operand) (operator sign) (second operand) = (answer), Examples: 2+9=11, 100-52=48, 72/9=8, 12*11=132. 
 * 
 * A sample of how I parsed for Assembly Line is contained on lines 
 * 
 */
public class generateEquations : MonoBehaviour
{
    private int gradeLevel;
    int startingNumber;  // randomly generated number for beginning of equation generation for addition and subtraction, unless it is mutiplication or division
    int secondNumber;   //  a randomly generated number raging from 1 to 100
    int firstNumber;    //  a randomly generated number ranging from 1 to 100
    string equationOperator; // a string character that represents the operator in the equation
    string[] operators = new string[] {"+","-","*","/" };   // an array to hold the possible operators used by the equation
                                                            // Use this for initialization
    private int[] factorsOf100 = new int[] { 1, 2, 4, 5, 10, 25, 50, 100 };
    private int[] factorsOf144 = new int[] {1,2,3,4,6,8,9,12,16,18,24,36,48,72,144};
    private int[] factorsOf900 = new int[] { 1, 2, 3, 4, 5, 6, 9, 10, 12, 15, 18, 20, 25, 30, 36, 45, 50, 60, 75, 90, 100, 150, 180, 225, 300, 450, 900 };
    public string generateEquation(int gradeLevel) // method for generating equations that calls different methods of equation generation based on the grade level given
    {
        string equation="";      // variable for holding generated equation
        switch(gradeLevel)       // switch case for sorting through gradelevels
        {
            case 0:              // case representing kindergarten
                startingNumber = (int)Random.Range(1, 11);
                firstNumber = (int)Random.Range(1, startingNumber);
                secondNumber = startingNumber - firstNumber;
                equation= ""+ secondNumber+"+" + firstNumber+"="+ startingNumber;
               // print(equation);
                
                break;
            case 1:              // case representing first grade
                equation =generateFirstGradeProblem();
                break;
            case 2:             //  case representing second grade
                equation = generateSecondGradeProblem();
                break;
            case 3:             // case representing third grade
                equation = generateThirdGradeProblem();
                break;
            case 4:             // case representing fourth grade
                equation = generateFourthGradeProblem();
                break;
            case 5:             // case representing fifth grade 
                equation = generateFifthGradeProblem();
                break;
            default:
                print("No grade found");
                break;
        }
        return equation;
    }
    public string generateFirstGradeProblem()    // First Grade problem generator
    {
        int operatorDecider = (int)Random.Range(0, 2);  // random number generation to decide operator/ Type of problem
        string equation = "";
        startingNumber = (int)Random.Range(1, 21);
        firstNumber = (int)Random.Range(1, startingNumber);
        secondNumber = startingNumber - firstNumber;
        switch (operatorDecider) // based on result of operatorDecider generates either addition or subtraction problem
        {
            case 0:
                equationOperator = operators[0];
                equation = "" + secondNumber + equationOperator + firstNumber + "=" + startingNumber;
                break;

            case 1:
                equationOperator = operators[1];
                equation = "" + startingNumber + equationOperator + firstNumber + "=" + secondNumber;
                break;
            
            default:
                break;
        }
        return equation;
         
    }

    public string generateSecondGradeProblem()  // Second Grade problem generator
    {
        int operatorDecider = (int)Random.Range(1, 3);
        string equation = "";
        //startingNumber = (int)Random.Range(1, 100);
        //firstNumber = (int)Random.Range(1, startingNumber);
        //secondNumber = startingNumber - firstNumber;
        switch (operatorDecider) // based on result of operatorDecider generates either addition or subtraction problem
        {
            case 1:
                equationOperator = operators[0];
                startingNumber = factorsOf100[(int)Random.Range(0, 8)];
                firstNumber = (int)Random.Range(0, 10);
                secondNumber = startingNumber + firstNumber;
                equation = "" + startingNumber + equationOperator + firstNumber + "=" + secondNumber;
                break;

            case 2:
                equationOperator = operators[1];
                startingNumber = factorsOf100[(int)Random.Range(0, 8)];
                firstNumber = (int)Random.Range(0, 10);
                if(startingNumber<firstNumber)
                {
                    startingNumber = factorsOf100[(int)Random.Range(4,factorsOf100.Length)];
                }
                secondNumber = startingNumber - firstNumber;
                equation = "" + startingNumber + equationOperator + firstNumber + "=" + secondNumber;
                break;

            default:
                break;
        }
        return equation;

    }

    public string generateThirdGradeProblem()  // Third through Fifth Grade problem generator
    {
        int operatorDecider = (int)Random.Range(1, 4);// random number generation to decide operator/ Type of problem
        string equation = "";
        //startingNumber = (int)Random.Range(1, 101);
        //firstNumber = (int)Random.Range(1, startingNumber);
        //secondNumber = startingNumber - firstNumber;
        switch (operatorDecider) // based on result of operatorDecider generates either addition or subtraction problem
        {
            case 1:
                equationOperator = operators[0];
                startingNumber = factorsOf100[(int)Random.Range(0, 8)];
                firstNumber = (int)Random.Range(0, 10);
                secondNumber = startingNumber + firstNumber;
                equation = "" + startingNumber + equationOperator + firstNumber + "=" + secondNumber;
                break;

            case 2:
                equationOperator = operators[1];
                startingNumber = factorsOf100[(int)Random.Range(0, 8)];
                firstNumber = (int)Random.Range(0, 10);
                if (startingNumber < firstNumber)
                {
                    startingNumber = factorsOf100[(int)Random.Range(4, factorsOf100.Length)];
                }
                secondNumber = startingNumber - firstNumber;
                equation = "" + startingNumber + equationOperator + firstNumber + "=" + secondNumber;
                break;

            case 3:
                equationOperator = operators[2];
                firstNumber = (int)Random.Range(1, 10);
                secondNumber = (int)Random.Range(1, 13);
                startingNumber = firstNumber * secondNumber;
                equation = "" + firstNumber + "*"+ secondNumber + "="+startingNumber;
                break;
            default:
                break;
        }
        return equation;

    }

    public string generateFourthGradeProblem()
    {
        string equation = "";
        int operatorDecider = (int)Random.Range(2, 4);
        switch (operatorDecider)
        {
            case 2:
                equationOperator = operators[2];
                startingNumber = factorsOf100[(int)Random.Range(0, factorsOf100.Length)];
                firstNumber= (int)Random.Range(1, 13);
                secondNumber = startingNumber * firstNumber;
                equation = "" + startingNumber + equationOperator + firstNumber + "=" +secondNumber;
                break;
            case 3:
                equationOperator = operators[3];
                int remainder;
                do
                {
                    startingNumber = factorsOf100[(int)Random.Range(0, factorsOf100.Length)];
                    firstNumber = (int)Random.Range(1, 13);
                    remainder = startingNumber % firstNumber;
                    secondNumber = startingNumber / firstNumber;
                } while (remainder != 0);
                equation = "" + startingNumber + equationOperator + firstNumber + "=" + secondNumber;
                break;
        }
        return equation;
    }

    public string generateFifthGradeProblem()
    {
        int decider = (int)Random.Range(0,2);
        string equation = "";
        int remainder;
        switch(decider)
        {
            case 0:
                do
                {
                    startingNumber = factorsOf900[(int)Random.Range(0, factorsOf900.Length)];
                    firstNumber = (int)Random.Range(1, 13);
                    remainder = startingNumber % firstNumber;
                    secondNumber = startingNumber / firstNumber;
                } while (remainder != 0);
                equation = "" + startingNumber + "/" +firstNumber+"="+secondNumber;
                break;
            case 1:
                do
                {
                    startingNumber = factorsOf144[(int)Random.Range(0, factorsOf144.Length)];
                    firstNumber = (int)Random.Range(1, 13);
                    remainder = startingNumber % firstNumber;
                    secondNumber = startingNumber / firstNumber;
                } while (remainder != 0);
                equation = "" + startingNumber + "/" + firstNumber + "=" + secondNumber;
                break;
            default:
                break;
        }
        return equation;
    }
    /*
    public void breakdownEquation(string wholeEquation) // breaks down individual parts of equation using for loop and specific key characters +,-,/,*,=
    {
        string numericalValue = "";                     // string for containing each part during for loop
        for (int i = 0; i < wholeEquation.Length; i++)
        {
            // print("wholeEquation[i] is " + wholeEquation[i]);
            if (wholeEquation[i].Equals('+') || wholeEquation[i].Equals('-') || wholeEquation[i].Equals('*') || wholeEquation[i].Equals('/')) // assigns first operand value
            {
                firstPart = ExpressionEvaluator.Evaluate<int>(numericalValue);
                //operatorSign = ""+wholeEquation[i];
                numericalValue = "";
            }
            else if (wholeEquation[i].Equals('='))                              // assigns second operand value
            {
                secondPart = ExpressionEvaluator.Evaluate<int>(numericalValue);
                numericalValue = "";
            }
            else                                                                // adds character to string
            {
                numericalValue += wholeEquation[i];
                //print("Right now numerical value is: " + numericalValue);
            }
        }
        answer = ExpressionEvaluator.Evaluate<int>(numericalValue);             // takes remaining portion of string and assigns that value to answer variable. 
    }
    */
}

