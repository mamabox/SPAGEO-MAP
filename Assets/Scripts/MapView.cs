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
            size = 130;
        }
        else if (mapView == 2)
        {
            cameraPosition = new Vector3(250, 45, -17.5f);
            size = 130;
        }
        else if (mapView == 3)
        {
            cameraPosition = new Vector3(187.5f, 45, 52.5f);
            size = 200;
        }
        transform.position = cameraPosition; // Sets camera to player movement + offset
        //TODO: Set camera size
    }
}
