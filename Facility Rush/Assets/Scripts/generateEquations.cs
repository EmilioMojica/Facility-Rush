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
                equation = generateThirdGradeThroughFifthGradeProblem();
                break;
            case 4:             // case representing fourth grade
                equation = generateThirdGradeThroughFifthGradeProblem();
                break;
            case 5:             // case representing fifth grade 
                equation = generateThirdGradeThroughFifthGradeProblem();
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
        startingNumber = (int)Random.Range(1, 101);
        firstNumber = (int)Random.Range(1, startingNumber);
        secondNumber = startingNumber - firstNumber;
        switch (operatorDecider) // based on result of operatorDecider generates either addition or subtraction problem
        {
            case 1:
                equationOperator = operators[0];
                equation = "" + secondNumber + equationOperator + firstNumber + "=" + startingNumber;
                break;

            case 2:
                equationOperator = operators[1];
                equation = "" + startingNumber + equationOperator + firstNumber + "=" + secondNumber;
                break;

            default:
                break;
        }
        return equation;

    }

    public string generateThirdGradeThroughFifthGradeProblem()  // Third through Fifth Grade problem generator
    {
        int operatorDecider = (int)Random.Range(1, 5);// random number generation to decide operator/ Type of problem
        string equation = "";
        startingNumber = (int)Random.Range(1, 101);
        firstNumber = (int)Random.Range(1, startingNumber);
        secondNumber = startingNumber - firstNumber;
        switch (operatorDecider) // based on result of operatorDecider generates either addition or subtraction problem
        {
            case 1:
                equationOperator = operators[0];
                equation = "" + secondNumber + equationOperator + firstNumber + "=" + startingNumber;
                break;

            case 2:
                equationOperator = operators[1];
                equation = "" + startingNumber + equationOperator + firstNumber + "=" + secondNumber;
                break;

            case 3:
                equationOperator = operators[2];
                firstNumber = (int)Random.Range(1, 13);
                secondNumber = (int)Random.Range(1, 13);
                startingNumber = firstNumber * secondNumber;
                equation = "" + firstNumber + "*"+ secondNumber + "="+startingNumber;
                break;
            case 4:
                equationOperator = operators[3];
                firstNumber = (int)Random.Range(1, 13);
                secondNumber = (int)Random.Range(1, 13);
                startingNumber = firstNumber * secondNumber;
                equation = "" + startingNumber + "/" + secondNumber + "=" + firstNumber;
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

