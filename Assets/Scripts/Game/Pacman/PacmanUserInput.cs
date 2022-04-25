using UnityEngine;

public class PacmanUserInput : Scenegleton<PacmanUserInput>
{
    [SerializeField] private KeyCode[] goUp;
    [SerializeField] private KeyCode[] goDown;
    [SerializeField] private KeyCode[] goRight;
    [SerializeField] private KeyCode[] goLeft;


    public static bool IsUp => AnyKey(Instance.goUp);
    public static bool IsDown => AnyKey(Instance.goDown);
    public static bool IsRight => AnyKey(Instance.goRight);
    public static bool IsLeft => AnyKey(Instance.goLeft);


    private static bool AnyKey(KeyCode[] keys)
    {
        foreach (var key in keys)
        {
            if (Input.GetKey(key)) return true;
        }

        return false;
    }
}
