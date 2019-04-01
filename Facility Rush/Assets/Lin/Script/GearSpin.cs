using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSpin : MonoBehaviour
{
    public int speed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 180), speed * Time.deltaTime);
    }
}
