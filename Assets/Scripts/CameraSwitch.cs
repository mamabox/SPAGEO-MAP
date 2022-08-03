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
        if (playerCam.activeInHierarchy)
        {
            playerCam.SetActive(false); //Map View
            mapCam.SetActive(true);
            playerSymbol.SetActive(true);
        }
        else
        {
            playerCam.SetActive(true); //City View
            mapCam.SetActive(false);
            playerSymbol.SetActive(false);
        }
    }


}
