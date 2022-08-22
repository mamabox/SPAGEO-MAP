using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameData
{
    public int scenario;
    public int route;
    public List<Coordinate> routeCoord;
    public List<string> playerRoute;
    public List<string> playerRouteWithDir;

    public GameData(int _scenario, int _route)
    {
        scenario = _scenario;
        route = _route;
        routeCoord = new List<Coordinate>();
        playerRoute = new List<string>();
        playerRouteWithDir = new List < string>();
    }
}
