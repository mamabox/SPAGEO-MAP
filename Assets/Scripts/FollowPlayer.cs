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
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        cameraTransformOffset = new Vector3(0, 0.75f, 0);
        cameraRotationOffset = new Vector3( -5,0,0);
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + cameraTransformOffset; // Sets camera to player movement + offset
        transform.rotation = Quaternion.Euler(cameraRotationOffset+ player.transform.eulerAngles); // Sets camera to 
    }
}
