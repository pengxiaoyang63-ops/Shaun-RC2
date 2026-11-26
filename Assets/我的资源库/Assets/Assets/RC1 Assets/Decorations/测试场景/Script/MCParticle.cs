using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCParticle : MonoBehaviour
{
    public Transform MCP;
    public Transform Main;
    public MainCharacterController MCC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MCC.Isgrounded == true)
        {
            MCP.position = new Vector2(Main.transform.position.x, MCP.transform.position.y);    
        }
    }
}
