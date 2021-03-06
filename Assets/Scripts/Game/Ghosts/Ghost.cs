using UnityEngine;

[RequireComponent(typeof(GhostPathFinding))]
public abstract class Ghost : MonoBehaviour
{
    public abstract GhostType Type { get; }

    private GhostPathFinding pathFinder;
    public GhostPathFinding PathFinder => this.pathFinder;

    private GameMode mode;
    public GameMode Mode => this.mode;

    public Facing Facing => pathFinder.Facing;

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

    public void EnterChaseMode()
    {
        this.mode = GameMode.Chase;
    }

    public void EnterScatterMode()
    {
        this.mode = GameMode.Scatter;
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}

public enum GhostType { Blinky, Inky, Pinky, Clyde, Dead }
