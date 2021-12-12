using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGroupsScreenView : MonoBehaviour
{
    public static string EVENT_SELECT_PUZZLE = "PuzzleGroupsScreenView.EVENT_SELECT_PUZZLE";
    
    // button hookups
    public void OnPuzzleSelect()
    {
        Debug.Log("OnPuzzleSelect");
        Dispatch.Instance.AlertAll(LandingScreenView.EVENT_SELECT_PUZZLES, this);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        var client = PuzzleRemoteClient.Instance;
        client.RequestPuzzleGroups(0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
