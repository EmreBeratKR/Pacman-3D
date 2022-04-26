using UnityEngine;

public class GhostContainer : Scenegleton<GhostContainer>
{
    [SerializeField] private Transform deadGhosts;


    public static bool HasDeadGhost => Instance.deadGhosts.childCount != 0;

    public static Ghost[] All => new Ghost[] { Instance.blinkyGhost, Instance.inkyGhost, Instance.pinkyGhost, Instance.clydeGhost };

    [SerializeField] private BlinkyGhost blinkyGhost;
    public static BlinkyGhost BlinkyGhost => Instance.blinkyGhost;

    [SerializeField] private InkyGhost inkyGhost;
    public static InkyGhost InkyGhost => Instance.inkyGhost;

    [SerializeField] private PinkyGhost pinkyGhost;
    public static PinkyGhost PinkyGhost => Instance.pinkyGhost;

    [SerializeField] private ClydeGhost clydeGhost;
    public static ClydeGhost ClydeGhost => Instance.clydeGhost;


    public static void HideAll()
    {
        BlinkyGhost.Hide();
        InkyGhost.Hide();
        PinkyGhost.Hide();
        ClydeGhost.Hide();
    }

    public static void ClearDeadGhosts()
    {
        for (int i = 0; i < Instance.deadGhosts.childCount; i++)
        {
            Destroy(Instance.deadGhosts.GetChild(0).gameObject);
        }
    }
}
