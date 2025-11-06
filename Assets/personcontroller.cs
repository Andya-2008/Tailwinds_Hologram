using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personcontroller : MonoBehaviour
{

    public float speed = 0.1f;
    public GameObject runner;
    public GameObject walker;
    public GameObject stopper;
    private int perstate = 0;


    public FixedJoystick fj;
    // Start is called before the first frame update
    void Start()
    {
        fj = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("fjvert:" +fj.Vertical +":" + perstate);
        if (fj.Vertical <= 0 && perstate != 0)
        {
            runner.SetActive(false);
            walker.SetActive(false);
            stopper.SetActive(true);
            perstate = 0;

        } else if (fj.Vertical >= .5 && perstate != 2) {
            Debug.Log("Running");
            runner.SetActive(true);
            walker.SetActive(false);
            stopper.SetActive(false);
            perstate = 2;
        }
        else if (fj.Vertical > 0 && fj.Vertical < .5 && perstate != 1)
        {
            Debug.Log("Walking");
            runner.SetActive(false);
            walker.SetActive(true);
            stopper.SetActive(false);
            perstate = 1;
        }
        if (fj.Vertical > 0) { 
            this.transform.position += this.transform.forward * speed;
        }
        this.transform.Rotate(new Vector3(0, fj.Horizontal*2, 0));
    }
}
