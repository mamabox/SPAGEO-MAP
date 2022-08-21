using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class DropPin : MonoBehaviour
{
    [SerializeField] bool cursorVisible;
    public bool dropPinEnabled; //Boolean to determine if it is possible to drop pins

    [SerializeField] GameObject dropPinPrefab;
    [SerializeField] Vector3 screenPosition;
    [SerializeField] Vector3 worldPosition = new Vector3();
    [SerializeField] Vector3 rayPosition = new Vector3();
    //[SerializeField] GameObject camManagerOld;
    [SerializeField] GameObject pinParent;
    [SerializeField] LayerMask pinsLayer;
    Ray ray;
    private int blockSize = 35;
    private int mapPosY = 40;   //Used for depth value

    //Camera
    private GameObject camManagerObj;
    //GameObject follow;
    //CameraSwitch camSwitch;
    CameraManager camManager;


    private CoordinatesManager coordManager;

    private void Awake()
    {
        camManagerObj = GameObject.FindGameObjectWithTag("CameraManager");
        //camSwitch = camManagerObj.GetComponent<CameraSwitch>();
        camManager = camManagerObj.GetComponent<CameraManager>();
        coordManager = GameObject.FindGameObjectWithTag("CoordinatesManager").GetComponent<CoordinatesManager>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //follow = Instantiate(dropPinPrefab);
        cursorVisible = true;
        dropPinEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (camManager.activeCam == "map" && Singleton.Instance.operationsMngr.dropPinEnabled)
        {
             //Get Mouse position on screen
            screenPosition = Mouse.current.position.ReadValue();
            //screenPosition.z = 40;
            worldPosition = camManager.mapCam.GetComponent<Camera>().ScreenToWorldPoint(screenPosition);   //Get world position from screen position
            ray = camManager.mapCam.GetComponent<Camera>().ScreenPointToRay(screenPosition);
            //follow.transform.position = new Vector3 (worldPosition.x, 40, worldPosition.z);

            //Make cursor visible
            if (cursorVisible)
                Cursor.visible = true;

            else
                Cursor.visible = false;

        }
    }

    //REMOVE: Now in player input
    public void OnDropDeletePin(InputAction.CallbackContext context)
    {
        if (context.performed && dropPinEnabled)
        {
            if (coordManager.IsDrawingCoordValid(worldPosition))  // IF player clicks in a valid coordinate for the current mapView
            {
                if (Physics.Raycast(ray, out RaycastHit hit, 100, pinsLayer))    //IF there is already a pin at this position
                {
                    //Debug.Log("DEL PIN");
                    Destroy(hit.transform.gameObject);
                }
                else    // ELSE drop a pin
                {
                    //Debug.Log("DROP PIN");
                    Instantiate(dropPinPrefab, new Vector3(worldPosition.x, mapPosY, worldPosition.z), Quaternion.identity, pinParent.transform); //Instantiate at depth value of map height
                }
            }

        }
    }

    public void DropDeletePin()
    {
        if (Singleton.Instance.coordinatesMngr.IsDrawingCoordValid(worldPosition) && Singleton.Instance.operationsMngr.dropPinEnabled)  // IF player clicks in a valid coordinate for the current mapView + dropPin is allowed
        {
            if (Physics.Raycast(ray, out RaycastHit hit, 100, pinsLayer))    //IF there is already a pin at this position
            {
                //Debug.Log("DEL PIN");
                Destroy(hit.transform.gameObject);
            }
            else    // ELSE drop a pin
            {
                //Debug.Log("DROP PIN");
                Instantiate(dropPinPrefab, new Vector3(worldPosition.x, mapPosY, worldPosition.z), Quaternion.identity, pinParent.transform); //Instantiate at depth value of map height
            }
        }
    }

    //TODO: Delete obsolete
    public void OnDropPin(InputAction.CallbackContext context)
    {

        if (context.performed && dropPinEnabled)
        {
            if (coordManager.IsDrawingCoordValid(worldPosition))
                Instantiate(dropPinPrefab, new Vector3(worldPosition.x, mapPosY, worldPosition.z), Quaternion.identity, pinParent.transform); //Instantiate at depth value of map height
        }
    }

    //TODO: Delete obsolete
    public void OnDeletePin(InputAction.CallbackContext context)
    {
        if (context.performed && dropPinEnabled)
        {
            Debug.Log("Right click");
            if (Physics.Raycast(ray, out RaycastHit hit, 10, pinsLayer))
            {
                
                rayPosition = hit.point;
                if (hit.collider)
                {
                    Debug.Log("HIT");
                    Destroy(hit.transform.gameObject);
                }
            }
            else
                Debug.Log(" NO hit");
        }
    }

    public void DeleteAllPins()
    {
        if (Singleton.Instance.operationsMngr.dropPinEnabled)
        {
            foreach (Transform child in pinParent.transform)
                Destroy(child.gameObject);
        }

    }
    


    //TODO: DELETE, Moved to CoordinatesManager.cs
    //Stores the valid coordinates for dropping pins on the map
    private bool IsValid()
    {
        int _viewNb = camManager.GetComponent<MapView>().mapViewNb;

        int urbanMinX = 0;
        int urbanMaxX = 7;
        int urbanMinY = 0;
        int urbanMaxY = 7;
        int suburbMinX = 4;
        int suburbMaxX = 11;
        int suburbMinY = -4;
        int suburbMaxY = 3;

        bool inUrban = false;
        bool inSuburb = false;

        Vector2 urbanMin = new Vector2(urbanMinX * blockSize, urbanMinY * blockSize);
        Vector2 urbanMax = new Vector2(urbanMaxX * blockSize, urbanMaxY * blockSize);
        Vector2 suburbMin = new Vector2(suburbMinX * blockSize, suburbMinY * blockSize);
        Vector2 suburbMax = new Vector2(suburbMaxX * blockSize, suburbMaxY * blockSize);



        // Urban coordinates
        if (worldPosition.x > urbanMin.x && worldPosition.x < urbanMax.x && worldPosition.z > urbanMin.y && worldPosition.z < urbanMax.y)
        {
            inUrban = true;
            //Debug.Log("In urban");
        }
        else
        {   
            //Debug.Log("Out of urban");
        }


        // Subburb coordinates
        if (worldPosition.x > suburbMin.x && worldPosition.x < suburbMax.x && worldPosition.z > suburbMin.y && worldPosition.z < suburbMax.y)
        {
            inSuburb = true;
            //Debug.Log("In suburb");
        }
        else
        {
            //Debug.Log("Out of suburb");
        }

           if ((_viewNb == 1 && inUrban) || (_viewNb == 2 && inSuburb) || (_viewNb == 3 && (inUrban || inSuburb )))
                return true;
        else
            return false;
    }
    //TODO: DELETE, Moved to CoordinatesManager.cs
    private bool IsCoordValid(Vector3 position)
    {
        if ((position.x >=0 && position.x <= 245) && (position.z >= 0 && position.z <= 245))
            return true;
        else {
            //Debug.Log("Out of bond");
            return false;
        }
            
    }

}
