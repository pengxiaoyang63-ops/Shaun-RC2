using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBehavior : StateMachineBehaviour
{
    private int rand;
    public float time;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = 0;
        animator.SetInteger("PostDefenseAction", 3);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time += Time.deltaTime;
        if (Input.GetKey(KeyCode.J))
        {
            animator.SetTrigger("Counter attack");
        }
        if (time > 2)
        {
            rand = Random.Range(0, 101);
            if (rand >= 50)
            {
                animator.SetInteger("PostDefenseAction", 1);
            }
            else
            {
                animator.SetInteger("PostDefenseAction", 3);
                animator.SetTrigger("Fly Back");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
