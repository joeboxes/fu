using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestTriggerButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnChildTriggerEnter(Collider other, Collider source)
    {
        Debug.Log($"OnChildTriggerEnter: {other}, {source}");
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter: {other}");
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"OnCollisionEnter: {collision}");
    }
    
    
    
}



