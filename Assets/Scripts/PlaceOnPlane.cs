using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class PlaceOnPlane : MonoBehaviour
{
    private ARPlaneManager _planeManager;
    public GameObject cube;
    // Start is called before the first frame update
    void Awake()
    {
        _planeManager = GetComponent<ARPlaneManager>();
    }
    private void OnEnable()
    {
        _planeManager.planesChanged += OnPlaneChanged;
    }
    private void OnDisable()
    {
        _planeManager.planesChanged -= OnPlaneChanged;
    }

    private void OnPlaneChanged(ARPlanesChangedEventArgs eventArgs)
    {
        foreach (var plane in eventArgs.added)
        {
            GameObject.Find("GameManager").GetComponent<gamemanager>().txtDebug.text = "new plane" + plane.transform.position;
            ARPlane p = plane;
           
            //var newPrefab = Instantiate(cube, p.transform.position + new Vector3(0,1,0), p.transform.rotation);

        }
    }
}
