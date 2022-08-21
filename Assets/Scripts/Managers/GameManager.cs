using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameData gameData;

    private void Awake()
    {

    }

    void Start()
    {
        // TEST configuration
        gameData.scenario = 10;
        Singleton.Instance.scenarioMngr.sc10.StartScenario(); // FOR TESTING
    }

    
    void Update()
    {
        
    }

}
