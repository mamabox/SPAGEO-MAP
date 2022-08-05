using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RouteDrawingController : MonoBehaviour
{
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject endPoint;
    [SerializeField] GameObject point;

    LineRenderer lr;
    float height = 41f;
    float multiplier = 35;
    List<string> coordinates = new List<string> { "0_0", "0_1", "0_2", "1_2" };
    List<Vector3> points = new List<Vector3>();
    Vector3[] pointsArray;
    public string xyCoordSeparator = "_"; //TODO: Convert to CHAR


    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();

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

}
