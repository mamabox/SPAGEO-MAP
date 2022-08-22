using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionsManager : MonoBehaviour
{

    [SerializeField] GameObject intersectionPrefab;
    [SerializeField] GameObject intersectionsParent;

    public Collider lastIntersectionCollider;
    public Coordinate lastIntersection;

    private float posY = 1f;
    public bool inIntersection;

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

        Coordinate _thisIntersection = other.gameObject.GetComponent<Intersection>().coord;
        CardinalDir _enteredFrom;

        //lastIntersectionCollider = other;
        inIntersection = true;
        if (GameManager.gameData.playerRoute.Count == 0) // IF the player has not started her route
        {
            //IF the player started in this intersection
            if (!PlayerMovement.hasMoved)
            {
                Debug.LogFormat("START in {0}", _thisIntersection.name);
                AddToPlayerRoute(_thisIntersection);
            }
            else
            {
                Debug.LogFormat("ENTER ({0})", _thisIntersection.name);
                AddToPlayerRoute(_thisIntersection);
                //TODO: Calculate directions
                //GameManager.gameData.playerRouteWithDir.Add(other.gameObject.GetComponent<Intersection>().coord.name);
                //GameManager.gameData.playerRouteCoordWithDir.Add(other.gameObject.GetComponent<Intersection>().coord);
            }
        }
        else // IF the player has already started her route
        {

            _enteredFrom = DirectionFromLastIntersection(lastIntersection, _thisIntersection);
            Debug.LogFormat("ENTER ({0}) from {1}", _thisIntersection.name, _enteredFrom.ToString());
            //TODO: IF player did not go trace back to the last intersection
            if (_thisIntersection.name != lastIntersection.name)
            {
                AddToPlayerRoute(_thisIntersection);
            }
            
        }

        lastIntersection = _thisIntersection; // Store the last intersection coordinate in the route
    }

    private void AddToPlayerRoute(Coordinate _coord)
    {
        GameManager.gameData.playerRoute.Add(_coord.name);
        GameManager.gameData.playerRouteCoord.Add(_coord);
        
    }

    // WHEN PLAYER LEAVES AN INTERSECTION
    public void OnIntersectionExit(Collider other) {
        Debug.LogFormat("EXIT ({0})", other.gameObject.GetComponent<Intersection>().coord.name);
        inIntersection = false;
    }

    //UNDONE: Move to RouteManager
    public void ConvertRouteToDirection()
    {

    }

    //UNDONE: Return cardinal direction
    public CardinalDir DirectionFromLastIntersection(Coordinate last, Coordinate current)
    {
        CardinalDir _cardDir;
        if (last.x - current.x > 0)
        {
            _cardDir = CardinalDir.W;
        }
        else if (last.x - current.x < 0)
        {
            _cardDir = CardinalDir.E;
        }
        else if (last.z - current.z > 0)
        {
            _cardDir = CardinalDir.S;
        }
        else
        {
            _cardDir = CardinalDir.N;
        }
        return _cardDir;
        
    }

    //UNDONE: 
    public bool CheckForQuarterTurn(CardinalDir last, CardinalDir now)
    {
        return true;
    }
}
