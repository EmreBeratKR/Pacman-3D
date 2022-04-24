using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PacDotCollider : MonoBehaviour, IConsumable
{
    [SerializeField] private GameObject main;


    public void Consume()
    {
        Destroy(main);
    }
}
