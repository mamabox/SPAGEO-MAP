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

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Vector3 cameraTransformOffset; //Camera offset from player
    [SerializeField] Vector3 cameraRotationOffset; //Camera x-axis tilt
    private GameObject player;
    [SerializeField] string view = "";
    [SerializeField] int mapView;

    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Start()
    {
        if (view == "city")
        {
            cameraTransformOffset = new Vector3(0, 0.75f, 0);
            cameraRotationOffset = new Vector3( -5,0,0);
        }
        else if (view == "map1")
        {
            cameraTransformOffset = new Vector3(168, 370, 47);
            cameraRotationOffset = new Vector3(90, 0, 0);
        }
        else if (view == "map2")
        {
            cameraTransformOffset = new Vector3(168, 244, 121);
            cameraRotationOffset = new Vector3(90, 0, 0);
        }
        else if (view == "map3")
        {
            cameraTransformOffset = new Vector3(245, 244, -16);
            cameraRotationOffset = new Vector3(90, 0, 0);
            GetComponent<Camera>().orthographicSize = 200;
        }
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + cameraTransformOffset; // Sets camera to player movement + offset
        transform.rotation = Quaternion.Euler(cameraRotationOffset+ player.transform.eulerAngles); // Sets camera to 
    }
}
