using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CubeExplosion : MonoBehaviour
{
    [SerializeField] GameObject Sphere;
    [SerializeField] GameObject Scalar;
    [SerializeField] List<GameObject> Cubes = new List<GameObject>();
    float startTime;
    public bool alreadyCalled;
    [SerializeField] float WaitTime = 2f;
    [SerializeField] bool Plane;
    [SerializeField] Transform PlaneInstTransform;
    [SerializeField] bool Cube;
    [SerializeField] Transform CubesInstTransform;
    [SerializeField] bool Human;
    [SerializeField] Transform HumanInstTransform;

    bool AlreadyTapped = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !AlreadyTapped)
        {
            GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>().TextDebug.text += " - Tapped";
            AlreadyTapped = true;
            GameObject.Find("ARSO").GetComponent<PlaceTrackedImages>().TapImage.SetActive(false);
            alreadyCalled = true;
            for (int i = 0; i < Cubes.Count; i++)
            {
                startTime = Time.time;
                Cubes[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            if (Plane)
            {
                GameObject.Find("EnterManager").GetComponent<gamemanager>().LaunchPlane(PlaneInstTransform);
            }
            if (Cube)
            {
                GameObject.Find("EnterManager").GetComponent<gamemanager>().LaunchCubes(CubesInstTransform);
            }
            if (Human)
            {
                GameObject.Find("EnterManager").GetComponent<gamemanager>().LaunchHuman(HumanInstTransform);
            }
            else
            {
                Sphere.GetComponent<Rigidbody>().AddForce(-Scalar.transform.forward * 500000);
            }
        }
        if (GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>().Vertical != 0 || GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>().Horizontal != 0)
        {
            startTime = Time.time;
            WaitTime = 2;
        }
        if(Time.time - startTime >= WaitTime && alreadyCalled)
        {
            GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>().TextDebug.text += "1";
            GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>().CallNextObjective(this.gameObject.name);
            GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>().TextDebug.text += "2";
            alreadyCalled = false;
            Destroy(this.gameObject);
        }
    }

}
