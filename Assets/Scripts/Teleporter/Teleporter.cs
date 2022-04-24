using System;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Teleporter pair;
    [SerializeField] private Transform teleporterPod;
    [SerializeField] private Transform arrivalPod;
    [SerializeField] private LayerMask targetLayer;


    [SerializeField] private Facing facingOverride;
    public Facing FacingOverride => facingOverride;


    public Vector3 Position
    {
        get
        {
            var result = arrivalPod.position;
            result.y = 0;
            return result;
        }
    }


    private void Update()
    {
        TryTeleport();
    }

    private void TryTeleport()
    {
        var overlaps = Physics.OverlapBox(teleporterPod.position, teleporterPod.localScale * 0.5f, Quaternion.identity, targetLayer);

        foreach (var overlap in overlaps)
        {
            if (overlap.TryGetComponent(out ITeleportable teleportable))
            {
                teleportable.Teleport(this, pair);
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (!debugSettings.debug) return;

        Gizmos.color = debugSettings.teleporterPodColor;
        Gizmos.DrawWireCube(teleporterPod.position, teleporterPod.localScale);

        Gizmos.color = debugSettings.arrivalPodColor;
        Gizmos.DrawWireCube(arrivalPod.position, arrivalPod.localScale);
    }



    [SerializeField] private DebugSettings debugSettings;

    [Serializable]
    internal struct DebugSettings
    {
        public Color teleporterPodColor;
        public Color arrivalPodColor;
        public bool debug;
    }
}
