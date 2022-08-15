using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    // Scenario managers
    public Sc10Manager sc10;
    public Sc11Manager sc11;
    public Sc12Manager sc12;

    private void Awake()
    {
        sc10 = GetComponent<Sc10Manager>();
        sc11 = GetComponent<Sc11Manager>();
        sc12 = GetComponent<Sc12Manager>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }


}
