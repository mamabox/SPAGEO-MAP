using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int activeScenario;
    public int activeRoute;

    private ScenarioManager scenarioManager;
    private UIManager uiManager;

    private void Awake()
    {
        scenarioManager = GameObject.FindGameObjectWithTag("ScenarioManager").GetComponent<ScenarioManager>();
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        
    }

    void Start()
    {
        activeScenario = 11;
        scenarioManager.sc11.StartScenario(); // FOR TESTING
    }

    
    void Update()
    {
        
    }

}
