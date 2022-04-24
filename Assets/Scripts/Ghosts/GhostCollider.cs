using UnityEngine;

public class GhostCollider : CustomCollider<BoxCollider>, IKillable, IRespawnable
{
    [SerializeField] private GameObject main;
    public GameObject Main => this.main;


    public void Kill()
    {
        DeadGhostSpawner.Spawn(this, main.transform.position);
        main.SetActive(false);
    }

    public void Respawn()
    {
        var positionY = main.transform.position.y;
        var respawnPoint = GameArea.Center;
        respawnPoint.y = positionY;
        main.transform.position = respawnPoint;
        main.SetActive(true);
    }
}
