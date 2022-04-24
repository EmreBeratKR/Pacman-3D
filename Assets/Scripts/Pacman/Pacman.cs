using UnityEngine;

public class Pacman : Scenegleton<Pacman>
{
    [SerializeField] private Transform desiredPosition;
    public static Vector3 DesiredPosition
    {
        get
        {
            var result = Instance.desiredPosition.position;
            result.y = 0;
            return result;
        }
    }

    private Facing facing;
    public static Facing Facing
    {
        get => Instance.facing;
        set => Instance.facing = value;
    }

    private PacmanState state;
    public static PacmanState State
    {
        get => Instance.state;
        set => Instance.state = value;
    }

    public static Transform Transform => Instance.transform;
    

}
