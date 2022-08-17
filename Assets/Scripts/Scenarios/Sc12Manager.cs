using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc12Manager : MonoBehaviour
{
    private GameManager gameManager;
    private CameraManager camManager;
    private ScenarioManager scenarioManager;
    public DrawRoute drawRoute;
    private MapView mapView;
    private UIManager uiManager;
    private GameObject playerObj;
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
    string startPos;


    private void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        camManager = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManager>();
        mapView = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<MapView>();
        scenarioManager = GetComponent<ScenarioManager>();
        
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
        

        route = Instantiate(DrawRoutePrefab, drawRouteParent.transform); //TODO: instantiate as parent
        route.name = "NEW ROUTE";
        drawRoute = route.GetComponent<DrawRoute>();
        //lr = route.GetComponent<LineRenderer>();
        pencil = drawRoute.pencilDot.GetComponent<Pencil>();
        scenarioManager.drawingAllowed = true;
        //drawRoute.SetPencil(pencilDot);

        //drawRoute = pencil.GetComponent<DrawRoute>();

        startPos = "3_3";
        drawRoute.SetStartPoint(startPos);
    }



    public void EndScenario()
    {
        Debug.Log(gameManager.activeScenario + ": EndScenario()");
        drawRoute.drawingAllowed = false;
    }

    private void SetActiveUIElements()
    {
        foreach (GameObject element in uiElements)
        {
            element.SetActive(true);
        }
    }
}
