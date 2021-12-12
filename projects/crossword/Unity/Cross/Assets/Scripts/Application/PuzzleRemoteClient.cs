using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PuzzleRemoteClient
{
    public delegate void Callback(object context, object result);

    private static PuzzleRemoteClient _instance;
    public static PuzzleRemoteClient Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PuzzleRemoteClient();
            }
            return _instance;
        }
    }
    
    public PuzzleRemoteClient()
    {
        Debug.Log("PuzzleRemoteClient");
    }

    private void RequestDataFromURL(string url)
    {
        //
        Debug.Log("RequestDataFromURL: "+url);
    }
    
    private void RequestDataFromPath(string filename)
    {
        //
        Debug.Log("RequestDataFromPath: "+filename);
        // string path = Path.Combine(UnityEngine.Application.dataPath, filename);
        // string path = Path.Combine(UnityEngine.Application.persistentDataPath, filename);
        string path = Path.Combine(UnityEngine.Application.streamingAssetsPath, filename);
        
        Debug.Log("path: "+path);
        var source = new StreamReader(path);
        Debug.Log("source: "+source);
        var data = source.ReadToEnd(); // async ?
        Debug.Log("data: "+data);

        PuzzleGroupResponse response = JsonUtility.FromJson<PuzzleGroupResponse>(data);

        Debug.Log("RESPONSE: "+response);
        Debug.Log("RESULT: "+response.result);
        Debug.Log("DATA: "+response.data);
        Debug.Log("NULL?: "+(response.data==null));
        Debug.Log("OFFSET: "+response.data.offset);
        Debug.Log("COUNT: "+response.data.count);
        Debug.Log("GROUPS: "+response.data.groups);
        Debug.Log("GROUP COUNT: "+response.data.groups.Length);
        
    }
    
    // interface ....
    
    public void RequestPuzzleGroups(int offset, int count)
    {
        //
        Debug.Log("RequestPuzzleGroups: "+offset+" : "+count);
        RequestDataFromPath("puzzleGroupData.txt");
    }
    
    public void RequestPuzzleList(int groupID, int offset, int count)
    {
        //
        Debug.Log("RequestPuzzleList: "+groupID+" - "+offset+" : "+count);
    }
    
    public void RequestPuzzleDetails(int puzzleID, int offset, int count)
    {
        //
        Debug.Log("RequestPuzzleDetails: "+puzzleID+" - "+offset+" : "+count);
    }


    [System.Serializable]
    public class PuzzleGroupResponse
    {
        public string result;
        public PuzzleGroupResponseData data;
        /*
        public static PuzzleGroupResponse CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<PuzzleGroupResponse>(jsonString);
        }
        */
    }
    
    [System.Serializable]
    public class PuzzleGroupResponseData
    {
        public int offset;
        public int count;
        /*
        public static PuzzleGroupResponseData CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<PuzzleGroupResponseData>(jsonString);
        }
        */
         public PuzzleGroups[] groups;
    }
    
    [System.Serializable]
    public class PuzzleGroups
    {
        public string id;
        public string title;
        public string description;
        public string iconURL;
        public int count;
    }
    
    // myObject = JsonUtility.FromJson<MyClass>(json);
}
