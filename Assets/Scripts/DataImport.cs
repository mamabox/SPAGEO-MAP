using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataImport : MonoBehaviour
{
    
    private string importPath;
    private string gameDataFile;
    private string scenariosDataFile;

    void Awake()
    {
        SetImportPath();
        gameDataFile = importPath + "gamesettingsdata.json";
        scenariosDataFile = importPath + "scenariosData.json";
        
        ImportGameData(gameDataFile);
        ImportScenariosData(scenariosDataFile);
    }

    private void SetImportPath()
    {
        if (Application.platform == RuntimePlatform.Android)
            importPath = Path.Combine(Application.streamingAssetsPath, "/");
        else
            importPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets/Data/Import/");
    }

    //Import data from text file using JSON utilitu and save it to gameManager.scenariosData
    public void ImportGameData(string dataFile)
    {
        if (File.Exists(dataFile))
        {
            Debug.LogFormat("File {0} exists", dataFile);
            string fileContents = File.ReadAllText(dataFile); //Read the entire file and save its content
            //GameManager.gameSettingsData = JsonUtility.FromJson<GameSettingsData>(fileContents);
        }
        else
        {
            Debug.LogFormat("File {0} does not exist", dataFile);
        }
    }

    //Import data from text file using JSON utilitu and save it to gameManager.scenariosData
    public void ImportScenariosData(string dataFile)
    {
        if (File.Exists(dataFile))
        {
            Debug.LogFormat("File {0} exists", dataFile);
            string fileContents = File.ReadAllText(dataFile); //Read the entire file and save its content
            GameManager.scData = JsonUtility.FromJson<ScenariosData>(fileContents);

        }
        else
        {
            Debug.LogFormat("File {0} does not exist", dataFile);
        }

    }
}
