using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class PuzzleGroupsScreenContext : ScreenContext
{
    private GameObject _gameObject;
    public PuzzleGroupsScreenContext(GameObject gameObject)
    {
        _gameObject = gameObject;
        Debug.Log("START WITH GO:  "+_gameObject);
    }
    
    override public void DidAddToContext(NavigationContext context)
    {
        base.DidAddToContext(context);
        Debug.Log("added specific PuzzleGroupsScreenContext");
        
        
        // Debug.Log(" > "+Screen.currentResolution);
        // Debug.Log(" > "+Screen.width+" x "+Screen.height);
        
    }
}
