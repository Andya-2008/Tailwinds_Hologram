using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gamemanager : MonoBehaviour
{
    public TMP_Text txtDebug;
    public GameObject airplane;
    public GameObject Cube;
    public GameObject Human;
    //public Transform ARSessionOriginTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchPlane(Transform planeTransform) 
    {
        Instantiate(airplane, planeTransform);
    }

    public void LaunchHuman(Transform humanTransform)
    {
        GameObject newHuman = Instantiate(Human, humanTransform.transform.position, humanTransform.transform.rotation);
        newHuman.GetComponent<Rigidbody>().AddForce(humanTransform.transform.forward * 150f);
    }
    public void LaunchCubes(Transform cubeTransform)
    {
        Instantiate(Cube, cubeTransform);
    }
}
