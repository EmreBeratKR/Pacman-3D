using UnityEngine;

public class GhostCollider : CustomCollider<BoxCollider>, IKillable, IRespawnable
{
    [SerializeField] private GameObject main;
    public GameObject Main => this.main;


    public GameMode Mode => ghost.Mode;

    private Ghost ghost;


    protected override void Awake()
    {
        base.Awake();

        ghost = main.GetComponent<Ghost>();
    }

    public void Kill()
    {
        ghost.EnterChaseMode();
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
