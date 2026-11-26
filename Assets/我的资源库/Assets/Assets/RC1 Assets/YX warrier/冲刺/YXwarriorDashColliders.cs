using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YXwarriorDashCollider : MonoBehaviour
{
    public MainCharacterHealth MainCharacterHealth;
    public YXwarriermove YXwarrierMove;
    public string triggerTag = "Player";
    public string ClipName;
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(triggerTag) && YXwarrierMove.DamageBool == true)
        {
            if (YXwarrierMove.AnimName == ClipName)
            {
                MainCharacterHealth.health -= 1;
                YXwarrierMove.DamageBool = false;
            }
        }
    }
}
