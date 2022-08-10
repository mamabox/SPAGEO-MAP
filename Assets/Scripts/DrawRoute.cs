using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

public class DrawRoute : MonoBehaviour
{
    [SerializeField] GameObject startPointPrefab;
    [SerializeField] GameObject endPointPrefab;
    //[SerializeField] GameObject pencilPrefab;
    [SerializeField] GameObject mapCam;

    //GameObject pencilDot;
    GameObject startPoint;
    GameObject endPoint;

    Vector2 drawInput;  //player input for drawing

    LineRenderer lr;
    Pencil pencil;
    MapView mapView;

    float height = 41f;
    float multiplier = 35;

    // USED FOR TESTING 
    List<string> testCoordinates = new List<string> { "0_0", "0_1", "0_2", "1_2" }; // Test line to draw
    List<Vector3> points = new List<Vector3>();
    Vector3[] pointsArray;

    public string xyCoordSeparator = "_"; //TODO: Convert to CHAR
    List<string> urbanViewCoordinates = new List<string>();
    List<string> suburViewCoordinates = new List<string>();
    List<string> validCoordinates = new List<string>();
    private string startPos;
    private Vector3 startPosVector;

    bool drawingAllowed;    // Is drawing allowed during this scenario



    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        pencil = GetComponent<Pencil>();
        mapView = mapCam.GetComponent<MapView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate Prefabs
        startPoint = Instantiate(startPointPrefab);


        startPos = "3_3";   // TESTING: start value
        SetValidCoordinates();
        SetStartPoint();

        //Debug.Log("coord count: "+ coordinates.Count);
        //Debug.Log("count: " + pointsArray.Length);

        // TEST SECTION: Creates
        pointsArray = new Vector3[testCoordinates.Count];
        StringToPoints(testCoordinates);
        //SetupLine(pointsArray);
    }


    private void SetStartPoint()
    {
        //Initialise pencil coordinates / information
        pencil.startCoord = CreateCoordinate(startPos);
        pencil.currentCoord = CreateCoordinate(startPos);
        pencil.lastCoord = CreateCoordinate(startPos);
        pencil.routeAllPoints.Add(startPos);
        pencil.route.Add(startPos);

        //Move start point and drawing point
        startPoint.transform.position = pencil.startCoord.pos;
        transform.position = pencil.startCoord.pos;

        //TODO: SetActive for objects depending on drawingAllowed
        //point.SetActive(false);

        //Line renderer setup
        lr.SetPosition(0,pencil.startCoord.pos);
        
    }

    //Creates a Coordinate from a string in format "X_Y"
    private Coordinate CreateCoordinate(string coord)
    {
        Coordinate _coord = new Coordinate();
        string[] _coordString = coord.Split(char.Parse(xyCoordSeparator));
        float[] _coordFloat = { float.Parse(_coordString[0]), float.Parse(_coordString[1]) };
        Vector3 _pos = new Vector3(_coordFloat[0] * multiplier, height, _coordFloat[1] * multiplier);

        _coord.name = coord;
        _coord.x = float.Parse(_coordString[0]);
        _coord.z = float.Parse(_coordString[1]);
        _coord.pos = new Vector3(_coord.x * multiplier, height, _coord.z * multiplier);

        return _coord;
    }

    // Stores in a list of coordinate ("X_Y", "X_Y") into the pointsarray
    //TODO: should return array
    private void StringToPoints(List<string> route)
    {
        //Debug.Log("Route: " + string.Join(",",coordinates));
        //Debug.Log("# of route points is " + route.Count);
        for (int i = 0; i < route.Count; i++)
        {
            string[] _coord = route[i].Split(char.Parse(xyCoordSeparator));
            //Debug.Log("coordX: " + _coord[0] + "|" + "coordY: " + _coord[1]);
            points.Add(new Vector3(float.Parse(_coord[0]) * multiplier, height, float.Parse(_coord[1]) * multiplier));
            //Debug.Log("count: " + pointsArray.Length);
            //pointsArray[i] = new Vector3(float.Parse(_coord[0]) * multiplier, height, float.Parse(_coord[1]) * multiplier);
      
        }
        pointsArray = points.ToArray();
    }

    //Render a line from a Vector3[]
    public void RenderLineFromArray(Vector3[] points)
    {
        lr.positionCount = points.Length;
        lr.SetPositions(points);
    }

    // HANDLES PLAYER INPUT
    public void OnDrawInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            drawInput = context.ReadValue<Vector2>();
            if (drawInput == new Vector2(0, 1))  //UP
            {
                Debug.Log("Draw UP");
                RenderLineFromInput("UP");
            }
            else if (drawInput == new Vector2(0, -1)) //DOWN
            {
                Debug.Log("Draw DOWN");
                RenderLineFromInput("DOWN");
            }
            else if (drawInput == new Vector2(-1, 0)) //LEFT
            {
                Debug.Log("Draw LEFT");
                RenderLineFromInput("LEFT");
            }
            else if (drawInput == new Vector2(1, 0))  //RIGHT
            {
                Debug.Log("Draw RIGHT");
                RenderLineFromInput("RIGHT");
            }
        }
    }

    public void OnDeleteLine(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            lr.positionCount = 1;   //remove all points

            //Delete routes
            pencil.routeAllPoints.Clear();
            pencil.route.Clear();

            SetStartPoint();
        }
    }

    public void OnValidateRoute(InputAction.CallbackContext context)
    {

    }

    public void RenderLineFromInput(string direction)
    {
        Coordinate _nextCoord;
        string _nextCoordString = "";

        if (direction == "UP")
        {
            _nextCoordString = pencil.currentCoord.x + xyCoordSeparator + (pencil.currentCoord.z + 1);
        }
        else if (direction == "DOWN")
        {
            _nextCoordString = pencil.currentCoord.x + xyCoordSeparator + (pencil.currentCoord.z - 1);
        }
        else if (direction == "LEFT")
        {
            _nextCoordString = (pencil.currentCoord.x - 1) + xyCoordSeparator + pencil.currentCoord.z;
        }
        else if (direction == "RIGHT")
        {
            _nextCoordString = (pencil.currentCoord.x + 1) + xyCoordSeparator + pencil.currentCoord.z;
        }
        Debug.Log("Next coord string: " + _nextCoordString);
        _nextCoord = CreateCoordinate(_nextCoordString);

        // If the next coordinate is valid, move the pencil
        if (IsCoordValid(_nextCoord.name))
            DrawNextPosition(_nextCoord);
    }

    //TODO: check if longer than 1
    private void DrawNextPosition(Coordinate nextCoord)
    {
        int _routeLength = pencil.route.Count;
        //Debug.Log("_nextCoord pos: " + nextCoord.pos);

        // (1) Record coordinates
        if ((pencil.route.Count > 1) && (pencil.route.ElementAt(pencil.route.Count-2) == nextCoord.name))  //IF tracing back step
        {
            //Delete the last point in the renderline
            lr.positionCount = pencil.route.Count - 1;
            pencil.route.RemoveAt(pencil.route.Count - 1);
        }
        else
        {
            //ELSE Add a point to the route render line
            pencil.route.Add(nextCoord.name);
            lr.positionCount = pencil.route.Count;
            lr.SetPosition(pencil.route.Count-1, nextCoord.pos);
        }

        pencil.routeAllPoints.Add(nextCoord.name); // Add to the list of all coordinates

        transform.position = nextCoord.pos; //Move pencil to the next position
        pencil.lastCoord = pencil.currentCoord; //Set the current position as the last position
        pencil.currentCoord = nextCoord; // Set the next coordinate as the current coordinate
        
    }

    //Stores the valid coordinates for drawing on the map
    private void SetValidCoordinates()
    {
        int urbanMinX = 0;
        int urbanMaxX = 7;
        int urbanMinY = 0;
        int urbanMaxY = 7;
        int suburbMinX = 4;
        int suburbMaxX = 11;
        int suburbMinY = -4;
        int suburbMaxY  = 3;

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
    private bool IsCoordValid(string coord)
    {
        if ((mapView.viewNb == 1 && urbanViewCoordinates.Contains(coord)) || (mapView.viewNb == 2 &&
            suburViewCoordinates.Contains(coord)) || (mapView.viewNb == 3 && validCoordinates.Contains(coord)))
            return true;
        else
            return false;
    }
    
}
