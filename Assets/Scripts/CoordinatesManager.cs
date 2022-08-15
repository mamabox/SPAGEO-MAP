using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CoordinatesManager : MonoBehaviour
{
    private CameraManager camManager;
    private MapView mapView;

    public string xyCoordSeparator = "_"; //TODO: Convert to CHAR
    List<string> urbanViewCoordinates = new List<string>();
    List<string> suburViewCoordinates = new List<string>();
    List<string> validCoordinates = new List<string>();

    Vector2 urbanMin = new Vector2();
    Vector2 urbanMax = new Vector2();
    Vector2 suburbMin = new Vector2();
    Vector2 suburbMax = new Vector2();

    // Min X and Y coordinates for SPAGEO City
    int urbanMinX = 0;
    int urbanMaxX = 7;
    int urbanMinY = 0;
    int urbanMaxY = 7;
    int suburbMinX = 4;
    int suburbMaxX = 11;
    int suburbMinY = -4;
    int suburbMaxY = 3;


    private int blockSize = 35;

    public void Awake()
    {
        camManager = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<CameraManager>();
        mapView = GameObject.FindGameObjectWithTag("CameraManager").GetComponent<MapView>();
    }

    void Start()
    {
        SetValidCoordinates();
        SetCoordinatesLimits();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Stores the valid coordinates in SPAGEOCity in X_Y format for drawing on the map
    private void SetValidCoordinates()
    {
        // Urban coordinates
        for (int x = urbanMinX; x <= urbanMaxX; x++)
        {
            for (int y = urbanMinY; y <= urbanMaxY; y++)
            {
                string _coord = x + xyCoordSeparator + y;
                urbanViewCoordinates.Add(_coord);
            }
        }

        // Subburb coordinates
        for (int x = suburbMinX; x <= suburbMaxX; x++)
        {
            for (int y = suburbMinY; y <= suburbMaxY; y++)
            {
                string _coord = x + xyCoordSeparator + y;
                suburViewCoordinates.Add(_coord);
            }
        }

        validCoordinates = urbanViewCoordinates.Union(suburViewCoordinates).ToList(); //Union of both sets of coordinates without duplicates

        //Debug.Log("validCoordinates: " + urbanCoordinates.Count+ "|" + suburbCoordinates.Count + "|"+ validCoordinates.Count + "): " + string.Join(",", validCoordinates));
        //Debug.Log("TEST (3,4): " + IsCoordValid("3_4"));
        //Debug.Log("TEST (-3,2): " + IsCoordValid("6_4"));
    }

    //Checks if a coordinate is part of the current mapView
    //TODO: should have 3 options depending on mapview
    public bool IsCoordValid(string coord)
    {
        if ((mapView.mapViewNb == 1 && urbanViewCoordinates.Contains(coord)) || (mapView.mapViewNb == 2 &&
            suburViewCoordinates.Contains(coord)) || (mapView.mapViewNb == 3 && validCoordinates.Contains(coord)))
            return true;
        else
            return false;
    }

    //Stores the valid min and max coordinates for drawing on the map in the urban and suburb section
    private void SetCoordinatesLimits()
    {

        urbanMin = new Vector2(urbanMinX * blockSize, urbanMinY * blockSize);
        urbanMax = new Vector2(urbanMaxX * blockSize, urbanMaxY * blockSize);
        suburbMin = new Vector2(suburbMinX * blockSize, suburbMinY * blockSize);
        suburbMax = new Vector2(suburbMaxX * blockSize, suburbMaxY * blockSize);

    }

    //Stores the valid coordinates for dropping pins on the map
    public bool IsDrawingCoordValid(Vector3 worldPosition)
    {
        int _viewNb = mapView.mapViewNb;

        bool inUrban = false;
        bool inSuburb = false;

        // Check if inside Urban coordinates
        if (worldPosition.x > urbanMin.x && worldPosition.x < urbanMax.x && worldPosition.z > urbanMin.y && worldPosition.z < urbanMax.y)
        {
            inUrban = true;
            //Debug.Log("In urban");
        }
        else
        {
            //Debug.Log("Out of urban");
        }

        // Check if inside Subburb coordinates
        if (worldPosition.x > suburbMin.x && worldPosition.x < suburbMax.x && worldPosition.z > suburbMin.y && worldPosition.z < suburbMax.y)
        {
            inSuburb = true;
            //Debug.Log("In suburb");
        }
        else
        {
            //Debug.Log("Out of suburb");
        }

        //Returns  
        if ((_viewNb == 1 && inUrban) || (_viewNb == 2 && inSuburb) || (_viewNb == 3 && (inUrban || inSuburb)))
            return true;
        else
            return false;
    }
    //TODO: TEST - To delete()
    private bool IsCoordValid(Vector3 position)
    {
        if ((position.x >= 0 && position.x <= 245) && (position.z >= 0 && position.z <= 245))
            return true;
        else
        {
            //Debug.Log("Out of bond");
            return false;
        }

    }
}
