using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc10Manager : MonoBehaviour
{
    //private GameManager gameManager;
    //private CameraManager camManager;
    //private MapView mapView;
    //private UIManager uiManager;
    //private GameObject playerObj;
    
    public List<GameObject> uiElements;
    [SerializeField] bool showPlayerSymbol;
    [SerializeField] bool showPlayerSymbolRot;
    [SerializeField] bool showStartSymbol;
    

    private void Awake()
    {
        //playerObj = GameObject.FindGameObjectWithTag("Player");
        //gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        //uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        //camManager = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManager>();
        //mapView = Singleton.Instance.cameraMngr.GetComponent<MapView>();
    }

    public void Start()
    {
        

    }

    //TODO: pull data from file
    public void SetRouteSettings()
    {
        //TODO: (1) Read data for active route 
        showPlayerSymbol = true;
        showPlayerSymbolRot = true;
        showStartSymbol = true;

        Singleton.Instance.cameraMngr.mapView.SetMapViewSettings(showPlayerSymbol, showPlayerSymbolRot, showStartSymbol);
    }

    public void StartScenario()
    {
        Debug.Log(GameManager.gameData.scenario + ": StartScenario()");
        //Debug.Log(gameManager.gameData.scenario + ": StartScenario()");
        //uiManager.ShowSidePanel(0);

        //REMOVE: old system
        // uiManager.HideUIParents();
        //uiManager.ShowSidePanel(0);

        Singleton.Instance.UIMngr.HideUIParents();
        Singleton.Instance.UIMngr.ShowSidePanel(0);
        SetActiveUIElements();

        SetRouteSettings();

    }



    public void EndScenario()
    {
        if (GameManager.gameData.scenario == 10)
        {
            Debug.Log(GameManager.gameData.scenario + ": EndScenario()");
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
