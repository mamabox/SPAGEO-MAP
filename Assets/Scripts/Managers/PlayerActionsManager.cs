using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Handles Player action input for map and player view
*/

public class PlayerActionsManager : MonoBehaviour
{
    private GameManager gameManager;
    private ScenarioManager scenarioManager;

    private bool drawingAllowed;
    Vector2 drawInput;  //player input for drawing

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        scenarioManager = GameObject.FindGameObjectWithTag("ScenarioManager").GetComponent<ScenarioManager>();
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
            switch (Singleton.Instance.gameMngr.activeScenario)
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
            if (gameManager.activeScenario == 12)
            {
                scenarioManager.sc12.route.GetComponent<DrawRoute>().ValidateRoute();
            }
        }

    }

    public void OnNewAttempt(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (Singleton.Instance.gameMngr.activeScenario)
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
        if (gameManager.activeScenario == 10)
            scenarioManager.sc10.EndScenario();
        else if (gameManager.activeScenario == 11)
            scenarioManager.sc11.EndScenario();
        else if (gameManager.activeScenario == 12)
            scenarioManager.sc12.EndScenario();

        gameManager.activeScenario = scenario;

        if (scenario == 10)
            {
                scenarioManager.sc10.StartScenario();
            
            }
            else if (scenario == 11)
            {

            scenarioManager.sc11.StartScenario();
            }
            else if (scenario == 12)
            {

            scenarioManager.sc12.StartScenario();
            }
        gameManager.activeScenario = scenario;
    }

    public void OnSubmitAnswer()
    {
        Debug.Log("Submit answer");
    }


}