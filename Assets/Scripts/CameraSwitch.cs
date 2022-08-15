using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitch : MonoBehaviour
{

    public GameObject playerCam;
    public GameObject mapCam;
    public GameObject playerSymbol;
    [SerializeField] GameObject sidePanels;
    [SerializeField] GameObject uiManager;

    //Player
    private GameObject playerObj;
    private PlayerInput playerInput;

    //Camera
    CameraManager camManager;

    UIManager ui;

    [SerializeField] InputActionAsset playerControls;
    InputAction toggleView;

    [SerializeField] private float mapViewTime = 2f;
    [SerializeField] bool mapViewTimeLimit = false;
    [SerializeField] bool mapViewAllowed = true;
    [SerializeField] int camView;
    public string activeCam;

    void Awake()
    {
        ui = uiManager.GetComponent<UIManager>();
        //MapActions() //Map actions

        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerInput = playerObj.GetComponent<PlayerInput>();
        camManager = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManager>();
}

    void Start()
    {
        camView = 1;
        HideMap();
    }

    void Update()
    {
  
    }

    IEnumerator ShowMapTimeLimit()  //Show map for a specific duration
    {
        Debug.Log("ShowMapTimeLimit");
        mapViewAllowed = false;
        playerCam.SetActive(false); //Map View
        mapCam.SetActive(true);
        sidePanels.SetActive(true);
        //playerSymbol.SetActive(true);
        yield return new WaitForSeconds(mapViewTime);
        HideMap();
        mapViewAllowed = true;
        activeCam = "map";

    }

    public void OnShowMap(InputAction.CallbackContext context)  //Show map
    {
        if (context.performed && mapViewAllowed)
        {
            if (!mapViewTimeLimit) {
                Debug.Log("ShowMap");
                playerCam.SetActive(false); //Map View
                mapCam.SetActive(true);
                playerSymbol.SetActive(true);
                sidePanels.SetActive(true);
                activeCam = "map";
                playerInput.SwitchCurrentActionMap("mapView");
            }
            else
                StartCoroutine(ShowMapTimeLimit());

        }
    }

    public void OnSetMapView(InputAction.CallbackContext context)
    {
        int maxCamView = 3;
        if(context.performed)
           {
            
            if (camView < maxCamView)
                camView++;
            else
                camView = 1;
            //Debug.Log("Change cam view to #" + camView);
            //mapCam.GetComponent<MapView>().SetCameraView(camView); //TODO: OLD, to delete
            this.GetComponent<MapView>().SetCameraView(camView);

        }
    }

    public void OnHideMap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            HideMap();
            
        }
    }

    private void HideMap()
    {
        Debug.Log("HideMap");
        playerCam.SetActive(true); //City View
        mapCam.SetActive(false);
        sidePanels.SetActive(false);
        activeCam = "player";
        playerInput.SwitchCurrentActionMap("playerView");
    }

    //Example of how to map actions in code VS in Editor
    void MapActions()
    {
        var playerViewActionMap = playerControls.FindActionMap("PlayerView");
        var mapViewActionMap = playerControls.FindActionMap("MapView");
        toggleView = playerViewActionMap.FindAction("Show Map");

        toggleView.performed += OnShowMap;
        toggleView.canceled -= OnShowMap;
        toggleView.Enable();
    }

}
