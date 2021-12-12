using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispatch
{
    public delegate void Callback(string eventName, object eventData); 
        
    private static Dispatch _dispatch;
    
    public static Dispatch Instance
    {
        get
        {
            if (_dispatch==null)
            {
                _dispatch = new Dispatch();
            }
            return _dispatch;
        }
    }

    private Dictionary<string, ArrayList> _entries = new Dictionary<string, ArrayList>();
    
    // Start is called before the first frame update
    public void AddListener(string eventName, Callback eventHandler)
    {
        ArrayList entry = null;
        if (_entries.ContainsKey(eventName))
        {
            entry = _entries[eventName];
        }
        else
        {
            entry = new ArrayList();
            _entries[eventName] = entry;
        }

        if (entry.Contains(eventHandler))
        {
            Debug.Log("already contains handler - skipping .... ?");
        }
        else
        {
            Debug.Log("ADDING EVENT HANDLER FOR "+eventName);
            entry.Add(eventHandler);
        }
    }

    // Update is called once per frame
    public void RemoveListener(string eventName, Callback eventHandler)
    {
        if (_entries.ContainsKey(eventName))
        {
            ArrayList entry = _entries[eventName];
            int index = entry.IndexOf(eventHandler);
            if (index >= 0)
            {
                entry.RemoveAt(index);
                if (entry.Count == 0)
                {
                    _entries.Remove(eventName);
                }
            }
        }
    }

    public void AlertAll(string eventName, object eventData)
    {
        Debug.Log("AlertAll - "+eventName+" - "+eventData);
        if (_entries.ContainsKey(eventName))
        {
            Debug.Log("CONTAINS "+eventName);
            ArrayList entry = _entries[eventName];
            var list = entry.ToArray();
            for (int i = 0; i < list.Length; ++i)
            {
                Callback cb = (Callback)list[i];
                cb(eventName, eventData);
            }
        }
    }
}
