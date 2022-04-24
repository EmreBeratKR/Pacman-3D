using UnityEngine;

[RequireComponent(typeof(GhostPathFinding))]
public abstract class Ghost : MonoBehaviour
{
    public abstract GhostType Type { get; }

    private GhostPathFinding pathFinder;
    public GhostPathFinding PathFinder => this.pathFinder;

    public Vector3 Position
    {
        get
        {
            var result = transform.position;
            result.y = 0;
            return result;
        }
    }


    protected virtual void Awake()
    {
        pathFinder = this.GetComponent<GhostPathFinding>();
    }
}

public enum GhostType { Blinky, Inky, Pinky, Clyde, Dead }
