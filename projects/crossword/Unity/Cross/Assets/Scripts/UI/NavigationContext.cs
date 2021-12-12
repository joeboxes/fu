using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class NavigationContext
{
    private ArrayList _contextStack = new ArrayList();
    
    // TODO: animations
    
    public void PushContext(ScreenContext context)
    {
        ScreenContext currentContext = _contextStack.Count > 0 ? (ScreenContext)_contextStack[_contextStack.Count-1] : null;
        
        Debug.Log("PushContext: "+context);

        if (currentContext!=null)
        {
            currentContext.WillHideInContext(this);
        }
        context.WillAddToContext(this);
        _contextStack.Add(context);
        context.DidAddToContext(this);
        if (currentContext!=null)
        {
            currentContext.DidHideInContext(this);
        }
    }
    
    public void PopContext([CanBeNull] ScreenContext context)
    {
        Debug.Log("PopContext: "+context);
        var index = _contextStack.Count - 1;
        var last = _contextStack[index];
        if (context != null)
        {
            if (last != context)
            {
                Debug.Log("not equal, don't pop");
                return;
            }
        }
        
        ScreenContext prevContext = _contextStack.Count > 1 ? (ScreenContext)_contextStack[_contextStack.Count-2] : null;

        if (prevContext != null)
        {
            prevContext.WillShowInContext(this);
        }
        
        context.WillRemoveFromContext(this);
        _contextStack.RemoveAt(index);
        context.DidRemoveFromContext(this);
        
        if (prevContext != null)
        {
            prevContext.DidShowInContext(this);
        }
    }
}
