using UnityEngine;

public class PowerPelletCollider : CustomCollider, IConsumable
{
    [SerializeField] private GameObject main;


    public void Consume()
    {
        Destroy(main);
    }
}
