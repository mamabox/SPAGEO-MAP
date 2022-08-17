using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public bool drawingAllowed;

    // Scenario managers
    public Sc10Manager sc10;
    public Sc11Manager sc11;
    public Sc12Manager sc12;

    private void Awake()
    {
        // Scenario Managers
        sc10 = GetComponent<Sc10Manager>();
        sc11 = GetComponent<Sc11Manager>();
        sc12 = GetComponent<Sc12Manager>();

        // Variables
        drawingAllowed = false;
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }


}
