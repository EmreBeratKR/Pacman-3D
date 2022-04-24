using UnityEngine;

public class InkyTargetProvider : GhostTargetProvider
{
    protected override Vector3 ChaseModeDestination
    {
        get
        {
            var pacmanPos = Pacman.Transform.position;
            pacmanPos.y = 0;
            var blinkyPos = GhostContainer.BlinkyGhost.Position;
            var blinkyDistance = pacmanPos - blinkyPos;
            return blinkyPos + blinkyDistance * 2;
        }
    }

    protected override Vector3 ScatterModeDestination => GameArea.BottomRightCorner;


    protected override bool OnDrawGizmos()
    {
        if (!base.OnDrawGizmos()) return false;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(GhostContainer.BlinkyGhost.Position + Vector3.up, ChaseModeDestination + Vector3.up);

        return true;
    }
}
