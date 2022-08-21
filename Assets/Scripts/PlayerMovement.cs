using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    CharacterController character;
    Vector3 moveVector;
    Vector3 rotateVector;


   
    InputActionAsset playerControls;
    [SerializeField] float speed = 6f;
    [SerializeField] float lookSpeed = 50f;
    float backStepForce = 2;
    float backStepDuration = .25f;
    float elapsedTime;
    public float moveInput;
    public float rotateInput;

    


    void Awake()
    {
        var cityActionMap = GetComponent<PlayerInput>().actions.FindActionMap("playerView");
        var mapActionMap = GetComponent<PlayerInput>().actions.FindActionMap("MapView");
        character = GetComponent<CharacterController>();
    }


    private void FixedUpdate()
    {
        //Character move
        if (moveInput > 0) // move forward
        {
            //Debug.Log("Forward");
            character.transform.Translate(Vector3.forward * moveInput * Time.deltaTime * speed);
        }
        else if (moveInput < 0) // move backward
        {
            //Debug.Log("Backward");
        }

        //Character rotate
        //rotateVector.y += rotateInput * Time.deltaTime * lookSpeed;
        //transform.eulerAngles = new Vector3(0, rotateVector.y, 0);
        character.transform.Rotate(rotateVector);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<float>();
        moveVector = new Vector3(moveInput, 0, 0);
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        rotateInput = context.ReadValue<float>();
        rotateVector = new Vector3(0,rotateInput, 0);
    }

    public void GotoCoordinate(string coordStr, CardinalDir dir)
    {

    }

}
