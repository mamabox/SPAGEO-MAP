using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIDebugMenu : MonoBehaviour
{

    //Debug UI elements
    [SerializeField] TextMeshProUGUI routeTxt;
    [SerializeField] TextMeshProUGUI routeDirTxt;
    [SerializeField] TextMeshProUGUI routeValidationTxt;
    [SerializeField] TextMeshProUGUI positionTxt;
    [SerializeField] TextMeshProUGUI rotationTxt;

    private char coordSeparator = ',';

    private void Awake()
    {
    }

    void Start()
    {
    }

    
    void Update()
    {
        if (GameManager.started)
        {
            routeTxt.text = "R: " + String.Join(coordSeparator, GameManager.gameData.playerRoute);
            routeDirTxt.text = "RD: " + String.Join(coordSeparator, GameManager.gameData.playerRouteWithDir);
        }
        
    }
}
