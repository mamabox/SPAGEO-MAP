using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionsManager : MonoBehaviour
{

    [SerializeField] GameObject intersectionPrefab;
    [SerializeField] GameObject intersectionsParent;

    public Collider lastIntersectionCollider;
    private Intersection lastIntersection;
    private Intersection thisIntersection;
    public Coordinate lastIntersectionCoord;

    CardinalDir _exitedFrom;
    CardinalDir _enteredFrom;
    CardinalDir _quarterTurn;

    private float posY = 1f;
    private float intersectionExitMargin = 5f;
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

        thisIntersection = other.gameObject.GetComponent<Intersection>();
        Coordinate _thisIntersectionCoord = other.gameObject.GetComponent<Intersection>().coord;
        //CardinalDir _quarterTurn; ;
        //CardinalDir _enteredFrom;

        //lastIntersectionCollider = other;
        inIntersection = true;
        // (A) Player has not started her route
        if (GameManager.gameData.playerRoute.Count == 0)
        {
            //(A1) Player started in this intersection
            if (!PlayerMovement.hasMoved)
            {
                Debug.LogFormat("START in {0}", _thisIntersectionCoord.name);
                _enteredFrom = DirectionFromLastIntersection(_thisIntersectionCoord);
                AddToPlayerRoute(_thisIntersectionCoord);
            }
            //(A2) Player started in a segment
            else
            {
                Debug.LogFormat("ENTER ({0})", _thisIntersectionCoord.name);
                _enteredFrom = Singleton.Instance.coordinatesMngr.OppositeCardDir(DirectionFromLastIntersection(_thisIntersectionCoord));
                //AddToPlayerRouteAll(_thisIntersection,_enteredFrom);
                AddToPlayerRoute(_thisIntersectionCoord);
                AddToPlayerRouteDir(_thisIntersectionCoord, _enteredFrom);
                AddToPlayerRouteDirLive(_thisIntersectionCoord, _enteredFrom);

            }
            thisIntersection.inDir = _enteredFrom;
            lastIntersectionCoord = _thisIntersectionCoord;// Store the last intersection coordinate in the route
        }
        // (B) the player has already started her route
        
        else
        {
            //TODO: IF player did not go trace back to the last intersection
            // (B1) Entering a new intersection
            if (_thisIntersectionCoord.name != lastIntersectionCoord.name)
            {
                _enteredFrom = DirectionFromLastIntersection(lastIntersectionCoord, _thisIntersectionCoord);
                thisIntersection.inDir = _enteredFrom;
                Debug.LogFormat("ENTER ({0}) heading {1}", _thisIntersectionCoord.name, _enteredFrom.ToString());

                //AddToPlayerRouteAll(_thisIntersection, _enteredFrom);
                AddToPlayerRoute(_thisIntersectionCoord);
                //if (GameManager.gameData.playerRoute.Count > 1) //if there is more than one intersection in the current route
                //{
                //if (GameManager.gameData.playerRouteWithDir.Count == 0) //if the list of direction is empty
                //{
                //    AddToPlayerRouteDir(lastIntersectionCoord, _enteredFrom);
                //}
                if (lastIntersection.outDir != lastIntersection.inDir) //fi change of direction in last intersection
                {
                    Debug.Log("Change of direction");
                    
                    //_quarterTurn = IntermediateTurn(lastIntersection.inDir, lastIntersection.outDir);
                    if (_quarterTurn != CardinalDir.X)
                    {
                        AddToPlayerRouteDir(lastIntersectionCoord, _quarterTurn);
                        //AddToPlayerRouteDirLive(lastIntersectionCoord, _quarterTurn);
                    }
                    AddToPlayerRouteDir(lastIntersectionCoord, _exitedFrom);
                }
                AddToPlayerRouteDir(_thisIntersectionCoord, _enteredFrom);
                AddToPlayerRouteDirLive(_thisIntersectionCoord, _enteredFrom);
                lastIntersectionCoord = _thisIntersectionCoord; // Store the last intersection coordinate in the route
            }
            // (B2) Re-entering the same intersection
            else
            {
                _enteredFrom = Singleton.Instance.coordinatesMngr.OppositeCardDir(_exitedFrom);
                Debug.LogFormat("RE-ENTER ({0}) heading {1}", _thisIntersectionCoord.name, _enteredFrom.ToString());
                // Delete the last DirLive unless it is the first one - to prevent deleting the first intersection enter

                //TODO: delete only if change of direction between lastintersection and current one
                if ((GameManager.gameData.playerRouteWithDirLive.Count > 1) && (thisIntersection.outDir != thisIntersection.inDir))
                { if (IntermediateTurn(thisIntersection.inDir, thisIntersection.outDir) != CardinalDir.X)//if made a quarter turn, delete one before last (quarter turn) coordinate
                    {
                        GameManager.gameData.playerRouteWithDirLive.RemoveAt(GameManager.gameData.playerRouteWithDirLive.Count - 2);
                    }

                        GameManager.gameData.playerRouteWithDirLive.RemoveAt(GameManager.gameData.playerRouteWithDirLive.Count - 1);
            }
                GameManager.gameData.playerRouteWithDirLiveAll.Add(_thisIntersectionCoord.name + _enteredFrom.ToString());
            }
            
        }

        
    }

    private void RemovePlayerRouteDirLive()
    {

    }

    private void AddToPlayerRoute(Coordinate _coord)
    {
        GameManager.gameData.playerRoute.Add(_coord.name);
        GameManager.gameData.playerRouteCoord.Add(_coord);  
    }

    private void RemoveFromPlayerRouteDir(Coordinate _coord)
    {
        GameManager.gameData.playerRouteWithDirLive.RemoveAt(GameManager.gameData.playerRouteWithDirLive.Count - 1);
        //GameManager.gameData.playerRouteWithDirCoord.RemoveAt(GameManager.gameData.playerRoute.Count - 1);
    }

    private void AddToPlayerRouteAll(Coordinate _coord, CardinalDir dir)
    {
        AddToPlayerRoute(_coord);
        //if (GameManager.gameData.playerRoute.Count > 1) //if there is more than one intersection in the current route
        //{
        if (lastIntersection.inDir != thisIntersection.inDir) //fi change of direction
        {
            AddToPlayerRouteDir(lastIntersectionCoord, dir);
        }
        AddToPlayerRouteDir(_coord, dir);
        AddToPlayerRouteDirLive(_coord, dir);

    }

    private void AddToPlayerRouteDir(Coordinate _coord, CardinalDir dir)
    {
        //To avoid repeat, only add the direction if it's different (there was a turn)
        string _temp = _coord.name + dir.ToString();


            GameManager.gameData.playerRouteWithDir.Add(_coord.name + dir.ToString());
        

    }

    private void AddToPlayerRouteDirLive(Coordinate _coord, CardinalDir dir)
    {
        //To avoid repeat, only add the direction if it's different (there was a turn)
        string _temp = _coord.name + dir.ToString();

            GameManager.gameData.playerRouteWithDirLive.Add(_coord.name + dir.ToString());
            GameManager.gameData.playerRouteWithDirLiveAll.Add(_coord.name + dir.ToString());
       

    }

    // WHEN PLAYER LEAVES AN INTERSECTION
    public void OnIntersectionExit(Collider other)
    {
        Coordinate _thisIntersection = other.gameObject.GetComponent<Intersection>().coord;
        //CardinalDir _quarterTurn;

        _exitedFrom = DirectionFromLastIntersection(lastIntersectionCoord);
        lastIntersection = other.gameObject.GetComponent<Intersection>();
        lastIntersection.outDir = _exitedFrom;
        //TODO: Get start orientation from 
        if (GameManager.gameData.playerRouteWithDir.Count == 0)
        {
            Debug.LogFormat("EXIT ({0}", _thisIntersection.name);
            AddToPlayerRouteDir(_thisIntersection, _exitedFrom);
            AddToPlayerRouteDirLive(_thisIntersection, _exitedFrom);
            lastIntersection.inDir = _exitedFrom; //Initialise if
        }
        else
        {
            //_exitedFrom = DirectionFromLastIntersection(lastIntersectionCoord);
          
            Debug.LogFormat("EXIT ({0} heading {1})", _thisIntersection.name, _exitedFrom.ToString());
            //if (GameManager.gameData.playerRouteWithDir.Count == 0) // if started within intersection
            //{
            //    AddToPlayerRouteDir(lastIntersection, _exitedFrom);
            //}


            // if not exiting from the samme direciton I entered in
            if (thisIntersection.inDir != thisIntersection.outDir)
            {
                //If there was a quarter turn
                _quarterTurn = IntermediateTurn(thisIntersection.inDir, thisIntersection.outDir);
                if ( GameManager.gameData.playerRouteWithDir.Count != 0 && _quarterTurn != CardinalDir.X) //check for RD lenght
                {
                    AddToPlayerRouteDirLive(_thisIntersection, _quarterTurn);
                }
                AddToPlayerRouteDirLive(_thisIntersection, _exitedFrom);
            }

            
        }


        inIntersection = false;
    }

    //UNDONE: Move to RouteManager
    public void ConvertRouteToDirection()
    {

    }

    //UNDONE: Return cardinal direction
    // Calculates the direction in which an interseciton was entered by comparing the last entered intersection and the current intersection. e.g. If player entered from teh EAST,they were going WEST
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

    public CardinalDir IntermediateTurn(CardinalDir inDir, CardinalDir outDir)
    {
        CardinalDir _quarterTurn = CardinalDir.X;

        if ((inDir == CardinalDir.N && outDir == CardinalDir.E) || (inDir == CardinalDir.E && outDir == CardinalDir.N))
            _quarterTurn = CardinalDir.NE;
        else if ((inDir == CardinalDir.S && outDir == CardinalDir.E) || (inDir == CardinalDir.E && outDir == CardinalDir.S))
            _quarterTurn = CardinalDir.SE;
        else if ((inDir == CardinalDir.N && outDir == CardinalDir.W) || (inDir == CardinalDir.W && outDir == CardinalDir.N))
            _quarterTurn = CardinalDir.NW;
        else if ((inDir == CardinalDir.S && outDir == CardinalDir.W) || (inDir == CardinalDir.W && outDir == CardinalDir.S))
            _quarterTurn = CardinalDir.SW;
        return _quarterTurn;
    }

    // Calculates the direction in which an intersection was entered by comparing the last entered intersection and the current position
    public CardinalDir DirectionFromLastIntersection(Coordinate last)
    {
        CardinalDir _cardDir;
        if (last.pos.x - GameManager.player.transform.position.x > intersectionExitMargin)
        {
            _cardDir = CardinalDir.W;
        }
        else if (last.pos.x - GameManager.player.transform.position.x < -intersectionExitMargin)
        {
            _cardDir = CardinalDir.E;
        }
        else if (last.pos.z - GameManager.player.transform.position.z > intersectionExitMargin)
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
