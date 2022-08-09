using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

public class DrawRoute : MonoBehaviour
{
    [SerializeField] GameObject startPointPrefab;
    [SerializeField] GameObject endPointPrefab;
    [SerializeField] GameObject pencilPrefab;

    GameObject pencilDot;
    GameObject startPoint;
    GameObject endPoint;

    Vector2 drawInput;  //player input for drawing

    LineRenderer lr;
    Pencil pencil;
    float height = 41f;
    float multiplier = 35;
    List<string> coordinates = new List<string> { "0_0", "0_1", "0_2", "1_2" }; // Test line to draw
    List<Vector3> points = new List<Vector3>();
    Vector3[] pointsArray;
    public string xyCoordSeparator = "_"; //TODO: Convert to CHAR
    List<string> urbanCoordinates = new List<string>();
    List<string> suburbCoordinates = new List<string>();
    List<string> validCoordinates = new List<string>();
    private string startPos;
    private Vector3 startPosVector;

   
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate Prefabs
        pencilDot = Instantiate(pencilPrefab);
        startPoint = Instantiate(startPointPrefab);

        lr = GetComponent<LineRenderer>();


        pencil = pencilDot.GetComponent<Pencil>();
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
        pencil.startCoord = CreateCoordinate(startPos);
        pencil.currentCoord = CreateCoordinate(startPos);
        pencil.lastCoord = CreateCoordinate(startPos);

        //Move start point and drawing point
        startPoint.transform.position = pencil.startCoord.pos;
        pencilDot.transform.position = pencil.startCoord.pos;
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

    
    public void OnDrawInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            drawInput = context.ReadValue<Vector2>();
            if (drawInput == new Vector2(0, 1))  //UP
            {
                Debug.Log("Draw UP");
                RenderLine("UP");
            }
            else if (drawInput == new Vector2(0, -1)) //DOWN
            {
                Debug.Log("Draw DOWN");
                RenderLine("DOWN");
            }
            else if (drawInput == new Vector2(-1, 0)) //LEFT
            {
                Debug.Log("Draw LEFT");
                RenderLine("LEFT");
            }
            else if (drawInput == new Vector2(1, 0))  //RIGHT
            {
                Debug.Log("Draw RIGHT");
                RenderLine("RIGHT");
            }
        }
    }

    public void RenderLine(string direction)
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
            MovePencil(_nextCoord);


    }

    private void MovePencil(Coordinate nextCoord)
    {
        //Debug.Log("_nextCoord pos: " + nextCoord.pos);
        pencilDot.transform.position = nextCoord.pos; //Move to the next position
        pencil.lastCoord = pencil.currentCoord; //Set the current position as the last position
        pencil.currentCoord = nextCoord; // Set the next coordinate as the current coordinate
        pencil.routeAllPoints.Add(nextCoord.name); // Add to the list of all coordinates
    }

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
                urbanCoordinates.Add(_coord);
            }
        }

        // Subburb coordinates
        for (int x = suburbMinX; x <= suburbMaxX; x++)
        {
            for (int y = suburbMinY; y <= suburbMaxY; y++)
            {
                string _coord = x + xyCoordSeparator + y;
                suburbCoordinates.Add(_coord);
            }
        }

        validCoordinates = urbanCoordinates.Union(suburbCoordinates).ToList(); //Union of both sets of coordinates without duplicates

        //Debug.Log("validCoordinates: " + urbanCoordinates.Count+ "|" + suburbCoordinates.Count + "|"+ validCoordinates.Count + "): " + string.Join(",", validCoordinates));
        //Debug.Log("TEST (3,4): " + IsCoordValid("3_4"));
        //Debug.Log("TEST (-3,2): " + IsCoordValid("6_4"));
    }

    private bool IsCoordValid(string coord)
    {
        if (validCoordinates.Contains(coord))
            return true;
        else
            return false;
    }
    
}
