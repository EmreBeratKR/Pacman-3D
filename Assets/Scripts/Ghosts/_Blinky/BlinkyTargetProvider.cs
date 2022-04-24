using UnityEngine;

public class BlinkyTargetProvider : GhostTargetProvider
{
    protected override Vector3 ChaseModeDestination => Pacman.Position;

    protected override Vector3 ScatterModeDestination => GameArea.TopRightCorner;


    protected override bool OnDrawGizmos()
    {
        if (!base.OnDrawGizmos()) return false;

        Gizmos.color = this.color;
        Gizmos.DrawLine(GhostContainer.BlinkyGhost.Position + Vector3.up, ChaseModeDestination + Vector3.up);

        return true;
    }
}
