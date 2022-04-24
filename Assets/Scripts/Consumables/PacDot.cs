using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PacDot : MonoBehaviour, IConsumable
{
    [SerializeField] private GameObject main;


    public void Consume()
    {
        Destroy(main);
    }
}
