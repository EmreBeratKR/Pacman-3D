using System;
using UnityEngine;

public class GameController : Scenegleton<GameController>
{
    private float scatterTimer;



    private GameMode gameMode;
    public static GameMode GameMode => Instance.gameMode;

    private bool gameStarted = false;
    public static bool GameStarted => Instance.gameStarted;

    public static bool IsCriticalScatter => Instance.scatterTimer <= Instance.settings.criticalScatterTime;


    private void Update()
    {
        TryExitScatterMode();
    }

    private bool TryExitScatterMode()
    {
        scatterTimer -= Time.deltaTime;

        if (scatterTimer <= 0)
        {
            EnterChaseMode();
            return true;
        }

        return false;
    }


    public static void StartGame()
    {
        Instance.gameStarted = true;
        EnterChaseMode();
    }

    public static void EnterScatterMode()
    {
        Instance.gameMode = GameMode.Scatter;
        Instance.scatterTimer = Instance.settings.scatterTime;
    }

    public static void EnterChaseMode()
    {
        Instance.gameMode = GameMode.Chase;
        Instance.scatterTimer = 0;
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
