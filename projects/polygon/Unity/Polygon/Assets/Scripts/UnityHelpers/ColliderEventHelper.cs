using UnityEngine;
using UnityEngine.Events;

public class ColliderEventHelper : MonoBehaviour
{
    // [SerializeField]
    // [SerializeReference]
    // public Action<Collider, ColliderEventHelper> TriggerEnterHandler;
    [SerializeField]
    public UnityEvent<Collider,Collider> TriggerEnterHandler;
    
    [SerializeField]
    public UnityEvent<Collision,Collider> CollisionEnterHandler;
    
    private Collider collider;

    private void OnAwake()
    {
        collider = GetComponent<Collider>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        TriggerEnterHandler?.Invoke(other, collider);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        CollisionEnterHandler?.Invoke(collision, collider);
    }

}
