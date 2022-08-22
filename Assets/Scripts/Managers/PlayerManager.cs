using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static Vector3 startCoord;
    public static Vector3 startRotation;
    public static Vector3 lastPosition;

    [SerializeField] Vector3 rotation;

    public static bool tookStep;
    public static bool hasMoved;
}
