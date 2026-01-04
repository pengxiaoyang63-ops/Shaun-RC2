using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTimeSetter : MonoBehaviour
{
    public ParticleSystem targetPs1;
    public ParticleSystem targetPs2;
    public ParticleSystem targetPs3;
    private ParticleSystem.MainModule P1;
    private ParticleSystem.MainModule P2;
    private ParticleSystem.MainModule P3;
    // Start is called before the first frame update
    void Start()
    {
        P1 = targetPs1.main;
        P2 = targetPs2.main;
        P3 = targetPs3.main;
    }

    // Update is called once per frame
    void Update()
    {
        P1.simulationSpeed = Time.timeScale;
        P2.simulationSpeed = Time.timeScale;
        P3.simulationSpeed = Time.timeScale;
    }
}
