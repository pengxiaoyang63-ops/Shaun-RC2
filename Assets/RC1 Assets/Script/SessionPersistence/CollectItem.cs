using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectitem : MonoBehaviour
{
    public string itemId;
    public bool respawnableItem = false;
    public WorldState worldState;

    private void Start()
    {
        worldState = WorldState.Instance;
        SaveManager.OnSaveDataLoaded += Refresh;
        Refresh();
    }

    private void OnDestroy()
    {
        SaveManager.OnSaveDataLoaded -= Refresh;
    }

    private void Refresh()
    {
        if (worldState == null)
            worldState = WorldState.Instance;

        if (worldState == null)
            return;

        gameObject.SetActive(respawnableItem || !worldState.HasItem(itemId));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (worldState == null)
            return;

        worldState.AddItem(itemId);
        gameObject.SetActive(false);
    }

}
