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
}
