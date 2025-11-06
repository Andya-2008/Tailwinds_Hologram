using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ImagePulse : MonoBehaviour
{
    [SerializeField] float scaleFactor = .1f;
    [SerializeField] public GameObject FoundObjectParent;
    bool scaled = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.transform.localScale.x <= .7f)
        {
            scaled = true;
        }
        if(this.transform.localScale.x >= 1f)
        {
            scaled = false;
        }
        float currentScaleFactor;
        //print(Mathf.Pow(scaleFactor, 2) * Mathf.Pow(GetComponent<RectTransform>().localScale.x - .85f, 4) + " : " + currentScaleFactor);
        currentScaleFactor = .005f * (scaleFactor + 1 - Mathf.Sin((GetComponent<RectTransform>().localScale.x - .85f) / .85f));
        if (scaled)
        {
            this.GetComponent<RectTransform>().localScale = new Vector3(this.transform.localScale.x + currentScaleFactor, this.transform.localScale.y + currentScaleFactor, this.transform.localScale.z + currentScaleFactor);
        }
        
        //print(currentScaleFactor);
        if (!scaled)
        {
            this.GetComponent<RectTransform>().localScale = new Vector3(this.transform.localScale.x - currentScaleFactor, this.transform.localScale.y - currentScaleFactor, this.transform.localScale.z - currentScaleFactor);
        }
    }
}
