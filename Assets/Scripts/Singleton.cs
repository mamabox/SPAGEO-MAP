using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This Singleton acts as a Service/Manager locator and give read-only access to the various managers
 * 
 * */

public class Singleton : MonoBehaviour
{
    // Instance of the Singleton
    public static Singleton Instance { get; private set; }

    // Managers used in game that will be accessed via Singleton
    public CameraManager cameraMngr { get; private set; }
    public EnvironmentManager environmentMngr { get; private set; }
    public CoordinatesManager coordinatesMngr { get; private set; }
    public GameManager gameMngr { get; private set; }
    public IntersectionsManager intersectionsMngr { get; private set; }
    public OperationsManager operationsMngr { get; private set; }
    public PlayerActionsManager playerInputMngr { get; private set; }
    public ScenarioManager scenarioMngr { get; private set; }
    public UIManager UIMngr { get; private set; }
    

    private void Awake()
    {
        // IF there is already an instance of the Singleton, delete myself
        if (Instance!= null & Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
            // Do not destroy the game object when loading different scenes
            //DontDestroyOnLoad(gameObject);
            SetupReferences();
        }
    }

    private void SetupReferences()
    {
        cameraMngr = GetComponentInChildren<CameraManager>();
        environmentMngr = GetComponentInChildren<EnvironmentManager>();
        coordinatesMngr = GetComponentInChildren<CoordinatesManager>();
        gameMngr = GetComponentInChildren<GameManager>();
        intersectionsMngr = GetComponentInChildren<IntersectionsManager>();
        operationsMngr = GetComponentInChildren<OperationsManager>();
        playerInputMngr = GetComponentInChildren<PlayerActionsManager>();
        scenarioMngr = GetComponentInChildren<ScenarioManager>();
        UIMngr = GetComponentInChildren<UIManager>();
    
    }
}
