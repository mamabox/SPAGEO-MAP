using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int activeScenario;
    public int activeRoute;

    private ScenarioManager scenarioManager;
    private UIManager uiManager;
    private CoordinatesManager coordManager;

    private void Awake()
    {
        scenarioManager = GameObject.FindGameObjectWithTag("ScenarioManager").GetComponent<ScenarioManager>();
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        coordManager = GameObject.FindGameObjectWithTag("CoordinatesManager").GetComponent<CoordinatesManager>();

    }

    void Start()
    {
        activeScenario = 12;
        scenarioManager.sc12.StartScenario(); // FOR TESTING
    }

    
    void Update()
    {
        
    }

}
