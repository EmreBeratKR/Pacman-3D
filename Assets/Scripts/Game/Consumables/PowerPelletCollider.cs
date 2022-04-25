using UnityEngine;

public class PowerPelletCollider : CustomCollider<BoxCollider>, IConsumable
{
    private static int Value = 50;

    [SerializeField] private GameObject main;


    public void Consume()
    {
        GameController.EnterScatterMode();
        EventSystem.ConsumableConsumed(Value);
        Destroy(main);
    }
}
