using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGroundChecker : MonoBehaviour
{
    public bool JumpAble;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            JumpAble = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            JumpAble = false;
        }
    }
}
