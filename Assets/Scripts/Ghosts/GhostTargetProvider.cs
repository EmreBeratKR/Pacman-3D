using UnityEngine;

public abstract class GhostTargetProvider : MonoBehaviour
{
    protected abstract Vector3 ChaseModeDestination { get; }
    protected abstract Vector3 ScatterModeDestination { get; }

    public Vector3 Destination
    {
        get
        {
            switch (GameController.GameMode)
            {
                default : return ChaseModeDestination;

                case GameMode.Scatter : return ScatterModeDestination;
            }
        }
    }


    public bool Debug;
    protected virtual bool OnDrawGizmos()
    {
        if (!Application.isPlaying) return false;

        return Debug;
    }
}
