using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerData
{
    public bool isGroupSession;
    public string groupID;
    public List<string> studentIDs;


    public PlayerData(bool _isGroup, string _groupID, List<string> _studentIDs)
    {
        isGroupSession = _isGroup;
        groupID = _groupID;
        studentIDs = _studentIDs;

    }

}
