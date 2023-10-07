using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 Instance { get; private set; }
    public static float Score;
    public Text ScoreDisplay;

    // Start is called before the first frame update
    private void Awake()
    {
        GameManager gm = new GameManager();
        SetScore(gm.GetScore());

        // If there is an instance, and it's not me, delete myself. 
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void IncrementScore()
    {
        Score += 50;
    }

    public float GetScore()
    {
        return Score;
    }

    public void SetScore(float num)
    {
        Score = num;
    }

    public void Update()
    {
        ScoreDisplay.text = "Score: " + Mathf.Round(Score);
    }
}
