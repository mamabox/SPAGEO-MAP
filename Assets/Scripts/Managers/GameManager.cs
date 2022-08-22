using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameData gameData;

    public static bool started;
    public static bool ended;
    public static bool isPaused;

    

    private void Awake()
    {
        //Initialise variables
        started = false;
        ended = false;
        started = false;
    }

    void Start()
    {
        // TEST configuration
        gameData = new GameData(10, 0); //This should happen from the menu
        started = true;
        Singleton.Instance.scenarioMngr.sc10.StartScenario(); // FOR TESTING
    }

    
    void Update()
    {
        
    }

}
