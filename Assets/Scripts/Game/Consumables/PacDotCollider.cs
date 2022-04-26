using UnityEngine;

public class PacDotCollider : CustomCollider<BoxCollider>, IConsumable
{
    private static int Value = 10;

    [SerializeField] private GameObject main;


    public void Consume()
    {
        main.transform.parent = null;
        AudioManager.PlayEatDot();
        EventSystem.ConsumableConsumed(Value);
        Destroy(main);
    }
}
