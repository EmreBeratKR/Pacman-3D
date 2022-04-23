using UnityEngine;

public class Pacman : Scenegleton<Pacman>
{
    private Facing facing;
    public static Facing Facing
    {
        get => Instance.facing;
        set => Instance.facing = value;
    }

    public static Transform Transform => Instance.transform;
    

}
