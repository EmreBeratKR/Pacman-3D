using UnityEngine;

public class PowerPelletCollider : CustomCollider<BoxCollider>, IConsumable
{
    [SerializeField] private GameObject main;


    public void Consume()
    {
        GameController.EnterScatterMode();
        Destroy(main);
    }
}
