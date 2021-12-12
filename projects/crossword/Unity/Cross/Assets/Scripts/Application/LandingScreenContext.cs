using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class LandingScreenContext : ScreenContext
{
    private GameObject _gameObject;
    public LandingScreenContext(GameObject gameObject)
    {
        _gameObject = gameObject;
        Debug.Log("START WITH GO:  "+_gameObject);
    }
    
    override public void DidAddToContext(NavigationContext context)
    {
        base.DidAddToContext(context);
        Debug.Log("added specific LandingScreenContext");
        
        
        // Debug.Log(" > "+Screen.currentResolution);
        // Debug.Log(" > "+Screen.width+" x "+Screen.height);
        
    }
}
