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
        EventSystem.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        EventSystem.OnGameOver -= OnGameOver;
    }

    private void OnGameOver(int score, int lifeLeft)
    {
        if (lifeLeft != 0) return;

        if (score > Value)
        {
            Value = score;
            UpdateHighScoreCounter();
        }
    }

    private void UpdateHighScoreCounter()
    {
        counter.text = Value.ToString();
    }
}
