using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float Score = 0;

    // Start is called before the first frame update
    private void Awake()
    {
       // Instance = this;
       // DontDestroyOnLoad(Instance);
    }

    public void IncrementScore()
    {
        // TODO Increment score logic and win condition 
        ++Score;/*
        if (Score >= 4)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   //load next scene
        }
        */
    }
}
