using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGrade : MonoBehaviour
{

	public void setGradeLevel(int gradeLevel)
    {
        PlayerPrefs.SetInt("grade", gradeLevel);
        print("Ths is the grade level "+ gradeLevel );
    }
}
