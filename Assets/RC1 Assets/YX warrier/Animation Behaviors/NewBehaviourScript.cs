using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject GameObject;
    public Animator Animator;
    private int rand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rand = Random.Range(0, 2);
        if (rand > 0)
        {
            Animator.ResetTrigger("Spear Spin 0");
            Animator.SetTrigger("Flash");
        }
        else
        {
            Animator.ResetTrigger("Flash");
            Animator.SetTrigger("Spear Spin 0");
        }

    }
    public void StopSpinning()
    {
        Animator.SetBool("Spear Spin Bool", false);
    }
}
