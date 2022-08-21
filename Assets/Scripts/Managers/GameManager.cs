using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int activeScenario;
    public static int activeRoute;

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
        activeScenario = 10;
        scenarioManager.sc10.StartScenario(); // FOR TESTING
    }

    
    void Update()
    {
        
    }

}
