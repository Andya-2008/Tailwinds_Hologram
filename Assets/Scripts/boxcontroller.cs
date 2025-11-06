using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxcontroller : MonoBehaviour
{
    public float speed = 0.1f;

    public FixedJoystick fj;
    // Start is called before the first frame update
    void Start()
    {
        fj = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position += new Vector3(fj.Horizontal * speed, 0, fj.Vertical * speed);
    }
}
