using System;
using UnityEngine;

public class PacmanCollisionChecker : Scenegleton<PacmanCollisionChecker>
{
    [SerializeField] private Vector3 colliderSize;
    [SerializeField] private LayerMask targetLayer;



    private void Update()
    {
        Check();
    }

    private void Check()
    {
        var overlaps = Physics.OverlapBox(Pacman.Transform.position, colliderSize * 0.5f, Quaternion.identity, targetLayer);

        foreach (var overlap in overlaps)
        {
            if (overlap.TryGetComponent(out IConsumable consumable))
            {
                consumable.Consume();
            }

            if (overlap.TryGetComponent(out GhostCollider ghost))
            {
                switch (GameController.GameMode)
                {
                    case GameMode.Chase:
                        Pacman.Die();
                        break;

                    case GameMode.Scatter:
                        ghost.Kill();
                        break;
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        if (!debugSettings.debug) return;

        Gizmos.color = debugSettings.color;
        Gizmos.DrawWireCube(Pacman.Transform.position, colliderSize);
    }



    [SerializeField] private DebugSettings debugSettings;

    [Serializable]
    internal struct DebugSettings
    {
        public Color color;
        public bool debug;
    }
}
