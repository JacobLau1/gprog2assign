using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static int Score = 0;
    public Text ScoreDisplay;
 
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    public void IncrementScore()
    {
        Score += 50;
    }

    public float GetScore()
    {
        return Score;
    }

    public void Update()
    {
        ScoreDisplay.text = "Score: " + Mathf.Round(Score);
    }
}
