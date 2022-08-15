using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* Handles the UI
 * 
 * 
 * */

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject debugMenu;
    [SerializeField] GameObject helpMenu;
    [SerializeField] GameObject[] helpMenuImgs;
    //[SerializeField] GameObject cameraMngr;

    private CameraManager camManager;
    public bool isMenuOpen; // can be used to prevent certain actions when a dialog box or a menu is opened (e.g. changing camera views, validating routes)


    private void Awake()
    {
        camManager = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        debugMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (helpMenu.activeInHierarchy)
        {
            if (camManager.activeCam == "player")
            {
                helpMenuImgs[0].SetActive(true);
                helpMenuImgs[1].SetActive(false);
            }
            else if (camManager.activeCam == "map")
            {
                helpMenuImgs[0].SetActive(false);
                helpMenuImgs[1].SetActive(true);
            }
        }
    }

    public void OnToggleHelpMenu(InputAction.CallbackContext context)
    {
        if (helpMenu.activeInHierarchy) // IF active, hide
        {
            helpMenu.SetActive(false);
            isMenuOpen = false;
        }
        else    // IF inactive, show
        {
            helpMenu.SetActive(true);
            isMenuOpen = true;
     
        }
    }

    public void ShowHideDebugMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (debugMenu.activeInHierarchy) // IF active, hide
                debugMenu.SetActive(false);
            else    // IF inactive, show
                debugMenu.SetActive(true);

        }
    }
}
