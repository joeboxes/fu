using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

// using UnityEngine;

public class ScreenContext
{
    [CanBeNull] private NavigationContext _navigationContext;
    
    virtual public void WillAddToContext(NavigationContext context)
    {
        _navigationContext = null;
        Debug.Log("adding");
    }
    
    virtual public void DidAddToContext(NavigationContext context)
    {
        // Debug.Log("adding");
    }
    
    virtual public void WillRemoveFromContext(NavigationContext context)
    {
        Debug.Log("removing");
    }
    
    virtual public void DidRemoveFromContext(NavigationContext context)
    {
        _navigationContext = null;
    }
    
    // 'under' next 
    virtual public void WillHideInContext(NavigationContext context)
    {
        Debug.Log("will hide");
    }
    
    virtual public void DidHideInContext(NavigationContext context)
    {
        Debug.Log("did hide");
    }
    
    // from 'under'
    
    virtual public void WillShowInContext(NavigationContext context)
    {
        Debug.Log("will show");
    }
    
    virtual public void DidShowInContext(NavigationContext context)
    {
        Debug.Log("did show");
    }
}
