using UnityEngine;

public class DeadTargetProvider : GhostTargetProvider
{
    protected override Vector3 ChaseModeDestination => GameArea.Center;

    protected override Vector3 ScatterModeDestination => GameArea.Center;
}
