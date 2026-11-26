using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")==true)
        {
            CharacterInteractions a = GetComponent<CharacterInteractions>();
            if (a != null)
            {
                a.TakeDamage(damage);
            }
        }
    }
}
