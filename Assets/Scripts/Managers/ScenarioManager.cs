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

    void Start()
    {
        
    }


    void Update()
    {
        
    }


}
