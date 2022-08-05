using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitch : MonoBehaviour
{

    public GameObject playerCam;
    public GameObject mapCam;
    public GameObject playerSymbol;

    [SerializeField] InputActionAsset playerControls;
    InputAction toggleView;

    [SerializeField] private float mapViewTime = 2f;
    [SerializeField] bool mapViewTimeLimit = false;
    [SerializeField] bool mapViewAllowed = true;


    // Start is called before the first frame update
    void Start()
    {
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

    public void OnToggleView(InputAction.CallbackContext context)
    {
       
        
            Debug.Log("Toggle view");
            if (playerCam.activeInHierarchy && mapViewAllowed)    //IF in City view, show map
            {

                if (!mapViewTimeLimit) { }
                //ShowMap();
                
            }
            else // IF in  Map view, hide map
            {
                
            }
        

    }


    IEnumerator ShowMapTimeLimit()  //Only one step back allowed before moving forward
    {
        mapViewAllowed = false;
        playerCam.SetActive(false); //Map View
        mapCam.SetActive(true);
        //playerSymbol.SetActive(true);
        yield return new WaitForSeconds(mapViewTime);
        HideMap();
        mapViewAllowed = true;

    }

    public void OnShowMap(InputAction.CallbackContext context)  //Only one step back allowed before moving forward
    {
        if (context.performed && mapViewAllowed)
        {
            if (!mapViewTimeLimit) {
                Debug.Log("ShowMap");
                playerCam.SetActive(false); //Map View
                mapCam.SetActive(true);
                playerSymbol.SetActive(true);
            }
            else
                StartCoroutine(ShowMapTimeLimit());

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
    }
}
