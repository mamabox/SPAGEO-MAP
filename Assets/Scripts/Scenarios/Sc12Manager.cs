using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc12Manager : MonoBehaviour
{
    //private GameManager gameManager;
    //private CameraManager camManager;
    //private ScenarioManager scenarioManager;
    //public DrawRoute drawRoute;
    //private MapView mapView;
    //private UIManager uiManager;
    //private GameObject playerObj;

    public List<GameObject> uiElements;
    [SerializeField] bool showPlayerSymbol;
    [SerializeField] bool showPlayerSymbolRot;
    [SerializeField] bool showStartSymbol;

    [SerializeField] GameObject DrawRoutePrefab;
    //[SerializeField] GameObject startPointPrefab;
    //[SerializeField] GameObject endPointPrefab;

    [SerializeField] GameObject drawRouteParent;

    public GameObject route;
    GameObject startPoint;
    GameObject endPoint;

    LineRenderer lr;
    Pencil pencil;
    public string startPos;


    private void Awake()
    {
        //playerObj = GameObject.FindGameObjectWithTag("Player");
        //gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        //uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        //camManager = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManager>();
        //mapView = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<MapView>();
        //scenarioManager = GameObject.FindGameObjectWithTag("ScenarioManager").GetComponent<ScenarioManager>();
    }

    public void Start()
    {

    }

    //TODO: pull data from file
    public void SetRouteSettigs()
    {
        showPlayerSymbol = true;
        showPlayerSymbolRot = true;
        showStartSymbol = true;

        startPos = "3_3";
    }

    public void StartScenario()
    {
        SetRouteSettigs();

        Debug.Log(Singleton.Instance.gameMngr.activeScenario + ": StartScenario()");
        Singleton.Instance.UIMngr.HideUIParents();
        Singleton.Instance.UIMngr.ShowSidePanel(2);
        SetActiveUIElements();
        Singleton.Instance.cameraMngr.mapView.MapViewSettings(showPlayerSymbol, showPlayerSymbolRot, showStartSymbol);
        

        route = Instantiate(DrawRoutePrefab, drawRouteParent.transform); //TODO: instantiate as parent
        route.name = "NEW ROUTE";
        //drawRoute = route.GetComponent<DrawRoute>();
        //lr = route.GetComponent<LineRenderer>();
        //pencil = drawRoute.pencilObj.GetComponent<Pencil>();
        Singleton.Instance.operationsMngr.drawingAllowed = true;
        //drawRoute.SetPencil(pencilDot);
        //drawRoute = pencil.GetComponent<DrawRoute>();

        route.GetComponent<DrawRoute>().SetStartPoint(startPos);
    }



    public void EndScenario()
    {
        if (Singleton.Instance.gameMngr.activeScenario == 12)
        {
            Debug.Log(Singleton.Instance.gameMngr.activeScenario + ": EndScenario()");
            Singleton.Instance.scenarioMngr.sc12.route.GetComponent<DrawRoute>().ResetLine();
            Singleton.Instance.operationsMngr.drawingAllowed = false;
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
