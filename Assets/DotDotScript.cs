using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DotDotScript : MonoBehaviour
{
    int dotNum;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time-startTime > .6f)
        {
            startTime = Time.time;
            AddDot();
        }
    }

    private void AddDot()
    {
        if(dotNum != 3)
        {
            dotNum += 1;
            this.GetComponent<TextMeshProUGUI>().text += ".";
        }
        else
        {
            dotNum = 0;
            this.GetComponent<TextMeshProUGUI>().text = "Looking for image";
        }
    }
}
