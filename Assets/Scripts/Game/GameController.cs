using System;
using System.Collections;
using UnityEngine;

public class GameController : Scenegleton<GameController>
{
    private float scatterTimer;
    public static float RemainingScatterTime => Instance.scatterTimer;


    public static float CriticalScatterTime => Instance.settings.criticalScatterTime;

    private GameMode gameMode;
    public static GameMode GameMode => Instance.gameMode;

    private bool gameStarted = false;
    public static bool GameStarted => Instance.gameStarted;

    private bool isFreezed = false;
    public static bool IsFreezed => Instance.isFreezed;

    public static bool IsCriticalScatter => Instance.scatterTimer <= Instance.settings.criticalScatterTime;


    private void OnEnable()
    {
        EventSystem.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        EventSystem.OnGameOver -= OnGameOver;
    }

    private void Update()
    {
        TryExitScatterMode();
    }

    private bool TryExitScatterMode()
    {
        if (isFreezed) return false;

        scatterTimer -= Time.deltaTime;

        if (scatterTimer <= 0)
        {
            EnterChaseMode();
            return true;
        }

        return false;
    }

    private void OnGameOver(int score, int lifeLeft)
    {
        StartCoroutine(GameOverCoroutine());

        IEnumerator GameOverCoroutine()
        {
            yield return PacmanParticleEmitter.EmitDieParticles();

            if (lifeLeft == 0)
            {
                ConsumableContainer.Destroy();
                
                yield return GameOverTitle.Show();
            }
            else
            {
                yield return SceneController.RestartGame();
            }
        }
    }


    public static void Init()
    {
        Pacman.RestoreLives();
        CurrentScore.ResetScore();
    }

    public static void StartGame()
    {
        Instance.gameStarted = true;
        EnterChaseMode();
    }

    public static void FreezeGame()
    {
        Instance.isFreezed = true;
    }

    public static void UnFreezeGame()
    {
        Instance.isFreezed = false;
    }

    public static void EnterChaseMode()
    {
        Instance.gameMode = GameMode.Chase;
        Instance.scatterTimer = 0;

        foreach (var ghost in GhostContainer.All)
        {
            ghost.EnterChaseMode();
        }

        EventSystem.GameModeChanged(Instance.gameMode);
    }

    public static void EnterScatterMode()
    {
        Instance.gameMode = GameMode.Scatter;
        Instance.scatterTimer = Instance.settings.scatterTime;
        
        foreach (var ghost in GhostContainer.All)
        {
            ghost.EnterScatterMode();
        }

        EventSystem.GameModeChanged(Instance.gameMode);
    }








    [SerializeField] private GameSettings settings;

    [Serializable]
    internal struct GameSettings
    {
        public float scatterTime;
        public float criticalScatterTime;
    }
}

public enum GameMode
{
    Chase,
    Scatter
}
