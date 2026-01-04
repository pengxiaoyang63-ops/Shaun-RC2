using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBack : MonoBehaviour
{
    public string TriggerTag;
    public string TriggerState;
    public string LastAnimName;
    public bool DamageBool;
    public bool HitBool;
    public MainCharacterController MainCharacterController;
    // Start is called before the first frame update
    void Awake()
    {
        MainCharacterController = GetComponentInParent<MainCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MainCharacterController.AnimName == TriggerState && LastAnimName != MainCharacterController.AnimName)
        {
            DamageBool = true;
        }
        LastAnimName = MainCharacterController.AnimName;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(TriggerTag) && DamageBool == true)
        {
            if (MainCharacterController.AnimName == TriggerState)
            {
                HitBool = true;
                DamageBool = false;
            }
        }
    }
}