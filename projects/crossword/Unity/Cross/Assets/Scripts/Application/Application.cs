using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Application : MonoBehaviour
{
    public GameObject landingScreenPrefab;
    public GameObject puzzleGroupsScreenPrefab;
    public GameObject puzzleListScreenPrefab;
    public GameObject userScreenPrefab;
    public GameObject settingsScreenPrefab;
    
    private NavigationContext _navigation = new NavigationContext();
    /*
     - screen navigator
     
     
     
     
     
     
     
     
     
     */
    // Start is called before the first frame update
    void Start()
    {
        var screen = Instantiate(landingScreenPrefab);
        screen.SetActive(true);
        var landingScreen = new LandingScreenContext(screen);
        
        _navigation.PushContext(landingScreen);
        
        
        
        Dispatch.Instance.AddListener(LandingScreenView.EVENT_SELECT_PUZZLES, LandingScreenSelectPuzzles);
    }
    
    private void LandingScreenSelectPuzzles(string eventName, object eventData)
    {
        Debug.Log("LandingScreenSelectPuzzles -> ...");

        var screen = Instantiate(puzzleGroupsScreenPrefab);
        screen.SetActive(true);
        var groupsScreen = new PuzzleGroupsScreenContext(screen);
        
        _navigation.PushContext(groupsScreen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
