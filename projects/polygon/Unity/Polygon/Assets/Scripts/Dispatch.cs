using System;
using System.Collections.Generic;

public class Dispatch
{
    public delegate void Method(System.Object data, System.Object context);

    private class TableEntry
    {
        public Method function;
        public Object context;
    }
    private Dictionary<String, List<TableEntry>> table = new Dictionary<String, List<TableEntry>>();

    public void AlertAll(string eventName, Object data)
    {
        var entries = new List<TableEntry>();
        if(table.ContainsKey(eventName))
        {
            var list = table[eventName];
            foreach (var entry in list)
            {
                entries.Add(entry);
            }
        }
        
        foreach (var entry in entries)
        {
            entry.function(data, entry.context);
        }
    }
    
    public void AddListener(string eventName, Method function, System.Object context)
    {
        if (!table.ContainsKey(eventName))
        {
            table[eventName] = new List<TableEntry>();
        }
        var entry = new TableEntry
        {
            function = function,
            context = context
        };
        table[eventName].Add(entry);
    }
    public void RemoveListener(string eventName, Method function, System.Object context)
    {
        if (table.ContainsKey(eventName))
        {
            var list = table[eventName];
            foreach (var entry in list)
            {
                if (entry.function == function) // check for context?
                {
                    if (list.Count == 1)
                    {
                        list.Clear();
                        table.Remove(eventName);
                    }
                    else
                    {
                        list.Remove(entry);
                        // todo does this need to be removed from foreach loop - mutating
                    }
                    return;
                }
            }
        }
    }
    
    public void Clear()
    {
        var keys = table.Keys;
        foreach (var key in keys)
        {
            var list = table[key];
            list.Clear();
            table.Remove(key);
            // todo does this need to be removed from foreach loop - mutating
        }
    }
}
