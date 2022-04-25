using UnityEngine;
using TMPro;

public class CurrentScore : Scenegleton<CurrentScore>
{
    private const string PlayerPrefs_CurrentScore = "Current Score";
    private const int StartingScore = 0;
    private const int SingleGhostKillScore = 200;
    private const int DoubleGhostKillScore = 400;
    private const int TripleGhostKillScore = 800;
    private const int QuadrupleGhostKillScore = 1600;


    [SerializeField] private TextMeshProUGUI counter;
    private int ghostKillCount = 0;

    public static int Score
    {
        get => PlayerPrefs.GetInt(PlayerPrefs_CurrentScore, StartingScore);
        private set => PlayerPrefs.SetInt(PlayerPrefs_CurrentScore, value);
    }


    public static void ResetScore()
    {
        Score = StartingScore;
        Instance.UpdateScoreCounter();
    }


    private void Start()
    {
        UpdateScoreCounter();   
    }

    private void OnEnable()
    {
        EventSystem.OnGhostKilled += OnGhostKilled;
        EventSystem.OnGameModeChanged += OnGameModeChanged;
        EventSystem.OnConsumableConsumed += OnConsumableConsumed;
    }

    private void OnDisable()
    {
        EventSystem.OnGhostKilled -= OnGhostKilled;
        EventSystem.OnGameModeChanged -= OnGameModeChanged;
        EventSystem.OnConsumableConsumed -= OnConsumableConsumed;
    }

    private void OnGhostKilled()
    {
        ghostKillCount++;

        var score = SingleGhostKillScore;

        if (ghostKillCount == 2) score = DoubleGhostKillScore; 

        else if (ghostKillCount == 3) score = TripleGhostKillScore;

        else if (ghostKillCount == 4) score = QuadrupleGhostKillScore;

        AddScore(score);
        UpdateScoreCounter();

        PacmanParticleEmitter.EmitScoreParticle(score);
    }

    private void OnGameModeChanged(GameMode gameMode)
    {
        ghostKillCount = 0;
    }

    private void OnConsumableConsumed(int score)
    {
        AddScore(score);
        UpdateScoreCounter();
    }


    private void AddScore(int score)
    {
        Score += score;
    }

    private void UpdateScoreCounter()
    {
        counter.text = Score.ToString();
    }
}
