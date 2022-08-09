using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

public class RouteDrawingController : MonoBehaviour
{
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject endPoint;
    [SerializeField] GameObject pencil;

    Vector2 drawInput;

    LineRenderer lr;
    RoutePointPrefab pencilInfo;
    float height = 41f;
    float multiplier = 35;
    List<string> coordinates = new List<string> { "0_0", "0_1", "0_2", "1_2" };
    List<Vector3> points = new List<Vector3>();
    Vector3[] pointsArray;
    public string xyCoordSeparator = "_"; //TODO: Convert to CHAR
    List<string> validCoordinates = new List<string>();
    private string startPos;
    private Vector3 startPosVector;

    //private Coordinate startCoord;
    //private Coordinate currentCoord;
    //private Coordinate lastCoord;



    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        pencilInfo = pencil.GetComponent<RoutePointPrefab>();
        startPos = "3_3";
        SetValidCoordinates();
        SetStartPoint();
        pointsArray = new Vector3[coordinates.Count];
        //Debug.Log("coord count: "+ coordinates.Count);
        //Debug.Log("count: " + pointsArray.Length);
        StringToPoints(coordinates);
        SetupLine(pointsArray);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    for (int i = 0; i < points.Length; i++)
    //    {
    //        lr.SetPosition(i, points[i].position);
    //    }
    //}

    private void SetStartPoint()
    {
        //startPos = "3_3";
        //string[] _coordString = startPos.Split(char.Parse(xyCoordSeparator));
        //float[] _coord = { float.Parse(_coordString[0]), float.Parse(_coordString[1])};
        //startPosVector = new Vector3(_coord[0] * multiplier, height, _coord[1] * multiplier);

        //Set pencil coordinates
        pencilInfo.startCoord = CreateCoordinate(startPos);
        pencilInfo.currentCoord = CreateCoordinate(startPos);
        pencilInfo.lastCoord = CreateCoordinate(startPos);

        //Move start point and drawing point
        startPoint.transform.position = pencilInfo.startCoord.pos;
        pencil.transform.position = pencilInfo.startCoord.pos;
        //point.SetActive(false);
        
    }

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

    public void SetupLine(Vector3[] points)
    {
        lr.positionCount = points.Length;
        lr.SetPositions(points);
    }

    public void OnDrawPath(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            drawInput = context.ReadValue<Vector2>();
            if (drawInput == new Vector2(0, 1))  //UP
            {
                Debug.Log("Draw UP");
                DrawRoute("UP");
            }
            else if (drawInput == new Vector2(0, -1)) //DOWN
            {
                Debug.Log("Draw DOWN");
                DrawRoute("DOWN");
            }
            else if (drawInput == new Vector2(-1, 0)) //LEFT
            {
                Debug.Log("Draw LEFT");
                DrawRoute("LEFT");
            }
            else if (drawInput == new Vector2(1, 0))  //RIGHT
            {
                Debug.Log("Draw RIGHT");
                DrawRoute("RIGHT");
            }

        }

    }

    public void DrawRoute(string direction)
    {
        Coordinate _nextCoord;
        string _nextCoordString = "";

        if (direction == "UP")
        {
            _nextCoordString = pencilInfo.currentCoord.x + xyCoordSeparator + (pencilInfo.currentCoord.z + 1);
        }
        else if (direction == "DOWN")
        {
            _nextCoordString = pencilInfo.currentCoord.x + xyCoordSeparator + (pencilInfo.currentCoord.z - 1);
        }
        else if (direction == "LEFT")
        {
            _nextCoordString = (pencilInfo.currentCoord.x - 1) + xyCoordSeparator + pencilInfo.currentCoord.z;
        }
        else if (direction == "RIGHT")
        {
            _nextCoordString = (pencilInfo.currentCoord.x + 1) + xyCoordSeparator + pencilInfo.currentCoord.z;
        }
        Debug.Log("Next coord string: " + _nextCoordString);
        _nextCoord = CreateCoordinate(_nextCoordString);
        MovePencil(_nextCoord);


    }

    private void MovePencil(Coordinate nextCoord)
    {
        //Debug.Log("_nextCoord pos: " + nextCoord.pos);
        pencil.transform.position = nextCoord.pos; //Move to the next position
        pencilInfo.lastCoord = pencilInfo.currentCoord; //Set the current position as the last position
        pencilInfo.currentCoord = nextCoord; // Set the next coordinate as the current coordinate
        pencilInfo.routeAllPoints.Add(nextCoord.name); // Add to the list of all coordinates
    }

    private void SetValidCoordinates()
    {
        int maxX = 5;
        int maxY  = 5;

        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                string _coord = x + xyCoordSeparator + y;
                validCoordinates.Add(_coord);

            }
        }
        Debug.Log("validCoordinates: " + string.Join(",", validCoordinates));
        Debug.Log("TEST (3,4): " + IsCoordValid("3_4"));
        Debug.Log("TEST (6,4): " + IsCoordValid("6_4"));
    }

    private bool IsCoordValid(string coord)
    {
        if (validCoordinates.Contains(coord))
            return true;
        else
            return false;
    }
    
}
