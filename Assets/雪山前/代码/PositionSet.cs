using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PositionSet : MonoBehaviour
{
    public float Index;
    // Start is called before the first frame update
    void Awake()
    {
        CameraDepth CD = FindObjectOfType<CameraDepth>();
        Index = CD.Index;
        Vector3 CameraPos = GameObject.Find("Main Camera").transform.position;
        transform.position = new Vector3(transform.position.x-(transform.position.x-CameraPos.x)*Index,transform.position.y-(transform.position.y-CameraPos.y)*Index,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
