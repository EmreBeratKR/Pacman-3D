using UnityEngine;

public class PacmanUserInput : Scenegleton<PacmanUserInput>
{
    [SerializeField] private KeyCode goUp;
    [SerializeField] private KeyCode goDown;
    [SerializeField] private KeyCode goRight;
    [SerializeField] private KeyCode goLeft;


    public static bool IsUp => Input.GetKey(Instance.goUp);
    public static bool IsDown => Input.GetKey(Instance.goDown);
    public static bool IsRight => Input.GetKey(Instance.goRight);
    public static bool IsLeft => Input.GetKey(Instance.goLeft);
}
