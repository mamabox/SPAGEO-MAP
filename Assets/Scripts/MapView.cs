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
    private Vector3 cameraPosition; //Camera offset from player
    private Vector3 cameraRotation = new Vector3(90, 0, 0); //Camera x-axis tilt
    private GameObject player;
    public int mapViewNb = 1;
    private int size;
    private GameObject mapCam;

    
    public GameObject playerSymbol;
    public GameObject playerRotSymbol;
    public GameObject startSymbol;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mapCam = this.GetComponent<CameraManager>().mapCam;
        
    }

    private void Start()
    {
        transform.rotation = Quaternion.Euler(cameraRotation);  // Set the camera rotation
        mapViewNb = 1; //Set the default view
        SetCameraView(mapViewNb);  //Move the camera to the default view;
    }

    // Set the camera to one of 3 possible view (1 = urban, 2 = suburb, 3 = entire map)
    public void SetCameraView(int _mapView)
    {
        mapViewNb = _mapView;
        if (mapViewNb == 1)    //Urban
        {
            cameraPosition = new Vector3(205, 45, 127.5f);
            mapCam.GetComponent<Camera>().orthographicSize = 135;
        }
        else if (mapViewNb == 2)  //Suburb
        {
            cameraPosition = new Vector3(342, 45, -7.5f);
            mapCam.GetComponent<Camera>().orthographicSize = 135;
        }
        else if (mapViewNb == 3)   //Entire map
        {
            cameraPosition = new Vector3(315, 45, 62.5f);
            mapCam.GetComponent<Camera>().orthographicSize = 207.5f;
        }
        mapCam.transform.position = cameraPosition; // Sets camera one of the 3 possible view
    }

    public void MapViewSettins(bool player, bool rotation, bool start)
    {
        playerSymbol.SetActive(player);
        playerRotSymbol.SetActive(player);
        startSymbol.SetActive(start);
    }
}
