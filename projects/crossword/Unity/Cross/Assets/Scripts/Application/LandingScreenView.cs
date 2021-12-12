using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingScreenView : MonoBehaviour
{
    public static string EVENT_SELECT_PUZZLES = "LandingScreenView.EVENT_SELECT_PUZZLES";
    // button hookups
    public void OnButtonPuzzleClick()
    {
        Debug.Log("OnButtonPuzzleClick");
        Dispatch.Instance.AlertAll(LandingScreenView.EVENT_SELECT_PUZZLES, this);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
