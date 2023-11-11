using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScore : MonoBehaviour
{

    private int score = 0;

    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private TextMeshProUGUI highScoreText;

    private void OnEnable()
    {
        Snake2D.Player.Snake.OnGrabFood += SetScore;
    }

    private int HighScore
    {
        get { return PlayerPrefs.GetInt("HighScore", 0); }
        set { PlayerPrefs.SetInt("HighScore", value); }
    }

    private void Start()
    {
        InitText();
    }

    private void InitText()
    {
        score = 0; 
        SetText();
    }

    public void SetScore(int value)
    {
        score += value;
        if (score >= HighScore)
        {
            HighScore = score;
        }
        SetText();
    }

    private void SetText()
    {
        scoreText.text = "Score : " + score + " ";
        highScoreText.text = "HighScore : " + HighScore + " ";
    }

    private void OnDisable()
    {
        Snake2D.Player.Snake.OnGrabFood -= SetScore;
    }
}
