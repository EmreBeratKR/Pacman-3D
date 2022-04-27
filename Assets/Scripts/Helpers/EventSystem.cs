public static class EventSystem
{
    public delegate void DefaultHandler();
    public delegate void GenericHandler<T>(T arg);
    public delegate void DoubleGenericHandler<T, T1>(T arg1, T1 arg2);
    public delegate void ParamsGenericHandler<T>(params T[] args);

    public static event DefaultHandler OnGhostKilled;
    
    public static event GenericHandler<int> OnGameWin;
    public static event GenericHandler<int> OnConsumableConsumed;
    public static event GenericHandler<GameMode> OnGameModeChanged;

    public static event DoubleGenericHandler<int, int> OnGameOver;




    public static void GhostKilled()
    {
        OnGhostKilled?.Invoke();
    } 


    public static void GameWin(int score)
    {
        OnGameWin?.Invoke(score);
    }

    public static void ConsumableConsumed(int score)
    {
        OnConsumableConsumed?.Invoke(score);
    }
    public static void GameModeChanged(GameMode gameMode)
    {
        OnGameModeChanged?.Invoke(gameMode);
    }


    public static void GameOver(int score, int lifeLeft)
    {
        OnGameOver?.Invoke(score, lifeLeft);
    }
}
