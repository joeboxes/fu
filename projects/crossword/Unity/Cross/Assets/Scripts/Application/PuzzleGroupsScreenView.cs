using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGroupsScreenView : MonoBehaviour
{
    public static string EVENT_SELECT_PUZZLE = "PuzzleGroupsScreenView.EVENT_SELECT_PUZZLE";
    
    public GameObject _rowPrefab;
    public GameObject _scrollContainer;

    public ScrollRect _scrollView;
    
    // button hookups
    public void OnPuzzleSelect()
    {
        Debug.Log("OnPuzzleSelect");
        Dispatch.Instance.AlertAll(LandingScreenView.EVENT_SELECT_PUZZLES, this);
        // GetComponent<ScrollRect>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        var client = PuzzleRemoteClient.Instance;
        client.RequestPuzzleGroups(0, 10);
        Debug.Log("RICHIE - start groups");
    
        for(int i=0; i<10; ++i)
        {
            var row = GameObject.Instantiate(_rowPrefab);
            row.transform.SetParent(_scrollContainer.transform);
            var script = row.GetComponent<PuzzleGroupRow>();
            script.row = i;
            script.OnRowSelectEvent += RowSelectEventHandler;
        }
        
        //var scrollView = GetComponent<ScrollRect>();
        // scrollView.verticalNormalizedPosition = 0.0f;
        //scrollView.normalizedPosition = new Vector2(0,1);
        _scrollView.normalizedPosition = new Vector2(0,1);
    }

    private void RowSelectEventHandler(PuzzleGroupRow row)
    {
        Debug.Log("ScreenView - RowSelectEventHandler: "+row.row);
        
        
        // for the moment -> go to the puzzle view
        
        
    } 

    // Update is called once per frame
    void Update()
    {
        //var scrollView = GetComponent<ScrollRect>();
        // _scrollView = 
        // scrollView.verticalNormalizedPosition = 0.0f;
        // _scrollView.normalizedPosition = new Vector2(0,1);
    }
}
