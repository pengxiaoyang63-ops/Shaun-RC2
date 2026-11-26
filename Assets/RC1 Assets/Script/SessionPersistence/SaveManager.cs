using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections;

public class SaveManager : MonoBehaviour
{

    public static event Action OnSaveDataLoaded;
    
    private readonly List<IDataPersistence> dataPersistenceObjects = new();
    private string SavePath => Path.Combine(Application.dataPath, "..", "save.json");

    // Register objects that implement the IDataPersistence interface
    public void Register(IDataPersistence dataPersistence)
    {
        dataPersistenceObjects.Add(dataPersistence);
    }

    void Start()
    {
        StartCoroutine(LoadAfterInit());
    }

    private void Update()
    {
        ManualSave();
        ManualLoad();
    }

    // Load after initialization
    private IEnumerator LoadAfterInit()
    {
        yield return null;
        LoadData();
    }

    // Save
    public void SaveData()
    {
        SaveData saveData = new();

        foreach (var obj in dataPersistenceObjects)
            obj.SaveData(saveData);
        

        string json = JsonUtility.ToJson(saveData, true); //true makes it pretty printed
        File.WriteAllText(SavePath, json);

        Debug.Log($"Saved data to {SavePath}");

    }

    // Load
    public void LoadData()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log($"Save file not found in {SavePath}");
            return;
        }

        string json = File.ReadAllText(SavePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        foreach (var obj in dataPersistenceObjects)
            obj.LoadData(data);

        OnSaveDataLoaded?.Invoke();

        Debug.Log($"Loaded data from {SavePath}");
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void ManualSave()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveData();

            Debug.Log("Manually Saved");
        }
    }

    private void ManualLoad()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            LoadData();

            Debug.Log("Manually Loaded");
        }
    }
}
