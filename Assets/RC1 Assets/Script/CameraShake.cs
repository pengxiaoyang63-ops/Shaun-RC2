using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void CameraShakeFunction(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(1f);
    }
}
