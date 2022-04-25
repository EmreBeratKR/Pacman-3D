using UnityEngine;

public class PacmanCollider : CustomCollider<BoxCollider>, ITeleportable
{
    public void Teleport(Teleporter from, Teleporter to)
    {
        var pacmanPositionY = Pacman.Transform.position.y;
        var destination = to.Position;

        Pacman.Transform.position = new Vector3(destination.x, pacmanPositionY, destination.z);
        Pacman.Facing = to.FacingOverride;
    }
}
