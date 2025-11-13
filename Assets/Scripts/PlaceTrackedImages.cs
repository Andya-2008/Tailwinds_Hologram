using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
[RequireComponent(typeof(ARTrackedImageManager))]
public class PlaceTrackedImages : MonoBehaviour
{

    [SerializeField]
    public TMP_Text TextDebug;


    private ARTrackedImageManager _trackedImagesManager;
    // Reference to AR tracked image manager component private ARTrackedImageManager _trackedImagesManager;
    // List of prefabs to instantiate
    public GameObject[] ArPrefabs;
    private readonly Dictionary<string, GameObject> _instantiatedPrefabs = new Dictionary<string, GameObject>();
    /*
    private void Start()
    {
        //TextDebug.text = "Start!";
        _trackedImagesManager = GetComponent<ARTrackedImageManager>();
        //TextDebug.text += _trackedImagesManager.enabled.ToString() + ":" + _trackedImagesManager.isActiveAndEnabled.ToString() + ":" + _trackedImagesManager.referenceLibrary.count;
    }

     */
    void Awake()
    {
        //TextDebug = GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>().TextDebug;
        //TextDebug.text = "Woke up!";
        _trackedImagesManager = GetComponent<ARTrackedImageManager>();
    }
    private void OnEnable()
    {
        _trackedImagesManager.trackedImagesChanged += OnTrackedImagesChanged;
    }
    private void OnDisable()
    {
        _trackedImagesManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        //GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>().TextDebug.text = eventArgs.added.Count.ToString() + ":" + eventArgs.updated.Count.ToString() + ":" + eventArgs.removed.Count.ToString();
        foreach (var trackedImage in eventArgs.added)
        {
            var imageName = trackedImage.referenceImage.name;

            foreach (var curPrefab in ArPrefabs)
            {
                //TextDebug.text += "curprefab:" + curPrefab.name;


                if (string.Compare(curPrefab.name, imageName, StringComparison.OrdinalIgnoreCase) == 0 && !_instantiatedPrefabs.ContainsKey(imageName))
                {
                    var newPrefab = Instantiate(curPrefab, trackedImage.transform);
                    _instantiatedPrefabs[imageName] = newPrefab;
                }
            }
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            var imageName = trackedImage.referenceImage.name;
            foreach (var curPrefab in ArPrefabs)
            {
                if (string.Compare(curPrefab.name, imageName, StringComparison.OrdinalIgnoreCase) == 0 && !_instantiatedPrefabs.ContainsKey(imageName))
                {
                    var newPrefab = Instantiate(curPrefab, trackedImage.transform);
                    _instantiatedPrefabs[imageName] = newPrefab;
                }
            }
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            _instantiatedPrefabs[trackedImage.referenceImage.name].SetActive(trackedImage.trackingState == TrackingState.Tracking);
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            Destroy(_instantiatedPrefabs[trackedImage.referenceImage.name]);
            _instantiatedPrefabs.Remove(trackedImage.referenceImage.name);
        }

    }
}