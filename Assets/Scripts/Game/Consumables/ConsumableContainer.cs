using UnityEngine;

public class ConsumableContainer : Singleton<ConsumableContainer>
{
    public static void Destroy()
    {
        Destroy(Instance.gameObject);
    }
}
