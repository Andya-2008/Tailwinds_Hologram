using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propspinner : MonoBehaviour
{
    public float propspeed = 5.0f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Rotate(0, 0, 1*propspeed);
    }
}
