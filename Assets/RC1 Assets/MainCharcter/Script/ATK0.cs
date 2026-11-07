using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATK0 : StateMachineBehaviour
{
    // 进入攻击状态时把 ATK 清 0；
    // 之后攻击过程中按下的 ATK 值必须保留到剪辑结束，
    // 状态机才能用 exitTime=1 + ATK Equals 1 衔接到下一段（ATK 1 -> ATK 2 -> ATK 1 ...）。
    // 原实现在 OnStateUpdate 里每帧清零，会把预输入抹掉，导致永远只能循环 ATK 1。
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Reset1(animator);
    }

    private void Reset1(Animator animator)
    {
        animator.SetFloat("RunSpeed", 1f);
        animator.SetInteger("ATK", 0);
    }
}
