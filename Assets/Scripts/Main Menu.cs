using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private List<TextMeshProUGUI> highScoresTexts;

    // Start is called before the first frame update
    private void Start()
    {
        SetHighScore();
    }

    public void NewGame()
    {
        HighScores.Instance.setCurrentUser(usernameInput.text);
        SceneManager.LoadScene(1);

    }

    private void SetHighScore()
    {
        int nScores = HighScores.Instance.GetNHighScores();
        if (nScores > 5){
            nScores = 5;
        }

        for (int i=0; i < nScores; i++)
        {
            (string username, float score) = HighScores.Instance.GetHighScore(i);
            int minutes = Mathf.FloorToInt(score / 60);
            int secondes = Mathf.FloorToInt(score % 60);
            highScoresTexts[i].text = $"{i + 1}. {username} - {minutes}:{secondes}";
        }
    }
}
