using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PowerPellet : MonoBehaviour, IConsumable
{
    [SerializeField] private GameObject main;


    public void Consume()
    {
        Destroy(main);
    }
}
