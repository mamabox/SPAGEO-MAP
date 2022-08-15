using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private bool drawingAllowed;
    Vector2 drawInput;  //player input for drawing

    private void Awake()
    {
        drawingAllowed = true;
    }

    private void Start()
    {
        
    }


}
