using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    void SaveData(SaveData savedata);
    void LoadData(SaveData savedata);
}
