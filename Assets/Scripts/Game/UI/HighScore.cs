using UnityEngine;
using TMPro;

public class HighScore : Scenegleton<HighScore>
{
    private const string PlayerPrefs_HighScore = "High Score";

    [SerializeField] private TextMeshProUGUI counter;

    public static int Value
    {
        get => PlayerPrefs.GetInt(PlayerPrefs_HighScore, 0);
        private set => PlayerPrefs.SetInt(PlayerPrefs_HighScore, value);
    }


    private void Start()
    {
        UpdateHighScoreCounter();
    }

    private void OnEnable()
    {
        EventSystem.OnGameWin += OnGameWin;
        EventSystem.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        EventSystem.OnGameWin -= OnGameWin;
        EventSystem.OnGameOver -= OnGameOver;
    }

    private void OnGameWin(int score)
    {
        CheckForNewHighScore(score);
    }

    private void OnGameOver(int score, int lifeLeft)
    {
        if (lifeLeft != 0) return;

        CheckForNewHighScore(score);
    }

    private void UpdateHighScoreCounter()
    {
        counter.text = Value.ToString();
    }

    public static void CheckForNewHighScore(int newScore)
    {
        if (newScore > Value)
        {
            Value = newScore;
            Instance.UpdateHighScoreCounter();
        }
    }
}
