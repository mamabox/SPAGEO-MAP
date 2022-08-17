using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    // Coordinates nitialized in DrawRoute.cs
    public Coordinate currentCoord; // Where the pencil is now
    public Coordinate startCoord;   // Where the route drawing began
    public Coordinate lastCoord;    // Last valid coordinate

    public List<string> routeAllPoints; // All coordinates in the route including backtracing
    public List<string> route;  // Coordinates of the current route in X_Y format


    //public GameObject startPoint;   
    //public GameObject endPoint;



    public LineRenderer lr; // Route rendered using List<string> route coordinates

    public string xyCoordSeparator = "_"; //TODO: Convert to CHAR
    float height = 41f;
    float multiplier = 35;

}
