using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScenariosData
{
    public int scenariosNb;
    public Sc10Data sc10Data;

}

[System.Serializable]
public class Sc10Data
{
    public ScInfo info;
    public ScSettings scSettings;
    public List<Sc10Routes> routes;
}

[System.Serializable]
public class Sc10Routes
{
    public MapSettings mapSettings;
    public List<POI> POIs;
}

[System.Serializable]
public class RouteInfo
{
    public string description;
    public string dropDownMenuText;
}

    [System.Serializable]
public class ScInfo
{
    public int scenarioID;
    public string description;
    public string dropDownMenuText;
    public Instructions instructions;
}

[System.Serializable]
public class ScSettings
{
    public int attemptsNb;
    public string validation;  //none, image or map
    public int validationNb;
    public bool receiverTransmitter;
}

[System.Serializable]
public class MapSettings
{
    public int mapNb;
    public bool showStart;
    public bool showPlayer;
    public bool showPlayerRot;
    public bool limitViews;
    public int viewsAllowed;
    public bool limitTime;
    public float timeAllowed;  
}

[System.Serializable]
public class Instructions
{
    public string start;
    public string end;
    public List<string> attempts;
}

[System.Serializable]
public class POI
{
    public string name;
    public string coord;
}