using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public string HW;
    public int f;
    public int a;
    public int frame;
    // Start is called before the first frame update
    void Start()
    {
        f = 5;

        a = 10; 
    }

    // Update is called once per frame
    void Update()
    {
        if (frame % 10 == 0)
        {
            f *= a;
        }
        frame +=1  ;
    

    }
}
