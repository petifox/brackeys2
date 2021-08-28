using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreChecker : MonoBehaviour
{
    public Text score;
    public string scoreText;

    public Text bestScore;
    public string bestScoreText;
    // Start is called before the first frame update
    void Start()
    {
        score.text = string.Format(scoreText, PlayerPrefs.GetInt("latest").ToString());
        bestScore.text = string.Format(bestScoreText, PlayerPrefs.GetInt("highScore").ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
