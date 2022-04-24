using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PacDotCollider : CustomCollider<BoxCollider>, IConsumable
{
    [SerializeField] private GameObject main;


    public void Consume()
    {
        Destroy(main);
    }
}
