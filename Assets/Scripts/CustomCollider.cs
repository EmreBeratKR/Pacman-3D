using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class CustomCollider<T> : MonoBehaviour where T : Collider
{
    private new T collider;
    public T Collider => this.collider;


    protected virtual void Awake()
    {
        collider = this.GetComponent<T>();
    }
}
