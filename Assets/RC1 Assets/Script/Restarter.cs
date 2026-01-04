using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour
{
    public MainCharacterController MaincharacterController;
    public MainCharacterHealth MainCharacterHealth;
    public YXWarrierHealth YXWarrierHealth;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            MaincharacterController.Controlled = true;
            Time.timeScale = 0f;
            YXWarrierHealth.health = 0;
        }
        if (Input.GetKey(KeyCode.Space) && YXWarrierHealth.health == 0)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (Input.GetKey(KeyCode.Space) && MainCharacterHealth.health == 0)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
