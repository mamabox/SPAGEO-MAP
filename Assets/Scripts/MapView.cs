using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * FollowPlayer.cs
 * 
 * Component of MapView Camera
 * Configures camera to follow the player
 * 
 **/

public class MapView : MonoBehaviour
{
    private Vector3 cameraPosition; //Camera offset from player
    private Vector3 cameraRotation = new Vector3(90, 0, 0); //Camera x-axis tilt
    private GameObject player;
    public int mapView = 1;
    private int size;

    [SerializeField] List<GameObject> sidePanels;   // 0 = enter text, 1 = draw route


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = Quaternion.Euler(cameraRotation);
        SetCameraView(mapView);
    }


    public void SetCameraView(int _mapView)
    {
        mapView = _mapView;
        if (mapView == 1)
        {
            cameraPosition = new Vector3(205, 45, 127.5f);
            GetComponent<Camera>().orthographicSize = 135;
        }
        else if (mapView == 2)
        {
            cameraPosition = new Vector3(342, 45, -7.5f);
            GetComponent<Camera>().orthographicSize = 135;
        }
        else if (mapView == 3)
        {
            cameraPosition = new Vector3(315, 45, 62.5f);
            GetComponent<Camera>().orthographicSize = 207.5f;
        }
        transform.position = cameraPosition; // Sets camera one of the 3 possible view
    }
}
