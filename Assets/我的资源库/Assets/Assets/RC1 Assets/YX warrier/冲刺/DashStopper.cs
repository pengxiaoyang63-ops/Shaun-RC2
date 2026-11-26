using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashStopper : MonoBehaviour
{
    public Transform Hit;
    public Transform DashEffectPosition;
    public YXwarriermove YXwarrierMove;
    public Animator animator;
    public Animator DashHit;
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Wall"))
        {
            if (YXwarrierMove.AnimName == "Dash")
            {
                Hit.position = DashEffectPosition.position;
                if (YXwarrierMove.directionmultiplier == -1)
                {
                    Hit.rotation = Quaternion.AngleAxis(YXwarrierMove.angle-90, Vector3.forward);
                }
                else
                {
                    Hit.rotation = Quaternion.AngleAxis(YXwarrierMove.angle+90, Vector3.forward);
                }
                DashHit.SetTrigger("Hit");
                animator.SetTrigger("DashStopper");
            }
        }
    }
}