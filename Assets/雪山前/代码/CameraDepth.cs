using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDepth : MonoBehaviour
{
    public Transform Targettransform;
    [Range(-1f, 1f)]
    public float Index;
    public Vector3 LastPosition;
    public Vector3 CurrentPosition;
    public Vector3 StartPosition;
    public Vector3 OriginPosition;
    public Vector2 MoveAmount;
    // Start is called before the first frame update
    void Start()
    {
        OriginPosition = transform.position;
        Targettransform = GameObject.Find("Main Camera").transform;
        StartPosition = Targettransform.position;
        CurrentPosition = Targettransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentPosition = Targettransform.position;
        MoveAmount = new Vector2(CurrentPosition.x-StartPosition.x,CurrentPosition.y-StartPosition.y);
        transform.position = new Vector3(OriginPosition.x+MoveAmount.x*Index,OriginPosition.y+MoveAmount.y*Index,0);
    }
}
