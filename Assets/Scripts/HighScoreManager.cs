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
        //Resets Prefs for testing
        //PlayerPrefs.DeleteAll();
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

        // Pre-generated list of 100 names
        string[] names = new string[]
        {
            "Adam", "Alex", "Alice", "Amy", "Andrew", "Ann", "Anthony", "Arthur", "Barbara", "Benjamin",
            "Betty", "Bill", "Bob", "Brian", "Carol", "Catherine", "Charles", "Chris", "Cindy", "Dan",
            "David", "Debbie", "Donna", "Dorothy", "Edward", "Elaine", "Elizabeth", "Emily", "Eric", "Eugene",
            "Evelyn", "Frank", "George", "Grace", "Greg", "Helen", "Henry", "Howard", "Irene", "Jack",
            "Jacob", "James", "Janet", "Jason", "Jean", "Jeff", "Jennifer", "Jessica", "Jim", "Joan",
            "Joe", "John", "Joseph", "Josh", "Joyce", "Judy", "Julia", "Julie", "Justin", "Karen",
            "Kathy", "Keith", "Ken", "Kevin", "Kim", "Kyle", "Laura", "Leo", "Linda", "Lisa",
            "Lori", "Louis", "Lucy", "Margaret", "Marie", "Mark", "Martha", "Martin", "Mary", "Matt",
            "Maureen", "Michael", "Michelle", "Mike", "Molly", "Nancy", "Nick", "Nicole", "Oliver", "Patricia",
            "Patrick", "Paul", "Pete", "Rachel", "Ralph", "Randy", "Rebecca", "Richard", "Robert", "Roger",
            "Rose", "Ruth", "Sam", "Sandra", "Sara", "Scott", "Sean", "Sharon", "Shirley", "Stanley",
            "Steve", "Susan", "Teresa", "Tim", "Tom", "Tony", "Tracy", "Victoria", "Walter", "William"
        };


        // Shuffle the names array to ensure randomness
        names = names.OrderBy(x => Random.value).ToArray();

        for (int i = 0; i < maxHighScores; i++)
        {
            HighScoreEntry entry = new HighScoreEntry();
            entry.playerName = names[i];
            entry.score = Random.Range(100, 600);
            defaultHighScores.Add(entry);
        }

        // Order the high scores in descending order
        defaultHighScores = defaultHighScores.OrderByDescending(entry => entry.score).ToList();
        
        return defaultHighScores;
    }

    public bool AddHighScore(string playerName, int score)
    {
        if (IsValidName(playerName))
        {
            HighScoreEntry newEntry = new HighScoreEntry();
            newEntry.playerName = playerName;
            newEntry.score = score;
            Debug.Log("Adding new high score: " + newEntry.playerName + " " + newEntry.score);  
            highScores.Add(newEntry);

            // Sort the high scores in descending order
            highScores = highScores.OrderByDescending(hs => hs.score).ToList();

            // If the list exceeds maxHighScores, remove the last one
            if (highScores.Count > maxHighScores)
            {
                highScores.RemoveAt(highScores.Count - 1);
            }

            SaveHighScores();
            return true;
        }
        else
        {
            Debug.LogWarning("Invalid player name.");
            return false;
        }
    }


    private bool IsValidName(string name)
    {
        if (name.Length < 1 || name.Length > 16) return false;

        // Ensuring only allowed characters
        foreach (char c in name)
        {
            if (!char.IsLetter(c) && c != ' ' && c != '-')
                return false;
        }

        return true;
    }

    public List<HighScoreEntry> GetHighScores()
    {
        return highScores;
    }

    public void ClearHighScores()
    {
        highScores.Clear();
        SaveHighScores();
    }


}
