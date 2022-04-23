using System;
using System.Collections.Generic;
using UnityEngine;

public class PacmanRaycaster : Scenegleton<PacmanRaycaster>
{
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Vector3 boxCastForwardSize;
    [SerializeField] private Vector3 boxCastSideSize;
    [SerializeField] private float forwardDistance;
    [SerializeField] private float sideDistance;
    [SerializeField] private RaycastDebugSettings debugSettings;

    

    public static bool IsBlocked(Facing facing)
    {
        Vector3 direction;
        RaycastType type;

        switch (facing)
        {
            default :
                direction = PacmanMovement.Up;
                type = RaycastType.Forward;
                break;

            case Facing.Down:
                direction = -PacmanMovement.Up;
                type = RaycastType.Forward;
                break;

            case Facing.Right:
                direction = PacmanMovement.Right;
                type = RaycastType.Side;
                break;

            case Facing.Left:
                direction = -PacmanMovement.Right;
                type = RaycastType.Side;
                break;
        }

        return Instance.IsBlocked(direction, type);
    }


    private bool IsBlocked(Vector3 direction, RaycastType type)
    {
        var size = type == RaycastType.Forward ? boxCastForwardSize : boxCastSideSize;
        var maxDistance = type == RaycastType.Forward ? forwardDistance : sideDistance;
        var extraAngle = type == RaycastType.Forward ? 0 : 90;
        var rotation = Quaternion.Euler(new Vector3(0, 0, PacmanMovement.DesiredAngle + extraAngle));
        return Physics.BoxCast(Pacman.Transform.position, size * 0.5f, direction, rotation, maxDistance, targetLayer);
    }

    private bool IsBlocked(Vector3 direction, RaycastType type, out RaycastHit hitInfo)
    {
        var size = type == RaycastType.Forward ? boxCastForwardSize : boxCastSideSize;
        var maxDistance = type == RaycastType.Forward ? forwardDistance : sideDistance;
        var extraAngle = type == RaycastType.Forward ? 0 : 90;
        var rotation = Quaternion.Euler(new Vector3(0, 0, PacmanMovement.DesiredAngle + extraAngle));
        return Physics.BoxCast(Pacman.Transform.position, size * 0.5f, direction, out hitInfo, rotation, maxDistance, targetLayer);
    }

    


    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;



        void DrawResult(Vector3 direction, RaycastType type)
        {
            var size = type == RaycastType.Forward ? boxCastForwardSize : boxCastSideSize;
            var maxDistance = type == RaycastType.Forward ? forwardDistance : sideDistance;
            var extraAngle = type == RaycastType.Forward ? 0 : 90;

            if (IsBlocked(direction, type, out RaycastHit hitInfo))
            {
                Gizmos.color = debugSettings.blockedColor;
                var rotation = Quaternion.Euler(new Vector3(0, 0, PacmanMovement.DesiredAngle + extraAngle));
                Gizmos.DrawWireMesh(debugSettings.mesh, Pacman.Transform.position + direction * hitInfo.distance, rotation, size);
            }
            else
            {
                Gizmos.color = debugSettings.notBlockedColor;
                var rotation = Quaternion.Euler(new Vector3(0, 0, PacmanMovement.DesiredAngle + extraAngle));
                Gizmos.DrawWireMesh(debugSettings.mesh, Pacman.Transform.position + direction * maxDistance, rotation, size);
            }
        }



        if (debugSettings.showInitial)
        {
            Gizmos.color = debugSettings.initialColor;
            var rotation = Quaternion.Euler(new Vector3(0, 0, PacmanMovement.DesiredAngle));
            Gizmos.DrawWireMesh(debugSettings.mesh, Pacman.Transform.position, rotation, boxCastForwardSize);
        }

        if (debugSettings.showUpResult)
        {
            DrawResult(PacmanMovement.Up, RaycastType.Forward);
        }

        if (debugSettings.showDownResult)
        {
            DrawResult(-PacmanMovement.Up, RaycastType.Forward);
        }

        if (debugSettings.showRightResult)
        {
            DrawResult(PacmanMovement.Right, RaycastType.Side);
        }

        if (debugSettings.showLeftResult)
        {
            DrawResult(-PacmanMovement.Right, RaycastType.Side);
        }
    }
}

internal enum RaycastType { Forward, Side }

[Serializable]
internal struct RaycastDebugSettings
{
    public Mesh mesh;
    public bool showInitial;
    public Color initialColor;
    public bool showUpResult;
    public bool showDownResult;
    public bool showRightResult;
    public bool showLeftResult;
    public Color blockedColor;
    public Color notBlockedColor;
}