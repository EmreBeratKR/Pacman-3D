using UnityEngine;

public class PinkyTargetProvider : GhostTargetProvider
{
    protected override Vector3 ChaseModeDestination => Pacman.DesiredPosition;

    protected override Vector3 ScatterModeDestination => GameArea.TopLeftCorner;


    protected override bool OnDrawGizmos()
    {
        if (!base.OnDrawGizmos()) return false;

        Gizmos.color = this.color;
        Gizmos.DrawLine(GhostContainer.PinkyGhost.Position + Vector3.up, ChaseModeDestination + Vector3.up);

        return true;
    }
}
