using UnityEngine;

public class Pacman : Scenegleton<Pacman>
{
    public const float TouchRange = GameArea.DotDistance;
    public const int MaxLife = 3;
    private const string PlayerPrefs_RemainingLife = "Remaining Life";


    public static Vector3 DesiredPosition
    {
        get
        {
            var result = Position + PacmanMovement.Up * GameArea.DotDistance * 2;
            result.y = 0;
            return result;
        }
    }

    public static int RemainingLife
    {
        get => PlayerPrefs.GetInt(PlayerPrefs_RemainingLife, MaxLife);
        private set => PlayerPrefs.SetInt(PlayerPrefs_RemainingLife, value);
    }

    private Facing facing = Facing.None;
    public static Facing Facing
    {
        get => Instance.facing;
        set => Instance.facing = value;
    }
    public static Facing InitialFacing => Facing.Right;

    private PacmanState state = PacmanState.Idle;
    public static PacmanState State
    {
        get => Instance.state;
        set => Instance.state = value;
    }

    public static Transform Transform => Instance.transform;
    
    public static Vector3 Position
    {
        get
        {
            var result = Instance.transform.position;
            result.y = 0;
            return result;
        }
    }


    public static void Show()
    {
        Instance.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        Instance.gameObject.SetActive(false);
    }

    public static void Die()
    {
        Pacman.RemainingLife--;
        Instance.gameObject.SetActive(false);
    }

    public static void RestoreLives()
    {
        RemainingLife = MaxLife;
        RemainingLives.UpdateLivesBoard(MaxLife);
    }
}
