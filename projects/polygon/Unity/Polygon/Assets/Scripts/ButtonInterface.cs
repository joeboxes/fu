using System;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInterface : MonoBehaviour
{
    [SerializeField] private Collider collider;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material materialIdle;
    [SerializeField] private Material materialActive;
    // [SerializeField] private Collider innerCollider;
    
    void Start()
    {
        Debug.Log("Start");

        // outerCollider.isTrigger = true;
        // outerCollider.
        meshRenderer.material = materialIdle;

    }
    
    private void OnTriggerEnter(Collider other)
    {
        // 
        meshRenderer.material = materialActive;
    }
}
