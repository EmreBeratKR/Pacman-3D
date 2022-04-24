using UnityEngine;

public abstract class Ghost : MonoBehaviour
{
    public abstract GhostType Type { get; }

    public Vector3 Position
    {
        get
        {
            var result = transform.position;
            result.y = 0;
            return result;
        }
    }
}

public enum GhostType { Blinky, Inky, Pinky, Clyde }
