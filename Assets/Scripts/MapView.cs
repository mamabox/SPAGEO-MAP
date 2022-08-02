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
    [SerializeField] Vector3 cameraTransformOffset; //Camera offset from player
    [SerializeField] Vector3 cameraRotationOffset; //Camera x-axis tilt
    private GameObject player;
    [SerializeField] int mapView = 1;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetCameraView(2);
    }


    public void SetCameraView(int mapView)
    {
        if (mapView == 1)
        {
            cameraTransformOffset = new Vector3(168, 370, 47);
            cameraRotationOffset = new Vector3(90, 0, 0);
        }
        else if (mapView == 2)
        {
            cameraTransformOffset = new Vector3(168, 244, 121);
            cameraRotationOffset = new Vector3(90, 0, 0);
        }
        else if (mapView == 3)
        {
            cameraTransformOffset = new Vector3(245, 244, -16);
            cameraRotationOffset = new Vector3(90, 0, 0);
        }
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + cameraTransformOffset; // Sets camera to player movement + offset
        transform.rotation = Quaternion.Euler(cameraRotationOffset + player.transform.eulerAngles); // Sets camera to 
    }
}
