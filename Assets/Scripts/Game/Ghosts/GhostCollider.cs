using UnityEngine;

public class GhostCollider : CustomCollider<BoxCollider>, IKillable, IRespawnable, ITeleportable
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

    private void OnEnable()
    {
        EventSystem.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        EventSystem.OnGameOver -= OnGameOver;
    }

    private void OnGameOver(int score, int lifeLeft)
    {
        main.SetActive(false);
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
        ghost.PathFinder.Warp(respawnPoint);
        main.SetActive(true);
    }

    public void Teleport(Teleporter from, Teleporter to)
    {
        var positionY = main.transform.position.y;
        var destination = to.Position;

        ghost.PathFinder.Warp(new Vector3(destination.x, positionY, destination.z));
    }
}
