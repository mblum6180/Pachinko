using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{

    public HighScoreManager highScoreManager;
    public TextMeshProUGUI highScoreText;
        
    void Start()
    {
        UpdateHighScoreText();
    }


    void UpdateHighScoreText()
    {
        var highScores = highScoreManager.GetHighScores();

        highScoreText.text = "";

        foreach (var highScore in highScores)
        {
            highScoreText.text += highScore.playerName + ": " + highScore.score.ToString() + "\n";
        }
    }


}
