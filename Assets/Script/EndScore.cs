using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class EndScore : MonoBehaviour
{

    public static EndScore Instance { get; private set; }
    public static float Score;
    public Text ScoreDisplay;

    // Start is called before the first frame update
    private void Awake()
    {
        GameManager3 gm = new GameManager3();
        Score = gm.GetScore();
        ScoreDisplay.text = "Score: " + Mathf.Round(Score);
    }
}
