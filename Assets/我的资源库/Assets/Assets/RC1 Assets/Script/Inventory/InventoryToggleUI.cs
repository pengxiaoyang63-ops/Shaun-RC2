using UnityEngine;
using UnityEngine.InputSystem;
public class InventoryToggleUI : MonoBehaviour
{
    [SerializeField] private Key toggleKey = Key.E;

    private bool isOpen;
    private float timeScaleBeforeOpen = 1f;

    void Start()
    {
        SetOpen(false);
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current[toggleKey].wasPressedThisFrame)
            SetOpen(!isOpen);
    }

    public void SetOpen(bool open)
    {
        isOpen = open;

        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(open);
            
        // // Pause the game when the inventory is open
        // if (open)
        // {
        //     timeScaleBeforeOpen = Time.timeScale;
        //     Time.timeScale = 0f;
        // }
        // else
        // {
        //     Time.timeScale = timeScaleBeforeOpen;
        // }
    }
}
