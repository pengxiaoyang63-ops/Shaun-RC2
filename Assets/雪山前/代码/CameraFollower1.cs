using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower1 : MonoBehaviour
{
    public Transform Targettransform;
    // Start is called before the first frame update
    void Start()
    {
     Targettransform = GameObject.Find("Main Camera (1)").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Targettransform.position = transform.position;
    }
}
