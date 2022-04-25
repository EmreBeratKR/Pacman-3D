using UnityEngine;

public class DeadGhostSpawner : Scenegleton<DeadGhostSpawner>
{
    [SerializeField] private DeadGhost prefab;

    
    public static void Spawn(GhostCollider deadGhost, Vector3 position)
    {
        var ghost = Instantiate(Instance.prefab, position, Quaternion.identity, Instance.transform);
        ghost.SetRepresentedGhost(deadGhost);
    }
}
