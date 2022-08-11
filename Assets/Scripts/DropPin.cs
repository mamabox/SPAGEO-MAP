using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class DropPin : MonoBehaviour
{
    [SerializeField] bool cursorVisible;
    [SerializeField] GameObject dropPinPrefab;
    [SerializeField] Vector3 screenPosition;
    [SerializeField] Vector3 worldPosition = new Vector3();
    [SerializeField] GameObject camManager;

    GameObject follow;
    CameraSwitch cam;

    // Start is called before the first frame update
    void Start()
    {
        //follow = Instantiate(dropPinPrefab);
        cursorVisible = true;
        cam = camManager.GetComponent<CameraSwitch>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cam.activeCam == "map")
        {
             //Get Mouse position
            screenPosition = Mouse.current.position.ReadValue();
            //screenPosition.z = 40;
            worldPosition = cam.mapCam.GetComponent<Camera>().ScreenToWorldPoint(screenPosition);
            //follow.transform.position = new Vector3 (worldPosition.x, 40, worldPosition.z);

            //Make cursor visible
            if (cursorVisible)
                Cursor.visible = true;

            else
                Cursor.visible = false;

        }
    }

    public void OnDropPin(InputAction.CallbackContext context)
    {
        if (context.performed) { 
        
        Instantiate(dropPinPrefab, new Vector3(worldPosition.x, 40, worldPosition.z), Quaternion.identity);
    }
    }
}
