using UnityEngine;

public class Pacman : Scenegleton<Pacman>
{
    public const float TouchRange = 1;


    public static Vector3 DesiredPosition
    {
        get
        {
            var result = Position + PacmanMovement.Up * GameArea.DotDistance * 2;
            result.y = 0;
            return result;
        }
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


    public static void Die()
    {
        Instance.gameObject.SetActive(false);
    }
}
