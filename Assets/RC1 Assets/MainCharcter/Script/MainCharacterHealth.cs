using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainCharacterHealth : MonoBehaviour
{
    public Slider Slider;
    public GameObject Lose;
    public int health;
    public int health2;
    public Animator animator;
    public MainCharacterController mainCharacterController;
    private CinemachineImpulseSource impulseSource;
    // Start is called before the first frame update
    void Start()
    {
        Slider.enabled = true;
        Lose.SetActive(false);
        Slider.maxValue = 10;
        Slider.minValue = 0;
        impulseSource = GetComponent<CinemachineImpulseSource>();
        animator = GetComponentInChildren<Animator>();
        health = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (health != health2 && health < 10)
        {
            CameraShake.instance.CameraShakeFunction(impulseSource);
            Debug.Log($"Health = {health}");
            mainCharacterController.CurrentHeight = 200;
            animator.SetFloat("RunSpeed",100f);
            animator.SetTrigger("Hitted");
        }
        Slider.value = health;
        health2 = health;
        if (health <= 0)
        {
            Lose.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
