using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc12Manager : MonoBehaviour
{
    private GameManager gameManager;
    private CameraManager camManager;
    private DrawRoute drawRoute;
    private MapView mapView;
    private UIManager uiManager;
    private GameObject playerObj;
    public List<GameObject> uiElements;
    [SerializeField] bool showPlayerSymbol;
    [SerializeField] bool showPlayerSymbolRot;
    [SerializeField] bool showStartSymbol;

    [SerializeField] GameObject pencilPrefab;
    [SerializeField] GameObject startPointPrefab;
    [SerializeField] GameObject endPointPrefab;

    GameObject pencilDot;
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
        
    }

    public void Start()
    {
        
        SetupRoute();
        pencilDot = Instantiate(pencilPrefab); //TODO: instantiate as parent
        pencilDot.name = "NEW PENCIL";
        startPoint = Instantiate(pencilDot.GetComponent<Pencil>().startPointPrefab);
        startPoint.name = "NEW STARTPT";
        endPoint = Instantiate(pencilDot.GetComponent<Pencil>().endPointPrefab);
        endPoint.name = "NEW ENDPT";

        lr = pencilDot.GetComponent<LineRenderer>();
        pencil = pencilDot.GetComponent<Pencil>();
        drawRoute = pencil.GetComponent<DrawRoute>();

        startPos = "3_3";

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
        drawRoute.drawingAllowed = true;
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
