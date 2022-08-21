using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * FollowPlayer.cs
 * 
 * Component of Orthographic MapView Camera
 * Configures the 3 different camera views
 * 
 **/

public class MapView : MonoBehaviour
{
    [SerializeField] private Vector3 cameraPosition; //Camera position
    private Vector3 cameraRotation = new Vector3(90, 0, 0); //Camera x-axis tilt

    public static int mapViewNb = 1;
    
    private GameObject mapCam;
    
    public GameObject playerSymbol;
    public GameObject playerRotSymbol;
    public GameObject startSymbol;

    // Camera positions coordinates
    private Vector3 mapViewUrban = new Vector3(205, 45, 127.5f);
    private Vector3 mapViewSuburb = new Vector3(342, 45, -7.5f);
    private Vector3 mapViewEntire = new Vector3(315, 45, 62.5f);

    private float partialViewOrthoSize = 135;
    private float entireViewOrthoSize = 207.5f;

    //MapView settings
    [SerializeField] bool showPlayer;
    [SerializeField] bool showPlayerRot;
    [SerializeField] bool showStart;

    void Awake()
    {
        mapCam = this.GetComponent<CameraManager>().mapCam;   
    }

    private void Start()
    {
        transform.rotation = Quaternion.Euler(cameraRotation);  // Set the camera rotation
        mapViewNb = 1; //Set the default view
        SetCameraView(mapViewNb);  //Move the camera to the default view;
        SetMapViewSettings(true, true, true);
    }

    // Set the camera to one of 3 possible view (1 = urban, 2 = suburb, 3 = entire map)
    public void SetCameraView(int newView)
    {
        mapViewNb = newView;
        if (mapViewNb == 1)    //Urban
        {
            cameraPosition = mapViewUrban;
            mapCam.GetComponent<Camera>().orthographicSize = partialViewOrthoSize;
        }
        else if (mapViewNb == 2)  //Suburb
        {
            cameraPosition = mapViewSuburb;
            mapCam.GetComponent<Camera>().orthographicSize = mapCam.GetComponent<Camera>().orthographicSize = partialViewOrthoSize;
            ;
        }
        else if (mapViewNb == 3)   //Entire map
        {
            cameraPosition = mapViewEntire;
            mapCam.GetComponent<Camera>().orthographicSize = entireViewOrthoSize;
        }
        mapCam.transform.position = cameraPosition; // Sets camera one of the 3 possible view
    }
    // Sets the player's symbol. rotation symbol and start symbol as either visible or invisible
    public void SetMapViewSettings(bool player, bool rotation, bool start)
    {
        playerSymbol.SetActive(player);
        playerRotSymbol.SetActive(player);
        startSymbol.SetActive(start);

        showPlayer = player;
        showPlayerRot = rotation;
        showStart = start;
    }

}
