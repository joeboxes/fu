using System;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInterface : MonoBehaviour
{
    [SerializeField] private Collider handCollider; // large outer-hand container
    [SerializeField] private Collider outerCollider; // controller
    [SerializeField] private Collider innerCollider; // front
    
    
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material[] materials;
    private int currentMaterialIndex = 0;

    void Start()
    {
        Debug.Log("Start");

        outerCollider.isTrigger = true;
        // outerCollider.

    }
    
    private void OnTriggerEnter(Collider other)
    {
        doTestColor();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // doTestColor();
    }
    
    private void doTestColor()
    {
        // 
        currentMaterialIndex += 1;
        if (currentMaterialIndex > materials.Length)
        {
            currentMaterialIndex = 0;
        }

        var material = materials[currentMaterialIndex];

        meshRenderer.material = material;
    }
}
