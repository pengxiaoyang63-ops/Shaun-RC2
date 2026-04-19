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
    public Vector2 MoveAmount;
    // Start is called before the first frame update
    void Start()
    {
        Targettransform = GameObject.Find("Main Camera").transform;
        LastPosition = Targettransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentPosition = Targettransform.position;
        MoveAmount = new Vector2(CurrentPosition.x-LastPosition.x,CurrentPosition.y-LastPosition.y);
        transform.position += new Vector3(MoveAmount.x*Index, MoveAmount.y*Index,0);
        LastPosition = Targettransform.position;
    }
}
