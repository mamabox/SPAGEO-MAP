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
        var gameplayActionMap = playerControls.FindActionMap("Default");
        toggleView = gameplayActionMap.FindAction("Toggle View");

        toggleView.performed += OnToggleView;
        toggleView.canceled -= OnToggleView;
        toggleView.Enable();

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
            if (!mapViewTimeLimit)
                ShowMap();
            else
                StartCoroutine(ShowMapTimeLimit());
        }
        else // IF in  Map view, hide map
        {
            HideMap();
        }
    }


    IEnumerator ShowMapTimeLimit()  //Only one step back allowed before moving forward
    {
        mapViewAllowed = false;
        playerCam.SetActive(false); //Map View
        mapCam.SetActive(true);
        playerSymbol.SetActive(true);
        yield return new WaitForSeconds(mapViewTime);
        HideMap();
        mapViewAllowed = true;

    }

    private void ShowMap()  //Only one step back allowed before moving forward
    {
        playerCam.SetActive(false); //Map View
        mapCam.SetActive(true);
        playerSymbol.SetActive(true);
    }

    private void HideMap()
    {
        playerCam.SetActive(true); //City View
        mapCam.SetActive(false);
        playerSymbol.SetActive(false);
    }
}
