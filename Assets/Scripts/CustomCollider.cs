using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class CustomCollider : MonoBehaviour
{
    private new Collider collider;
    public Collider Collider => this.collider;


    private void Awake()
    {
        collider = this.GetComponent<Collider>();
    }
}
