using UnityEngine;

public class GhostContainer : Scenegleton<GhostContainer>
{
    [SerializeField] private BlinkyGhost blinkyGhost;
    public static BlinkyGhost BlinkyGhost => Instance.blinkyGhost;

    [SerializeField] private InkyGhost inkyGhost;
    public static InkyGhost InkyGhost => Instance.inkyGhost;
}
