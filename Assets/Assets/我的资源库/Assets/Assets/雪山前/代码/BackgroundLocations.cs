using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackgroundLoader
{
    public string Background;    
    public string Backgroundpath;
}

public class BackgroundLocations : MonoBehaviour
{
    public List<BackgroundLoader> BackgroundLoadingList = new List<BackgroundLoader>();
    public static BackgroundLocations Instance;
    public TextAsset BackgroundLocation;
    [ContextMenu("Load from CSV")]
    public void LoadFromCsv()
    {
        if (BackgroundLocation == null)
        {
            Debug.LogError("Can't find BackgroundCSV");
            return;
        }

        BackgroundLoadingList.Clear();

        string csvText = BackgroundLocation.text;
        
        string[] lines = csvText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            if (line.Contains("Illustration")) 
            {
                continue;
            }
            string[] cols = line.Split(',');

            BackgroundLoader row = new BackgroundLoader();
            row.Background = cols[0].Trim();
            row.Backgroundpath = cols[1].Trim();

            BackgroundLoadingList.Add(row);
        }

        Debug.Log($"CSV 加载完成，共 {BackgroundLoadingList.Count} 条数据。");
    }
    public BackgroundLoader GetBackgroundLoadingList(string background)
    {
        BackgroundLoader row = BackgroundLoadingList.Find(r => 
        r.Background ==  background
        );

        if (row == null)
        {
            Debug.LogWarning($"Asset Not Found! Name:{background}");
        }

        return row;
    }
    void Start()
    {
        BackgroundLocation = Resources.Load<TextAsset>("TextAssets/CSV Backgrounds");
        LoadFromCsv();
    }
    void Awake()
    {
        Instance = this;
    }
}
