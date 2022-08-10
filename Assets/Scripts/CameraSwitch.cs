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
    }


    // Start is called before the first frame update
    void Start()
    {
        camView = 1;
        HideMap();
        /*
        var gameplayActionMap = playerControls.FindActionMap("City View");
        toggleView = gameplayActionMap.FindAction("Toggle View");

        toggleView.performed += OnToggleView;
        toggleView.canceled -= OnToggleView;
        toggleView.Enable();
        */
    }

    // Update is called once per frame
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
        if (context.started && mapViewAllowed)
        {
            if (!mapViewTimeLimit) {
                Debug.Log("ShowMap");
                playerCam.SetActive(false); //Map View
                mapCam.SetActive(true);
                playerSymbol.SetActive(true);
                sidePanels.SetActive(true);
                activeCam = "map";
            }
            else
                StartCoroutine(ShowMapTimeLimit());

        }
    }

    public void OnSetMapView(InputAction.CallbackContext context)
    {
        int maxCamView = 3;
        if(context.started)
           {
            
            if (camView < maxCamView)
                camView++;
            else
                camView = 1;
            //Debug.Log("Change cam view to #" + camView);
            mapCam.GetComponent<MapView>().SetCameraView(camView);
        }
    }

    public void OnHideMap(InputAction.CallbackContext context)
    {
        if (context.started)
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
    }


}
