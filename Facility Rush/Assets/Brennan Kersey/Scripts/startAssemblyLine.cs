using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startAssemblyLine : MonoBehaviour
{
    [SerializeField] private assemblyManager assembler;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startAssemblyTimer()
    {
        assembler.gameOver = false;
    }
}
