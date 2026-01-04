using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class keyBindConfig : ScriptableObject
{
    [Serializable]
    public class keyBind
    {
        public string keyName;
        public KeyCode keyCode;
    }

    public keyBind[] keyBinds;
    
    public bool isKeyPressed(string name)
    {
        for (int i = 0; i < keyBinds.Length; i++)
        {
            if (keyBinds[i].keyName == name)
            {
                return Input.GetKeyDown(keyBinds[i].keyCode);
            }
        }
        return false;
    }
}
