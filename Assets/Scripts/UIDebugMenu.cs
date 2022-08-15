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
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();    
    }

    void Start()
    {
        shortcutsTxt.text = "0 - SC10 | 1 - SC11 | 2 - SC12 | 3 - SC13 | 4 - SC4 |  5 - SC2 |- (M) Menu d'aide - (D) Debug";
    }

    
    void Update()
    {
        //Scenario info
        scenarioInfoTxt.text = "SC: " + gameManager.activeScenario;
    }
}
