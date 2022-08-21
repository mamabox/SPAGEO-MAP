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

        // Initialise Variables
        drawingAllowed = false;
    }

    public void SwitchToScenario(int scenario)
    {
        if (GameManager.activeScenario == 10)
            Singleton.Instance.scenarioMngr.sc10.EndScenario();
        else if (GameManager.activeScenario == 11)
            Singleton.Instance.scenarioMngr.sc11.EndScenario();
        else if (GameManager.activeScenario == 12)
            Singleton.Instance.scenarioMngr.sc12.EndScenario();

        GameManager.activeScenario = scenario;

        switch (GameManager.activeScenario)
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
