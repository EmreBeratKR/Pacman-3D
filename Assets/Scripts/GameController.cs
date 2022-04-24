using UnityEngine;

public class GameController : Scenegleton<GameController>
{
    private GameMode gameMode;
    public static GameMode GameMode => Instance.gameMode;


    private void Start()
    {
        gameMode = GameMode.Chase;
    }
}

public enum GameMode
{
    Chase,
    Scatter
}