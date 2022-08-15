using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int activeScenario;
    private GameObject scenarioManager;
    private GameObject uiManager;

    private void Awake()
    {
        scenarioManager = GameObject.FindGameObjectWithTag("ScenarioManager");
        uiManager = GameObject.FindGameObjectWithTag("UIManager");
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

}
