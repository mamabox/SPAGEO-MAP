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
    public int scenarioID;
    public string description;
    public string dropDownMenuText;
    public Instructions instructions;
    public List<POI> POIs;
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
    public Coordinate coord;
    public string name;
}