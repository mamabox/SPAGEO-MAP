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
    public List<string> playerRouteWithDir; // Route the player has taken as string, used to generate IMG string
    public List<string> playerRouteWithDirNew;  // This new route includes re-entering and exiting interscions

    public GameData(int _scenario, int _route)
    {
        scenario = _scenario;
        route = _route;
        routeCoord = new List<Coordinate>();
        playerRoute = new List<string>();
        playerRouteCoord = new List<Coordinate>();
        playerRouteWithDir = new List < string>();
        playerRouteWithDirNew = new List<string>();
    }
}
