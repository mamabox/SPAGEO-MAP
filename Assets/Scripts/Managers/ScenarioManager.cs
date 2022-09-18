using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    //Static variables that determine what functionalities are enabled for a given scenario
    public bool drawingAllowed;

    // Scenario managers
    public Sc10Manager sc10;
    public Sc11Manager sc11;
    public Sc12Manager sc12;

    private void Awake()
    {
        // Scenario Managers
        sc10 = GetComponentInChildren<Sc10Manager>();
        sc11 = GetComponentInChildren<Sc11Manager>();
        sc12 = GetComponentInChildren<Sc12Manager>();
        //sc13 = GetComponentInChildren<Sc13Manager>();

        // Initialise Variables
        drawingAllowed = false;
    }

    public void SwitchToScenario(int scenario)
    {
        if (GameManager.gameData.scenario == 10)
            Singleton.Instance.scenarioMngr.sc10.EndScenario();
        else if (GameManager.gameData.scenario == 11)
            Singleton.Instance.scenarioMngr.sc11.EndScenario();
        else if (GameManager.gameData.scenario == 12)
            Singleton.Instance.scenarioMngr.sc12.EndScenario();

        GameManager.gameData.scenario = scenario;

        switch (GameManager.gameData.scenario)
        {
            case 10:
                Singleton.Instance.scenarioMngr.sc10.StartScenario();
                break;
            case 11:
                Singleton.Instance.scenarioMngr.sc11.StartScenario();
                break;

            case 12:
                Singleton.Instance.scenarioMngr.sc12.StartScenario();
                break;
        }
        Debug.LogFormat("StartScenario({0})", scenario);
    }


    void Start()
    {
        
    }


    void Update()
    {
        
    }


}
