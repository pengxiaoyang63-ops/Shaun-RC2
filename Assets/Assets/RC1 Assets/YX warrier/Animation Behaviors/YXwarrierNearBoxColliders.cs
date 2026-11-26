using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YXwarrier : MonoBehaviour
{
    public string triggerTag = "Player";
    public bool Isnear;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(triggerTag))
        {
            Isnear = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(triggerTag))
        {
            Isnear = false;
        }
    }
}
