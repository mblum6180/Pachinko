using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    [System.Serializable]
    public class HighScoreEntry
    {
        public string playerName;
        public int score;
    }

    [System.Serializable]
    public class HighScoreList
    {
        public List<HighScoreEntry> highScores;
    }

    public int maxHighScores = 10;

    private List<HighScoreEntry> highScores;

    private void Awake()
    {
        highScores = LoadHighScores();

        if (highScores.Count == 0)
        {
            highScores = GenerateDefaultHighScores();
            SaveHighScores();
        }
    }

    private List<HighScoreEntry> LoadHighScores()
    {
        string jsonString = PlayerPrefs.GetString("HighScores", "");
        if (!string.IsNullOrEmpty(jsonString))
        {
            return JsonUtility.FromJson<HighScoreList>(jsonString).highScores;
        }
        return new List<HighScoreEntry>();
    }

    private void SaveHighScores()
    {
        HighScoreList highScoreList = new HighScoreList();
        highScoreList.highScores = highScores.Take(maxHighScores).ToList();

        string jsonString = JsonUtility.ToJson(highScoreList);
        PlayerPrefs.SetString("HighScores", jsonString);
        PlayerPrefs.Save();
    }

    private List<HighScoreEntry> GenerateDefaultHighScores()
    {
        List<HighScoreEntry> defaultHighScores = new List<HighScoreEntry>();
        for (int i = 0; i < maxHighScores; i++)
        {
            HighScoreEntry entry = new HighScoreEntry();
            entry.playerName = "Player" + (i + 1);
            entry.score = Random.Range(100, 1000);
            defaultHighScores.Add(entry);
        }
        return defaultHighScores;
    }

    // Other methods to manipulate high scores...

}
