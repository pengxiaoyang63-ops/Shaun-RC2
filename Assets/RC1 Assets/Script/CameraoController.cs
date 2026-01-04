using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraoController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float speed;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPos = player.position + offset;
        if (Vector3.Distance(desiredPos, transform.position) > 10)
        {
            transform.position = desiredPos;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, desiredPos, speed * Time.deltaTime);
        }
    }
}
