using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class YXWarrierHealth : MonoBehaviour
{
    public int health;
    public int health2;
    public GameObject Victory;
    public Transform Transform;
    public Animator animator;
    public YXwarriermove CharacterController;
    private CinemachineImpulseSource impulseSource;
    // Start is called before the first frame update
    void Start()
    {
        Victory.SetActive(false);
        health = 40;
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health != health2)
        {
            CameraShake.instance.CameraShakeFunction(impulseSource);
            int rand = UnityEngine.Random.Range(0,360);
            Transform.rotation = Quaternion.AngleAxis(rand, Vector3.forward);
            Debug.Log($"YXHealth = {health}");
            animator.SetTrigger("Hitted");
        }
        health2 = health;
        if (health <= 0)
        {
            Victory.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
