using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager3 : MonoBehaviour
{
    public static GameManager3 Instance { get; private set; }
    public static int Score;
    public Text ScoreDisplay;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
        Score = GameManager2.Score;
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
