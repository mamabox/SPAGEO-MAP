using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection : MonoBehaviour
{
    public Coordinate coord;
    public CardinalDir inDir;
    public CardinalDir outDir;

    private void Awake()
    {
        //Singleton.Instance.coordinatesMngr.CreateCoordinate(coordString, posY);
    }
}
