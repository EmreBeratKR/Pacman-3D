using UnityEngine;

public class PowerPelletCollider : CustomCollider<BoxCollider>, IConsumable
{
    private static int Value = 50;

    [SerializeField] private GameObject main;


    public void Consume()
    {
        GameController.EnterScatterMode();
        main.transform.parent = null;
        EventSystem.ConsumableConsumed(Value);
        Destroy(main);
    }
}
