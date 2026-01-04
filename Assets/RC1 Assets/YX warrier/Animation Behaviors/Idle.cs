using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : StateMachineBehaviour
{
    public YXwarrier YX;
    private int rand;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        YX = animator.GetComponentInChildren<YXwarrier>();
        if (YX.Isnear == false)
        {
            animator.SetBool("DefenseBool", false);
            rand = Random.Range(0, 101);
            if (rand >= 20 && rand <= 70)
            {
                animator.SetTrigger("Flash");
            }
            else if(rand > 70)
            {
                animator.SetTrigger("Fly Back");
            }
            else
            {
                animator.SetTrigger("Dash 02");
            }
        }
        else
        {
            rand = Random.Range(0, 101);
            if (rand <= 30)
            {
                animator.SetBool("DefenseBool", true);
            }
            else if (rand > 30 && rand <= 60)
            {
                animator.SetTrigger("Counter attack");
            }
            else
            {
                animator.SetBool("DefenseBool", false);
                rand = Random.Range(0, 101);
                if (rand <= 40)
                {
                    animator.SetTrigger("Flash");
                }
                else
                {
                    animator.SetTrigger("Fly Back");
                }
            }
            
        }
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
