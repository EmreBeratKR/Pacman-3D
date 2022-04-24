using UnityEngine;

public class ClydeTargetProvider : GhostTargetProvider
{
    public const float PacmanRange = GameArea.DotDistance * 4;
    private static bool pacmanTouched = false;

    protected override Vector3 ChaseModeDestination
    {
        get
        {
            if (PacmanInRange)
            {
                if (pacmanTouched) return ScatterModeDestination;

                if (PacmanTouched)
                {
                    pacmanTouched = true;
                    return ScatterModeDestination;
                }
            }

            return Pacman.Position;
        }
    }

    protected override Vector3 ScatterModeDestination => GameArea.BottomLeftCorner;


    public static bool PacmanInRange
    {
        get
        {
            var sqrRange = PacmanRange * PacmanRange;
            var sqrDistance = (Pacman.Position - GhostContainer.ClydeGhost.Position).sqrMagnitude;

            if (sqrDistance <= sqrRange) return true;

            pacmanTouched = false;
            return false;
        }
    }

    public static bool PacmanTouched
    {
        get
        {
            var sqrRange = Pacman.TouchRange * Pacman.TouchRange;
            var sqrDistance = (Pacman.Position - GhostContainer.ClydeGhost.Position).sqrMagnitude;

            return sqrDistance <= sqrRange;
        }
    }

    protected override bool OnDrawGizmos()
    {
        if (!base.OnDrawGizmos()) return false;

        Gizmos.color = this.color;
        Gizmos.DrawLine(GhostContainer.ClydeGhost.Position + Vector3.up, ChaseModeDestination + Vector3.up);

        Gizmos.color = PacmanInRange ? Color.white : this.color;
        Gizmos.DrawWireSphere(Pacman.Position + Vector3.up, PacmanRange);

        return true;
    }
}
