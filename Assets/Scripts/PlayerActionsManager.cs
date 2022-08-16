using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionsManager : MonoBehaviour
{
    [SerializeField] int scenario;
    private GameManager gameManager;
    private ScenarioManager scenarioManager;

    private bool drawingAllowed;
    Vector2 drawInput;  //player input for drawing

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        scenarioManager = GameObject.FindGameObjectWithTag("ScenarioManager").GetComponent<ScenarioManager>();
        scenario = gameManager.activeScenario;
    }

    // HANDLES PLAYER INPUT
    public void OnDrawInput(InputAction.CallbackContext context)
    {
        if (context.performed)
            Debug.Log("OnDrawInput.performed");
        else if (context.started)
            Debug.Log("OnDrawInput.started");
        else if (context.canceled)
            Debug.Log("OnDrawInput.canceled");

        if (context.performed && scenario == 12)
            {
                Vector2 drawInput = context.ReadValue<Vector2>();//player input for drawing
            Debug.Log("Context.x: " + drawInput.x + "Context.y: " + drawInput.y);
                scenarioManager.sc12.route.GetComponent<DrawRoute>().DrawInput(drawInput);
            }
    }
    

    

}
