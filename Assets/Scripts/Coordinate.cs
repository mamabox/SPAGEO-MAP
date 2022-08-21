using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Coordinate
{
        public string name;
        public float x;
        public float z;
        public string carDir;
        public Vector3 pos;
        public CardinalDir cardinalDir; // In what cardinal direction should the player point if moved back to this coordinate

    public enum CardinalDir
    {
        N,NE,E,SE,S,SW,W,NW
    }

    //TODO: rename to public Coordinate
    public void CoordinateConstructor(string coord, float posY)
    {
        
        string[] _coordString = coord.Split(char.Parse(CoordinatesManager.xyCoordSeparator));
        float[] _coordFloat = { float.Parse(_coordString[0]), float.Parse(_coordString[1]) };
        Vector3 _pos = new Vector3(_coordFloat[0] * EnvironmentManager.blockSize, posY, _coordFloat[1] * EnvironmentManager.blockSize);

        name = coord;
        x = float.Parse(_coordString[0]);
        z = float.Parse(_coordString[1]);
        pos = new Vector3(x * EnvironmentManager.blockSize, posY, z * EnvironmentManager.blockSize);

    }
}
