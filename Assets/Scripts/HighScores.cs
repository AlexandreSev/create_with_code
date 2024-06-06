using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    public static HighScores Instance;

    [System.Serializable]
    class HighScore
    {
        public string username;
        public float score;
    }

    [System.Serializable]
    class HighScoresSerializable
    {
        public List<HighScore> highscores;
    }

    private List<HighScore> highScores;

    private string currentUsername;
    
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        highScores = new List<HighScore>();
        LoadScores();
    }

    public void AddNewScore(float score)
    {
        HighScore newHighScore = new HighScore();
        newHighScore.username = currentUsername;
        newHighScore.score = score;

        highScores.Add(newHighScore);
        highScores.Sort((x, y) => x.score.CompareTo(y.score));
        if (highScores.Count > 5)
        {
            highScores.RemoveAt(highScores.Count - 1);
        }
        SaveScores();
    }

    public void SaveScores()
    {
        HighScoresSerializable saveData = new HighScoresSerializable();
        saveData.highscores = highScores;
        string savePath = Application.persistentDataPath + "/highscores.json";
        string jsonData = JsonUtility.ToJson(saveData);

        File.WriteAllText(savePath, jsonData);
    }

    public void LoadScores()
    {
        string savePath = Application.persistentDataPath + "/highscores.json";
        if ( File.Exists(savePath)){
            string data = File.ReadAllText(savePath);
            highScores = JsonUtility.FromJson<HighScoresSerializable>(data).highscores;
        }
    }

    public (string, float) GetHighScore(int n) // ENCAPSULATION
    {
        if (n >= highScores.Count || n < 0)
        {
            return ("WRONG", 0.0f);
        }
        HighScore highScore = highScores[n];
        return (highScore.username, highScore.score);
    }

    public int GetNHighScores() // ENCAPSULATION
    {
        return highScores.Count;
    }

    public void setCurrentUser(string username) // ENCAPSULATION
    {
        currentUsername = username;
    }
}
