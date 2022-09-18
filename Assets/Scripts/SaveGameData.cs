using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveGameData : MonoBehaviour
{
    private string exportPath;
    private string fileName;
    private float recordingInterval = 1f; //sample rate in Hz (cycle per second)

    private string playerGameSummary;


    private StreamWriter sw;
    private char fileNameDelimiter ='-';
    private char delimiter = ',';

    void Awake()
    {
        SetExportPath();

    }

    private void SetExportPath()
    {
        if (Application.platform == RuntimePlatform.Android)
            exportPath = Path.Combine(Application.streamingAssetsPath, "/");
        else
            exportPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets/Data/Export/");

        if (!System.IO.Directory.Exists(exportPath))    //if save directory does not exist, create it
        {
            System.IO.Directory.CreateDirectory(exportPath);
        }

    }

    public void StartSavingData()
    {
        SetFileName();
        sw = File.AppendText(exportPath + fileName);
        sw.WriteLine(HeaderConstructor());
        InvokeRepeating("SaveData", 0, 1 / recordingInterval);

    }

    public void ContinueSavingData()
    {
        sw = File.AppendText(exportPath + fileName);
        InvokeRepeating("SaveData", 0, 1 / recordingInterval);
    }

    public string HeaderConstructor()
    {
        return "headers";
    }

    public void SaveData()
    {
        string time;

        time = TimeSpan.FromSeconds(Time.unscaledTime).ToString(@"mm\:ss");

        sw.WriteLine(time);
    }

    public void StopSavingData()
    {
        Debug.Log("Stop saving data");
        sw.Close();
        CancelInvoke();
    }

    private void SetFileName()
    {

        if (GameManager.playerData.isGroupSession)
        {
            playerGameSummary = string.Format("GRP{1}{0}SCN{2}{0}RTE{3}", fileNameDelimiter, GameManager.playerData.groupID, GameManager.gameData.scenario, GameManager.gameData.route);
        }
        else {
            playerGameSummary = string.Format("ELV{1}{0}SCN{2}{0}RTE{3}", fileNameDelimiter, GameManager.playerData.studentIDs[0], GameManager.gameData.scenario, GameManager.gameData.route);
        }

        fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + fileNameDelimiter + playerGameSummary + ".csv";
        GameManager.gameData.fileName = fileName;

    }

}
