using UnityEngine;

public class GhostContainer : Scenegleton<GhostContainer>
{
    public static Ghost[] All => new Ghost[] { Instance.blinkyGhost, Instance.inkyGhost, Instance.pinkyGhost, Instance.clydeGhost };

    [SerializeField] private BlinkyGhost blinkyGhost;
    public static BlinkyGhost BlinkyGhost => Instance.blinkyGhost;

    [SerializeField] private InkyGhost inkyGhost;
    public static InkyGhost InkyGhost => Instance.inkyGhost;

    [SerializeField] private PinkyGhost pinkyGhost;
    public static PinkyGhost PinkyGhost => Instance.pinkyGhost;

    [SerializeField] private ClydeGhost clydeGhost;
    public static ClydeGhost ClydeGhost => Instance.clydeGhost;
}
