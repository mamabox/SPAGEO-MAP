using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinate
{
        public string name;
        public float x;
        public float z;
        public string carDir;
        public Vector3 pos;
        public CarDir cardinalDir;

    public enum CarDir
    {
        N,NE,E,SE,S,SW,W,NW
    }

    //TODO: set and get from environment manager
    int multiplier;
    string xyCoordSeparator;

    //TODO: rename to public Coordinate
    public void CoordinateConstructor (string coord, int posY)
    {
        
        string[] _coordString = coord.Split(char.Parse(xyCoordSeparator));
        float[] _coordFloat = { float.Parse(_coordString[0]), float.Parse(_coordString[1]) };
        Vector3 _pos = new Vector3(_coordFloat[0] * multiplier, posY, _coordFloat[1] * multiplier);

        name = coord;
        x = float.Parse(_coordString[0]);
        z = float.Parse(_coordString[1]);
        pos = new Vector3(x * multiplier, posY, z * multiplier);

    }

    ////Creates a Coordinate from a string in format "X_Y"
    //private Coordinate CreateCoordinate(string coord)
    //{
    //    Coordinate _coord = new Coordinate();
    //    string[] _coordString = coord.Split(char.Parse(xyCoordSeparator));
    //    float[] _coordFloat = { float.Parse(_coordString[0]), float.Parse(_coordString[1]) };
    //    Vector3 _pos = new Vector3(_coordFloat[0] * multiplier, height, _coordFloat[1] * multiplier);

    //    _coord.name = coord;
    //    _coord.x = float.Parse(_coordString[0]);
    //    _coord.z = float.Parse(_coordString[1]);
    //    _coord.pos = new Vector3(_coord.x * multiplier, height, _coord.z * multiplier);

    //    return _coord;
    //}
}
