using System.Collections.Generic;
using UnityEngine;

//Data that needs to be saved in Json
[System.Serializable]
public class SaveData
{
    public Vector2 playerPosition;
    public List<ItemStack> items = new List<ItemStack>();
}

[System.Serializable]
public class ItemStack
{
    public string itemId;
    public int count;
}
