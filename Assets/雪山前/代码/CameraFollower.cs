using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform Targettransform;
    // Start is called before the first frame update
    void Start()
    {
     Targettransform = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Targettransform.position;
    }
}
