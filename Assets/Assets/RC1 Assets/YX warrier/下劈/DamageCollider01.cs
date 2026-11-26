using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YXwarriorAACollider : MonoBehaviour
{
    public MainCharacterHealth MainCharacterHealth;
    public YXwarriermove YXwarrierMove;
    public string triggerTag = "Player";
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(triggerTag) && YXwarrierMove.DamageBool == true)
        {
            if (YXwarrierMove.AnimName == "AirAttack")
            {
                MainCharacterHealth.health -= 1;
                YXwarrierMove.DamageBool = false;
            }
        }
    }
}
