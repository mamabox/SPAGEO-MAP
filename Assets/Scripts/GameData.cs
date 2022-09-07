using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameData
{
    public int scenario;
    public int route;
    public List<Coordinate> routeCoord;
    public List<string> playerRoute;    // Route the player has taken as string
    public List<Coordinate> playerRouteCoord; // Route the player has taken as cooridinates
    // Route the player has taken as string, used to generate IMG string
    public List<string> playerRouteWithDir; // Route version used in 4P and 5P
    public List<string> playerRouteWithDirLive; // Route the player has taken as string, used to generate IMG string
    public List<string> playerRouteWithDirLiveAll;  // This new route includes re-entering and exiting intersections

    public GameData(int _scenario, int _route)
    {
        scenario = _scenario;
        route = _route;
        routeCoord = new List<Coordinate>();
        playerRoute = new List<string>();
        playerRouteCoord = new List<Coordinate>();
        playerRouteWithDir = new List<string>();
        playerRouteWithDirLive = new List < string>();
        playerRouteWithDirLiveAll = new List<string>();
    }
}
