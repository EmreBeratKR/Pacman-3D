using UnityEngine;

public class DeadGhost : Ghost
{
    private GhostCollider representedGhost;

    public override GhostType Type => GhostType.Dead;


    private void Update()
    {
        if (this.PathFinder.IsArrived)
        {
            representedGhost.Respawn();
            Destroy(this.gameObject);
        }
    }

    public void SetRepresentedGhost(GhostCollider deadGhost)
    {
        representedGhost = deadGhost;
    }
}
