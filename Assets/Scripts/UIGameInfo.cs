using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Displays game information during gameplay
 * */

public class UIGameInfo : MonoBehaviour
{

    //Game info UI elements
    [SerializeField] TextMeshProUGUI scenarioInfoTxt;
    [SerializeField] TextMeshProUGUI shortcutsTxt;


    private void Awake()
    {
    }

    void Start()
    {
        shortcutsTxt.text = "(M) Help - (V) Toggle Player/Map view - (C) Change MapView - (D) Debug - (X) New attempt - (SPACE) Validate ";
    }


    void Update()
    {
        //Scenario info
        scenarioInfoTxt.text = "SC: " + GameManager.gameData.scenario;
    }
}
