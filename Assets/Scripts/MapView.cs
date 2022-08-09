using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * FollowPlayer.cs
 * 
 * Component of Main Camera
 * Configures camera to follow the player
 * 
 **/

public class MapView : MonoBehaviour
{
    private Vector3 cameraPosition; //Camera offset from player
    private Vector3 cameraRotation = new Vector3(90, 0, 0); //Camera x-axis tilt
    private GameObject player;
    [SerializeField] int view = 1;
    private int size;

    [SerializeField] List<GameObject> sidePanels;   // 0 = enter text, 1 = draw route


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = Quaternion.Euler(cameraRotation);
        SetCameraView(1);
    }


    public void SetCameraView(int mapView)
    {
        view = mapView;
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
        transform.position = cameraPosition; // Sets camera to player movement + offset
        //TODO: Set camera size
    }
}
