using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code;

public class World : MonoBehaviour
{
    private Dispatch dispatch;

    public Transform playerTransform;
    public GameObject lazerBeam;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("WORLD START");
        dispatch = new Dispatch();
        // Debug.Log("add");
        dispatch.AddListener("EVENT_NAME", handleCallback);
        // Debug.Log("dis");
        dispatch.DispatchEvent("EVENT_NAME", "value");
    
        Vector3 positionLeft = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        Vector3 positionRight = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        Quaternion rotationLeft = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        Quaternion rotationRight = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
        
        Debug.Log("LEFT: "+positionLeft+" | "+rotationLeft);
        Debug.Log("RIGHT: "+positionRight+" | "+rotationRight);
        
        
        
        
    }

    public void handleCallback(object data, object obj)
    {
        //Debug.Log("handleCallback");
        //Debug.Log(" "+data);
        //Debug.Log(" "+obj);
        dispatch.RemoveListener("EVENT_NAME", handleCallback);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionLeft = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        Vector3 positionRight = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        Quaternion rotationLeft = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        Quaternion rotationRight = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
        
        // Debug.Log("RICHIE - LEFT: "+positionLeft+" | "+rotationLeft);
        // Debug.Log("RICHIE - RIGHT: "+positionRight+" | "+rotationRight);
        // Debug.Log("RICHIE: "+positionRight.x+" | "+positionRight.y+" | "+positionRight.z);

        if (lazerBeam != null)
        {
            
            // Vector3 destPos = m_parentTransform.TransformPoint(m_anchorOffsetPosition + handPos);
            // Quaternion destRot = m_parentTransform.rotation * handRot * m_anchorOffsetRotation;
            
            // playerTransform
                
            // Transform t = new Transform();
            // Transform.po
            // t.position = ;
            
            // lazerBeam.transform.position = positionRight;
            // lazerBeam.transform.rotation = rotationRight;
            
            //lazerBeam.transform = t;
            
            // append transform to parent
            Vector3 destPos = playerTransform.TransformPoint(positionRight);
            Quaternion destRot = playerTransform.rotation * rotationRight;
            
            lazerBeam.transform.position = destPos;
            lazerBeam.transform.rotation = destRot;

            Vector3 origin = destPos;
            Vector3 direction = new Vector3(0,0,1);
            direction = Quaternion.Inverse(destRot) * direction;
            
            Ray ray = new Ray(origin, direction);
            // Debug.DrawRay();
            // int layerMask = 0;
            RaycastHit hitInfo = new RaycastHit();
            // distance = 1000
            // Physics.Raycast(origin, direction, hitInfo, DistanceJoint2D, layerMask);
            
            
            Debug.DrawRay(origin,direction);
            
            Physics.Raycast(ray, out hitInfo);
            // Physics.Raycast(origin, direction, hitInfo);
            if (hitInfo.rigidbody != null)
            {
                Debug.Log("RICHIE - hitInfo: " + hitInfo + " : " + hitInfo.rigidbody);
                //Physics.Raycast(origin, direction, hitInfo, distance, layerMask);
            }
        }
    }
}
