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
    [SerializeField] TextMeshProUGUI routeDirLiveTxt;
    [SerializeField] TextMeshProUGUI routeDirAllTxt;
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
            routeDirLiveTxt.text = "RDL: " + String.Join(coordSeparator, GameManager.gameData.playerRouteWithDirLive);
            routeDirAllTxt.text = "RDLA: " + String.Join(coordSeparator, GameManager.gameData.playerRouteWithDirLiveAll);
            routeValidationTxt.text = "Validation";
            positionTxt.text = $"POS: ({GameManager.player.transform.position.x.ToString("F2")} , {GameManager.player.transform.position.z.ToString("F2")})";
            rotationTxt.text = "ROT: " + GameManager.player.transform.rotation.eulerAngles.y.ToString("F2");
        }
        
    }
}
