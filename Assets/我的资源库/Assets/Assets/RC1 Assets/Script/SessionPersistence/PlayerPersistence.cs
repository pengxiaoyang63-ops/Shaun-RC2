using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPersistence : MonoBehaviour, IDataPersistence
{

    public SaveManager saveManager;

    //Register objects
    void Awake()
    {
        saveManager.Register(this);
    }
    public void SaveData(SaveData savedata)
    {
        savedata.playerPosition = transform.position;
    }

    public void LoadData(SaveData savedata)
    {
        transform.position = savedata.playerPosition;
    }

}
