using UnityEngine;

[RequireComponent(typeof(Ghost))]
public abstract class GhostTargetProvider : MonoBehaviour
{
    private Ghost ghost;

    protected abstract Vector3 ChaseModeDestination { get; }
    protected abstract Vector3 ScatterModeDestination { get; }

    public Vector3? Destination
    {
        get
        {
            if (!GameController.GameStarted) return null;

            switch (ghost.Mode)
            {
                default : return ChaseModeDestination;

                case GameMode.Scatter : return ScatterModeDestination;
            }
        }
    }


    protected virtual void Awake()
    {
        ghost = this.GetComponent<Ghost>();
    }



    public Color color;
    public bool debug;
    protected virtual bool OnDrawGizmos()
    {
        if (!Application.isPlaying) return false;

        return debug;
    }
}
