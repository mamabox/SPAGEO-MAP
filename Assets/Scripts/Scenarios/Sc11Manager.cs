using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc11Manager : MonoBehaviour
{
//    private GameManager gameManager;
//    private CameraManager camManager;
//    //public DropPin dropPin;
//    private MapView mapView;
//    private UIManager uiManager;
//    private GameObject playerObj;
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
        //mapView = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<MapView>();
        //dropPin = GetComponent<DropPin>();
    }

    public void Start()
    {
        
        

    }

    //TODO: pull data from file
    public void SetRouteSettings()
    {
        showPlayerSymbol = true;
        showPlayerSymbolRot = true;
        showStartSymbol = true;
    }

    public void StartScenario()
    {
        SetRouteSettings();

        //Debug.Log(GameManager.gameData.scenario + ": StartScenario()");
        //uiManager.ShowSidePanel(0);
        Singleton.Instance.UIMngr.HideUIParents();
        Singleton.Instance.UIMngr.ShowSidePanel(1);
        SetActiveUIElements();
        Singleton.Instance.cameraMngr.mapView.SetMapViewSettings(showPlayerSymbol, showPlayerSymbolRot, showStartSymbol);
        Singleton.Instance.operationsMngr.dropPinEnabled = true;
    }

    public void SaveData()
    {

    }

    public void EndScenario()
    {
        if (GameManager.gameData.scenario == 11)
        {
            //Debug.LogFormat("EndScenario({0})", GameManager.gameData.scenario);
            Singleton.Instance.operationsMngr.dropPinEnabled = false;
            Singleton.Instance.operationsMngr.dropPin.DeleteAllPins();
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
