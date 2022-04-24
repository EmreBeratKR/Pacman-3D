using UnityEngine;

public class BlinkyTargetProvider : GhostTargetProvider
{
    protected override Vector3 ChaseModeDestination
    {
        get
        {
            var result = Pacman.Transform.position;
            result.y = 0;
            return result;
        }
    }

    protected override Vector3 ScatterModeDestination => GameArea.TopRightCorner;


    protected override bool OnDrawGizmos()
    {
        if (!base.OnDrawGizmos()) return false;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(GhostContainer.BlinkyGhost.Position + Vector3.up, Pacman.Transform.position);

        return true;
    }
}
