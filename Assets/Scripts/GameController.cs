using UnityEngine;

public class GameController : Scenegleton<GameController>
{
    private GameMode gameMode;
    public static GameMode GameMode => Instance.gameMode;

    private bool gameStarted = false;
    public static bool GameStarted => Instance.gameStarted;


    public static void StartGame()
    {
        Instance.gameStarted = true;
        Instance.gameMode = GameMode.Chase;
    }
}

public enum GameMode
{
    Chase,
    Scatter
}