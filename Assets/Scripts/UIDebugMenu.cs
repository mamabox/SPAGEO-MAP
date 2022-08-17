using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDebugMenu : MonoBehaviour
{
    private GameManager gameManager;

    //Debug UI elements
    [SerializeField] TextMeshProUGUI scenarioInfoTxt;
    [SerializeField] TextMeshProUGUI shortcutsTxt;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();  
    }

    void Start()
    {
        shortcutsTxt.text = "(M) Help - (D) Debug";
    }

    
    void Update()
    {
        //Scenario info
        scenarioInfoTxt.text = "SC: " + gameManager.activeScenario;
    }
}
