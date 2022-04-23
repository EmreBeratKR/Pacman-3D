using UnityEngine;

public class Pacman : Scenegleton<Pacman>
{
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
