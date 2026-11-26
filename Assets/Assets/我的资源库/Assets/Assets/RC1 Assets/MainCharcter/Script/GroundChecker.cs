using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public MainCharacterController MainCharacterController;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            MainCharacterController.animator.SetInteger("Jump", 0);
            MainCharacterController.CurrentHeight = 0;
            MainCharacterController.Isgrounded = true;
            MainCharacterController.Jump = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            MainCharacterController.Isgrounded = false;
        }
    }
}