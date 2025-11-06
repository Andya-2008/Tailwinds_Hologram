using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class PlacePrize : MonoBehaviour
{

    [SerializeField]
    public TMP_Text TextDebug;


    private ARTrackedImageManager _trackedImagesManager;

    public GameObject[] ArPrefabs;
    // Keep dictionary array of created prefabs
    private readonly Dictionary<string, GameObject> _instantiatedPrefabs = new Dictionary<string, GameObject>();
    public bool objOn;
    public Canvas TappedCanvas;
    [SerializeField] Canvas ObjCanvas;
    public GameObject TapImage;
    void Awake()
    {
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
            foreach (var trackedImage in eventArgs.added)
            {
                var imageName = trackedImage.referenceImage.name;
                int prizeNum = PlayerPrefs.GetInt("Prize",0);
                GameObject curPrefab = ArPrefabs[prizeNum];
                if (_instantiatedPrefabs.Count == 0)
                {
                /*
                TappedCanvas.enabled = true;
                ObjCanvas.enabled = false;
                TapImage.SetActive(true);
                */
                    TappedCanvas.gameObject.SetActive(false);
                    var newPrefab = Instantiate(curPrefab, trackedImage.transform);
                    _instantiatedPrefabs[imageName] = newPrefab;
                    GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>().foundObject = true;
                }

            }

            foreach (var trackedImage in eventArgs.updated)
            {
                var imageName = trackedImage.referenceImage.name;
                int prizeNum = PlayerPrefs.GetInt("Prize", 0);
                GameObject curPrefab = ArPrefabs[prizeNum];
                if (_instantiatedPrefabs.Count== 0)
                {
                    TappedCanvas.gameObject.SetActive(false);
                    ObjCanvas.enabled = false;
                    TapImage.SetActive(true);
                    var newPrefab = Instantiate(curPrefab, trackedImage.transform);
                    _instantiatedPrefabs[imageName] = newPrefab;
                    GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>().foundObject = true;
                }

                //GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>().TextDebug.text = "name:" + imageName + "\n" +  "prefab pos:" + _instantiatedPrefabs[imageName].transform.position + "\n" + "image pos:" + trackedImage.transform.position + "\n";
            }

            foreach (var trackedImage in eventArgs.removed)
            {
                //TextDebug.text += "Removed Image";
                _instantiatedPrefabs[trackedImage.referenceImage.name].SetActive(trackedImage.trackingState == TrackingState.Tracking);
            }

            foreach (var trackedImage in eventArgs.removed)
            {
                Destroy(_instantiatedPrefabs[trackedImage.referenceImage.name]);
                _instantiatedPrefabs.Remove(trackedImage.referenceImage.name);
            }
    }
}