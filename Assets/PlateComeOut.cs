using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateComeOut : MonoBehaviour
{
    [SerializeField] float PlateSpeed;
    [SerializeField] float PlateTime;
    [SerializeField] float WaitTime;
    [SerializeField] GameObject Kitchenette;
    float startTime;
    float waitStartTime;
    bool canMove;
    bool alreadyCalled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && !canMove)
        {
            StartPlate();
        }
        if(Time.time - startTime <= PlateTime && canMove)
        {
            transform.Translate(PlateSpeed, 0, 0);
            alreadyCalled = true;
            waitStartTime = Time.time;
        }

        if (Time.time - waitStartTime >= WaitTime && alreadyCalled)
        {
            GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>().CallNextObjective("Kitchenette");
            alreadyCalled = false;
            Destroy(this.gameObject);
        }
    }

    public void StartPlate()
    {
        startTime = Time.time;
        canMove = true;
    }
}
