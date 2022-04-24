using UnityEngine;

public class GameArea : Scenegleton<GameArea>
{
    public const float DotDistance = 3;

    [SerializeField] private Transform topRightCorner;
    [SerializeField] private Transform topLeftCorner;
    [SerializeField] private Transform bottomRightCorner;
    [SerializeField] private Transform bottomLeftCorner;


    public static Vector3 TopRightCorner
    {
        get
        {
            var result = Instance.topRightCorner.position;
            result.y = 0;
            return result;
        }
    }

    public static Vector3 TopLeftCorner
    {
        get
        {
            var result = Instance.topLeftCorner.position;
            result.y = 0;
            return result;
        }
    }

    public static Vector3 BottomRightCorner
    {
        get
        {
            var result = Instance.bottomRightCorner.position;
            result.y = 0;
            return result;
        }
    }

    public static Vector3 BottomLeftCorner
    {
        get
        {
            var result = Instance.bottomLeftCorner.position;
            result.y = 0;
            return result;
        }
    }
}
