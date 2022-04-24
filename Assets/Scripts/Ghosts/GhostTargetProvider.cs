using UnityEngine;

public abstract class GhostTargetProvider : MonoBehaviour
{
    protected abstract Vector3 ChaseModeDestination { get; }
    protected abstract Vector3 ScatterModeDestination { get; }

    public Vector3? Destination
    {
        get
        {
            if (!GameController.GameStarted) return null;

            switch (GameController.GameMode)
            {
                default : return ChaseModeDestination;

                case GameMode.Scatter : return ScatterModeDestination;
            }
        }
    }


    public Color color;
    public bool debug;
    protected virtual bool OnDrawGizmos()
    {
        if (!Application.isPlaying) return false;

        return debug;
    }
}
