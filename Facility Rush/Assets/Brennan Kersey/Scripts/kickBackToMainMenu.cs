using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class kickBackToMainMenu : MonoBehaviour
{
    [SerializeField] private float timeTillExit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SceneManager.LoadScene("Ford_Test");
        }
    }

    public void activateAutoKick()
    {
        StartCoroutine(kickAfterFive());
    }

    IEnumerator kickAfterFive()
    {
        yield return new WaitForSeconds(timeTillExit);
        SceneManager.LoadScene("Ford_Test");
    }
}
