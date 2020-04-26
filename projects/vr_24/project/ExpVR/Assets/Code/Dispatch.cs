using System;
using System.Collections;
using System.Collections.Generic;

namespace Code
{
    
public class Dispatch
{
    public delegate void CallbackFxn(object data, object parameter);

    private class CallbackEntry
    {
        public CallbackFxn fxn;
        public object ctx;

        public CallbackEntry(CallbackFxn f, object p)
        {
            fxn = f;
            ctx = p;
        }
    }
    
    private Dictionary<String, ArrayList> list = new Dictionary<String, ArrayList>();
    
    public void AddListener(String eventName, CallbackFxn fxn, object parameter = null)
    {
        CallbackEntry entry = new CallbackEntry(fxn, parameter);
        ArrayList entries;
        if (list.ContainsKey(eventName))
        {
            entries = list[eventName];
        }
        else
        {
            entries = new ArrayList();
            list[eventName] = entries;
        }
        entries.Add(entry);
    }
    
    public void RemoveListener(String eventName, CallbackFxn fxn, object parameter = null)
    {
        ArrayList entries = list[eventName];
        if (entries != null)
        {
            //foreach (var entry in entries)
            //{
            for(int i=0; i<entries.Count; ++i)
            {
                object entry = entries[i];
                CallbackEntry callback = (CallbackEntry) entry;
                CallbackFxn fx2 = callback.fxn;
                object ctx = callback.ctx;
                if (fx2 == fxn && ctx == parameter)
                {
                    // entries.Remove(entry);
                    entries.RemoveAt(i);
                    --i;
                    // break; // remove only first single
                }
            }
            // remove empty list
            if (entries.Count == 0)
            {
                list.Remove(eventName);
            }
        }
    }

    public void DispatchEvent(String eventName, object data)
    {
        if (!list.ContainsKey(eventName))
        {
            ArrayList source = list[eventName];
            if (source != null)
            {
                object[] entries = source.ToArray(); // need to iterate over copy
                foreach (var entry in entries)
                {
                    CallbackEntry callback = (CallbackEntry) entry;
                    CallbackFxn fxn = callback.fxn;
                    object ctx = callback.ctx;
                    fxn(data, ctx);
                }
            }
        }
    }
}


}