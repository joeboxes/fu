using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // [SerializeField, SerializeReference]
    public GameObject scopeObject;
    
    private Weapon weapon;
    
    // Start is called before the first frame update
    void Start()
    {
        weapon = new Weapon();
        weapon.type = Weapon.Type.Glock;
        weapon.currentCapacity = 25;
        weapon.totalCapacity = 100;
        weapon.damage = 10.5f;
        weapon.speed = 10.0f;
        weapon.shootCount = 1;
        
        Debug.Log("START - WeaponController: "+gameObject);
        Debug.Log("scopeObject: "+scopeObject);
        // hide scope initially
        scopeObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
