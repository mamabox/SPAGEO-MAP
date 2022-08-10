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

    [SerializeField] GameObject helpMenu;
    [SerializeField] GameObject[] helpMenuImgs;
    [SerializeField] GameObject cameraMngr;

    private CameraSwitch camera;
    public bool isMenuOpen; // can be used to prevent certain actions when a dialog box or a menu is opened (e.g. changing camera views, validating routes)


    private void Awake()
    {
        camera = cameraMngr.GetComponent<CameraSwitch>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (helpMenu.activeInHierarchy)
        {
            if (camera.activeCam == "player")
            {
                helpMenuImgs[0].SetActive(true);
                helpMenuImgs[1].SetActive(false);
            }
            else if (camera.activeCam == "map")
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
}
