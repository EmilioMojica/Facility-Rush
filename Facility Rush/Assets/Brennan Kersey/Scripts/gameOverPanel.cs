using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameOverPanel : MonoBehaviour
{
    [SerializeField] private Text congratulationsText;
    [SerializeField] private Text newHighScoreText;
    [SerializeField] private Text scoreOnGameOverPanel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateNewHighScoreFeedback()
    {
        newHighScoreText.gameObject.SetActive(true);
    }
}
