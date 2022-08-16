using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    public Coordinate currentCoord;
    public Coordinate startCoord;
    public Coordinate lastCoord;
    public List<string> routeAllPoints;
    public List<string> route;

    //public GameObject startPointPrefab;
    //public GameObject endPointPrefab;

    public GameObject startPoint;
    public GameObject endPoint;



    public LineRenderer lr;

    public string xyCoordSeparator = "_"; //TODO: Convert to CHAR
    float height = 41f;
    float multiplier = 35;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
   
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void SetStartPoint( string startPos)
    //{
    //    startPoint = Instantiate(startPointPrefab);
    //    startPoint.name = "NEW STARTPT";
    //    endPoint = Instantiate(endPointPrefab);
    //    endPoint.name = "NEW ENDPT";
    //    endPoint.SetActive(false);

    //    //Initialise pencil coordinates / information
    //    startCoord = CreateCoordinate(startPos);
    //    currentCoord = CreateCoordinate(startPos);
    //    lastCoord = CreateCoordinate(startPos);
    //    routeAllPoints.Add(startPos);
    //    route.Add(startPos);

    //    //Move start point and drawing point
    //    startPoint.transform.position = startCoord.pos;
    //    transform.position = startCoord.pos;

    //    //TODO: SetActive for objects depending on drawingAllowed
    //    //point.SetActive(false);

    //    //Line renderer setup
    //    lr.SetPosition(0, startCoord.pos);

    //}

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
}
