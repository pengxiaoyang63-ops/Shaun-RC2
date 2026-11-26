using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDepth : MonoBehaviour
{
    public Transform Targettransform;
    [Range(-1f, 1f)]
    public float Index;
    private Vector3 CurrentPosition;
    private Vector3 StartPosition;
    private Vector3 OriginPosition;
    private Vector2 MoveAmount;

    void Start()
    {
        Vector3 origin = transform.position;
        if (Targettransform == null)
        {
            Targettransform = GameObject.Find("Main Camera").transform;
        }
        Vector3 camPos = Targettransform.position;
        transform.position = new Vector3(
            origin.x - (origin.x - camPos.x) * Index,
            origin.y - (origin.y - camPos.y) * Index,
            0);

        OriginPosition = new Vector3(transform.position.x,transform.position.y,0);
        StartPosition = new Vector3(Targettransform.position.x,Targettransform.position.y,0);
        CurrentPosition = new Vector3(Targettransform.position.x,Targettransform.position.y,0);
        Debug.Log(CurrentPosition);
    }

    void Update()
    {
        CurrentPosition = new Vector3(Targettransform.position.x,Targettransform.position.y,0);
        MoveAmount = new Vector2(CurrentPosition.x - StartPosition.x, CurrentPosition.y - StartPosition.y);
        transform.position = new Vector3(OriginPosition.x + MoveAmount.x * Index, OriginPosition.y + MoveAmount.y * Index, 0);
    }
}
