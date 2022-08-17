using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

/*
 * Draws lines in Map view
 * 
 * */

public class DrawRoute : MonoBehaviour
{

    private GameObject camManagerObj;
    private CoordinatesManager coordManager;
    private ScenarioManager scenarioManager;
    private GameManager gameManager;

    MapView mapView;

    public GameObject pencilObj;
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject endPoint;

    

    [SerializeField] Vector2 drawInput;  //player input for drawing

    LineRenderer lr;
    private Pencil pencil;
  

    float height = 41f;
    float multiplier = 35;

    // USED FOR TESTING 
    List<string> testCoordinates = new List<string> { "0_0", "0_1", "0_2", "1_2" }; // Test line to draw
    List<Vector3> points = new List<Vector3>();
    Vector3[] pointsArray;
    private string startPos;    // FOR TESTING
    private Vector3 startPosVector;

    public string xyCoordSeparator = "_"; //TODO: Convert to CHAR

    List<string> urbanViewCoordinates = new List<string>();
    List<string> suburViewCoordinates = new List<string>();
    List<string> validCoordinates = new List<string>();

    public bool drawingAllowed;    // Is drawing allowed during this scenario

    private void Awake()
    {
        //Debug.Log("game object attached to: " + this.name);
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        camManagerObj = GameObject.FindGameObjectWithTag("CameraManager");
        coordManager = GameObject.FindGameObjectWithTag("CoordinatesManager").GetComponent<CoordinatesManager>();
        mapView = camManagerObj.GetComponent<MapView>();
        scenarioManager = GameObject.FindGameObjectWithTag("ScenarioManager").GetComponent<ScenarioManager>();

        pencil = pencilObj.GetComponent<Pencil>();
        lr = pencilObj.GetComponent<LineRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        pointsArray = new Vector3[testCoordinates.Count];
        StringToPoints(testCoordinates);
        //SetupLine(pointsArray);
    }

    //Called from scenario 12
    public void SetStartPoint(string pos)
    {
        startPos = pos; //Stores the start position for a reset
        //Initialise pencil coordinates / information
        pencil.startCoord = CreateCoordinate(pos);
        pencil.currentCoord = CreateCoordinate(pos);
        pencil.lastCoord = CreateCoordinate(pos);
        pencil.routeAllPoints.Add(pos);
        pencil.route.Add(pos);

        //Move pencil dot and start point
        pencil.transform.position = pencil.startCoord.pos;
        startPoint.transform.position = pencil.startCoord.pos;
        endPoint.transform.position = pencil.startCoord.pos;
        endPoint.SetActive(false);  //hide end point

        //TODO: SetActive for objects depending on drawingAllowed
        //point.SetActive(false);

        //Line renderer setup
        lr.SetPosition(0, pencil.startCoord.pos);

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

    public void DrawInput(Vector2 drawInput)
    {
        if (scenarioManager.drawingAllowed)
        {
            if (drawInput == new Vector2(0, 1))  //UP
            {
                //Debug.Log("Draw UP");
                RenderLineFromInput("UP");
            }
            else if (drawInput == new Vector2(0, -1)) //DOWN
            {
                //Debug.Log("Draw DOWN");
                RenderLineFromInput("DOWN");
            }
            else if (drawInput == new Vector2(-1, 0)) //LEFT
            {
                //Debug.Log("Draw LEFT");
                RenderLineFromInput("LEFT");
            }
            else if (drawInput == new Vector2(1, 0))  //RIGHT
            {
                //Debug.Log("Draw RIGHT");
                RenderLineFromInput("RIGHT");
            }
        }
    }

    public void ResetLine()
    {
        lr.positionCount = 1;   //remove all points

        //Delete routes
        pencil.routeAllPoints.Clear();
        pencil.route.Clear();

        SetStartPoint(startPos);    //Reset the start postion
        scenarioManager.drawingAllowed = true;  // Allow drawing
    }

    public void ValidateRoute()
    {
        endPoint.transform.position = pencil.transform.position; //Move the endPoint to current pencil position
                                                                 //pencil.endPoint.transform.position = pencil.currentCoord.pos;
        endPoint.SetActive(true);
        //Debug.Log("endPoint position = " + pencil.endPoint.transform.position.x);

        scenarioManager.drawingAllowed = false;
        //TODO: SAVE ROUTE

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
        //Debug.Log("Next coord string: " + _nextCoordString);
        _nextCoord = CreateCoordinate(_nextCoordString);

        // If the next coordinate is valid, move the pencil
        if (coordManager.IsCoordValid(_nextCoord.name))
            DrawNextPosition(_nextCoord);
    }

    public void OnDeleteLine(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            lr.positionCount = 1;   //remove all points

            //Delete routes
            pencil.routeAllPoints.Clear();
            pencil.route.Clear();

            //Delete children (start and end point)
            //Destroy(pencil.startPoint);
            //Destroy(pencil.endPoint);
   

            SetStartPoint(startPos);
            drawingAllowed = true;
        }
    }

    // Sets the end point to the current pencil position and disables further drawing


    // 1. Adds or delete coordinates to the list of route coordinates
    // 2. Adds or deletes points to the pencil's line renderer
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

        pencil.transform.position = nextCoord.pos; //Move pencil to the next position
        pencil.lastCoord = pencil.currentCoord; //Set the current position as the last position
        pencil.currentCoord = nextCoord; // Set the next coordinate as the current coordinate
        
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

    //TODO: TO delete, using OnDrawInput instead
    public void DrawInput2(Vector2 drawInput)
    {
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
