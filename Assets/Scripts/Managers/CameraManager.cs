using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject mapCam;
   
    public MapView mapView;

    public string activeCam;

    private void Awake()
    {

        mapView = GetComponent<MapView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
