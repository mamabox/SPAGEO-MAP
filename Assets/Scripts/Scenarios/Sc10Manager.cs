using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc10Manager : MonoBehaviour
{
    private GameManager gameManager;
    private CameraManager camManager;
    private MapView mapView;
    private UIManager uiManager;
    private GameObject playerObj;
    public List<GameObject> uiElements;
    [SerializeField] bool showPlayerSymbol;
    [SerializeField] bool showPlayerSymbolRot;
    [SerializeField] bool showStartSymbol;
    

    private void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        camManager = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManager>();
        mapView = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<MapView>();
    }

    public void Start()
    {
        SetupRoute();

    }

    //TODO: pull data from file
    public void SetupRoute()
    {
        showPlayerSymbol = true;
        showPlayerSymbolRot = true;
        showStartSymbol = true;
    }

    public void StartScenario()
    {
        Debug.Log(gameManager.activeScenario + ": StartScenario()");
        //uiManager.ShowSidePanel(0);
        uiManager.HideUIParents();
        SetActiveUIElements();
        mapView.MapViewSettins(showPlayerSymbol, showPlayerSymbolRot, showStartSymbol);

    }



    public void EndScenario()
    {
        if (gameManager.activeScenario == 10)
        {
            Debug.Log(gameManager.activeScenario + ": EndScenario()");
        }
    }

    private void SetActiveUIElements()
    {
        foreach (GameObject element in uiElements)
        {
            element.SetActive(true);
        }
    }



}
