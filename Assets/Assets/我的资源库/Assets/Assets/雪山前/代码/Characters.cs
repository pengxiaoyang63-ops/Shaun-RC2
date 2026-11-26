using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public class Characters
{
    [Header("数值属性")]
    public int Health;
    public int CurrentHealth;

    [Header("行为状态")]
    public State CurrentState = State.Idle;
    public InteractionState CurrentInteraction = InteractionState.Enabled;

    // 定义状态枚举
    public enum State
    {
        Idle,       // 待机
        Dead,       // 死亡
        Wandering,  // 游荡/巡逻
        Aggressive,   // 攻击性 (注：保留了你的拼写，建议改为 Aggressive)
        Weak        // 虚弱 (如低血量、被控制)
    }

    // 定义交互状态枚举
    public enum InteractionState
    {
        Enabled,    // 可交互
        Disabled,    // 不可交互 (注：建议拼写改为 Disabled)
        Talkable    // 可对话
    }
}