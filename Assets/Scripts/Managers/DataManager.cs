using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //public DataImport dataImport;
    public SaveGameData saveGameData;


    void Awake()
    {
        //dataImport = GetComponent<DataImport>();
        saveGameData = GetComponent<SaveGameData>();
    }

    void Update()
    {
        
    }
}
