using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionsManager : MonoBehaviour
{

    [SerializeField] GameObject intersectionPrefab;
    [SerializeField] GameObject intersectionsParent;

    public Collider lastIntersectionCollider;

    private float posY = 1f;
    public bool inInterseciton;

    void Start()
    {
        GenerateIntersections();
    }

    private void GenerateIntersections()
    {
        foreach (string coord in Singleton.Instance.coordinatesMngr.validCoordinates)
        {
            InstantiatePrefab(coord);
        }
    }

    //UNDONE: Finish instantiating the prefabs
    private void InstantiatePrefab(string coord)
    {
        Coordinate _coord = Singleton.Instance.coordinatesMngr.CreateCoordinate(coord, posY);

        GameObject _intersection = Instantiate(intersectionPrefab, _coord.pos, Quaternion.identity, intersectionsParent.transform);
        _intersection.name = _coord.name;   //name the new intersction
        _intersection.GetComponent<Intersection>().coord = _coord;  // assigne a coordinate to the intersection
    }

    // WHEN PLAYER ENTERS AN INTERSECTION
    public void OnIntersectionEnter(Collider other) {
        Debug.LogFormat("ENTER ({0})", other.gameObject.GetComponent<Intersection>().coord.name);
        lastIntersectionCollider = other;
        inInterseciton = true;
        if (GameManager.gameData.playerRoute.Count == 0) // IF the player has not started her route
        {
            if (!PlayerMovement.hasMoved)
            {
                Debug.Log("Started in intersection");
                AddToPlayerRoute(other.gameObject.GetComponent<Intersection>().coord);
            }
            else
            {
                AddToPlayerRoute(other.gameObject.GetComponent<Intersection>().coord);
                //TODO: Calculate directions
                //GameManager.gameData.playerRouteWithDir.Add(other.gameObject.GetComponent<Intersection>().coord.name);
                //GameManager.gameData.playerRouteCoordWithDir.Add(other.gameObject.GetComponent<Intersection>().coord);
            }
        }
        else // IF the player has already started her route
        {
            //TODO: Check to see if intersection is same as last
            AddToPlayerRoute(other.gameObject.GetComponent<Intersection>().coord);
        }
    }

    private void AddToPlayerRoute(Coordinate _coord)
    {
        GameManager.gameData.playerRoute.Add(_coord.name);
        GameManager.gameData.playerRouteCoord.Add(_coord);
    }

    // WHEN PLAYER LEAVES AN INTERSECTION
    public void OnIntersectionExit(Collider other) {
        Debug.LogFormat("EXIT ({0})", other.gameObject.GetComponent<Intersection>().coord.name);
        inInterseciton = false;
    }

    //UNDONE: Move to RouteManager
    public void ConvertRouteToDirection()
    {

    }

    //UNDONE: Return cardinal direction
    public CardinalDir DirectionFromLastIntersection(Coordinate last, Coordinate current)
    {
        CardinalDir _cardDir = CardinalDir.N;

        return _cardDir;
    }

    //UNDONE: 
    public bool CheckForQuarterTurn(CardinalDir last, CardinalDir now)
    {
        return true;
    }
}
