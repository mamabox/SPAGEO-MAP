using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationsManager : MonoBehaviour
{
    public DrawRoute drawRoute;
    public DropPin dropPin;

    public bool dropPinEnabled;

    private void Awake()
    {
        drawRoute = GetComponentInChildren<DrawRoute>();
        dropPin = GetComponentInChildren<DropPin>();

    }

}
