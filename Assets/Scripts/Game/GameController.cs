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
        EventSystem.OnGameWin += OnGameWin;
        EventSystem.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        EventSystem.OnGameWin -= OnGameWin;
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

    private void OnGameWin(int score)
    {
        AudioManager.StopGhostMove();
        AudioManager.StopGhostTurnBlue();

        GhostContainer.HideAll();
        GhostContainer.ClearDeadGhosts();

        StartCoroutine(GameWinCoroutine());

        IEnumerator GameWinCoroutine()
        {
            yield return WinAnimation.Play();

            ConsumableContainer.Destroy();

            yield return TextWriter.WriteGameWin();

            yield return SceneTransition.FadeIn();
        
            SceneController.LoadMainMenu();

            yield return SceneTransition.FadeOut();
        }
    }

    private void OnGameOver(int score, int lifeLeft)
    {
        AudioManager.StopGhostMove();
        AudioManager.StopGhostTurnBlue();

        GhostContainer.HideAll();
        GhostContainer.ClearDeadGhosts();

        StartCoroutine(GameOverCoroutine());

        IEnumerator GameOverCoroutine()
        {
            AudioManager.PlayGameOver();

            yield return PacmanParticleEmitter.EmitDieParticles();

            while (AudioManager.IsGameOverPlaying) yield return null;

            if (lifeLeft == 0)
            {
                yield return TextWriter.WriteGameOver();

                yield return SceneTransition.FadeIn();
                
                ConsumableContainer.Destroy();
            
                SceneController.LoadMainMenu();

                yield return SceneTransition.FadeOut();
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
        AudioManager.PlayGhostMove();
    }

    public static void FreezeGame()
    {
        Instance.isFreezed = true;
        AudioManager.StopGhostMove();
        AudioManager.PauseGhostTurnBlue();
    }

    public static void UnFreezeGame()
    {
        Instance.isFreezed = false;
        AudioManager.PlayGhostMove();
        AudioManager.UnPauseGhostTurnBlue();
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
