using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractions: MonoBehaviour
{
    public Characters characterData = new Characters();

        public void TakeDamage(int amount)
    {
        characterData.CurrentHealth -= amount;
        
        // 简单的边界检查
        if (characterData.CurrentHealth < 0) 
        {
            characterData.CurrentHealth = 0;
            SetState(Characters.State.Dead);
        }   
    }
    public void SetState(Characters.State newState)
    {
        if (characterData.CurrentState == Characters.State.Dead) return;

        characterData.CurrentState = newState;
        Debug.Log($"{gameObject.name} 状态变更为：{newState}");
    }
    public void SetInteraction(Characters.InteractionState newInteraction)
    {
        characterData.CurrentInteraction = newInteraction;
    }
}

