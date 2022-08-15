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

    //UI Menus
    [SerializeField] GameObject debugMenu;
    [SerializeField] GameObject helpMenu;
    [SerializeField] GameObject[] helpMenuImgs;
    [SerializeField] List<GameObject> sidePanels;   // 0 = enter text, 1 = indicate POI, 3 = draw route

    // Parent of Gameobjects to instantiate
    public GameObject pinsParent;
    public GameObject poiNumbersParent;
    public GameObject pencilParent;


    //[SerializeField] GameObject cameraMngr;

    private CameraManager camManager;
    private CameraSwitch camSwitch;
    public bool isMenuOpen; // can be used to prevent certain actions when a dialog box or a menu is opened (e.g. changing camera views, validating routes)


    private void Awake()
    {
        camManager = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManager>();
        camSwitch = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraSwitch>();
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

    //Hide all sidePanels except for one
    public void ShowSidePanel (int panelID)
    {
        for (int x = 0; x < sidePanels.Count; x++)
        {
            if (x == panelID)
                sidePanels[x].SetActive(true);
            else
                sidePanels[x].SetActive(false);
        }
    }

    //Hide All GameObjects Parent
    public void HideUIParents()
    {
        pinsParent.SetActive(false);
        poiNumbersParent.SetActive(false);
        pencilParent.SetActive(false);
    }

    public void OnSubmitAnswer()
    {
        Debug.Log("Submit Answer");
        camSwitch.HideMap();
    }
}
