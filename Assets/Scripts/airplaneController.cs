using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class airplaneController : MonoBehaviour
{
    public float speed = 0.1f;
    public float elevatorspeed = .7f;
    public float rollspeed = .7f;
    public float turnspeed = .5f;
    public FixedJoystick fj; 
    // Start is called before the first frame update
    void Start()
    {
        fj = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position += this.transform.forward * speed;
        this.transform.Rotate(new Vector3(fj.Vertical * elevatorspeed, 0, -1.0f*fj.Horizontal*rollspeed));
    }
}
