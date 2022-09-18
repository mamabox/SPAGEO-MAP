using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameData gameData; //Information saved in scenario menu
    public static PlayerData playerData; //Information saved in scenario menu
    public static ScenariosData scData;
    //public static GameSettings gameSettings;
    public static GameObject player;

    public static bool started;
    public static bool ended;
    public static bool isPaused;

    //USED FOR TESTING
    List<string> testList = new List<string>() { "0" };

    

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //Initialise variables
        started = false;
        ended = false;
        started = false;
    }

    void Start()
    {
        // TEST configuration
        playerData = new PlayerData(false, "groupIDtest", testList); //This should happen from the menu
        gameData = new GameData(10, 0); //This should happen from the menu
       
        started = true;
        // FOR TESTING
        Singleton.Instance.scenarioMngr.sc10.StartScenario();
        Debug.Log("IMPORT TEST " + scData.sc10Data.routes[0].POIs[1].name);
        Singleton.Instance.dataMngr.saveGameData.StartSavingData();
        Singleton.Instance.dataMngr.saveGameData.StopSavingData();
    }

    
    void Update()
    {
        
    }

}
