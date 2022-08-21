using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Handles Player action input for map and player view
*/

public class PlayerActionsManager : MonoBehaviour
{
    private void Awake()
    {
    }

    // (1) BOTH PLAYER AND MAP VIEW

    // (2) PLAYER VIEW ONLY

    // (3) MAP VIEW ONLY
    public void OnDropDeletePin(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Singleton.Instance.operationsMngr.dropPin.DropDeletePin();
        }
    }

    public void OnDrawInput(InputAction.CallbackContext context)
    {
        //Debug.Log("Draw");
        //if (context.performed)
        //    Debug.Log("OnDrawInput.performed");
        //else if (context.started)
        //    Debug.Log("OnDrawInput.started");
        //else if (context.canceled)
        //    Debug.Log("OnDrawInput.canceled");

        if (context.performed)
        {
            //Debug.Log("DrawPerformed");
            switch (GameManager.activeScenario)
            {
                case 12:
                    Vector2 drawInput = context.ReadValue<Vector2>(); //player input for drawing
                    //Debug.Log("Context.x: " + drawInput.x + "Context.y: " + drawInput.y);
                    Singleton.Instance.scenarioMngr.sc12.route.GetComponent<DrawRoute>().DrawInput(drawInput);
                    break;
            }

            }
    }

    public void OnValidateRoute(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (GameManager.activeScenario == 12)
            {
                Singleton.Instance.scenarioMngr.sc12.route.GetComponent<DrawRoute>().ValidateRoute();
            }
        }

    }

    public void OnNewAttempt(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (GameManager.activeScenario)
            {
                case 11:
                    Singleton.Instance.operationsMngr.dropPin.DeleteAllPins();
                    break;

                case 12:
                    Singleton.Instance.scenarioMngr.sc12.route.GetComponent<DrawRoute>().ResetLine();
                    break;
            }
        }
    }

    // (4) DEBUG

    public void OnSwitchScenario(int scenario)
    {
        Singleton.Instance.scenarioMngr.SwitchToScenario(scenario);
    }

    public void OnSubmitAnswer()
    {
        Debug.Log("Submit answer");
    }


}
