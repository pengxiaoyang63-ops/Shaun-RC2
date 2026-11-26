using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class WorldState : MonoBehaviour, IDataPersistence
{
 
 public Dictionary<string, int> inventoryItems = new Dictionary<string, int>();

    public SaveManager saveManager;
    public static WorldState Instance { get; private set; }
    public event System.Action OnChanged;

    private void Awake()
    {
        // Only one worldsatee
        Instance = this;
        saveManager.Register(this);
    }

    public void LoadData(SaveData savedata)
    {
        inventoryItems = new Dictionary<string, int>();

        if (savedata.items != null)
        {
            foreach (var item in savedata.items)
            {
                if (string.IsNullOrEmpty(item.itemId))
                    continue;

                int amount = item.count > 0 ? item.count : 1;
                inventoryItems.TryGetValue(item.itemId, out int collected);
                inventoryItems[item.itemId] = collected + amount;
            }
        }

        OnChanged?.Invoke();
    }

    public bool HasItem(string itemId)
    {
        return !string.IsNullOrEmpty(itemId) && inventoryItems.ContainsKey(itemId);
    }

    public int GetCount(string itemId)
    {
        if (string.IsNullOrEmpty(itemId))
            return 0;

        inventoryItems.TryGetValue(itemId, out int count);
        return count;
    }

    public void SaveData(SaveData savedata)
    {
        savedata.items = new List<ItemStack>();
        foreach (var pair in inventoryItems)
            savedata.items.Add(new ItemStack { itemId = pair.Key, count = pair.Value });
    }

    public void AddItem(string itemId, int amount = 1)
    {
        if (string.IsNullOrEmpty(itemId) || amount <= 0) return;

        inventoryItems.TryGetValue(itemId, out int count);
        inventoryItems[itemId] = count + amount;

        OnChanged?.Invoke();
    }

}
