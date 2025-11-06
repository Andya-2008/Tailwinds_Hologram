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
    public GameObject[] Prizes;
    // Keep dictionary array of created prefabs
    private readonly Dictionary<string, GameObject> _instantiatedPrefabs = new Dictionary<string, GameObject>();
    public bool objOn;
    public Canvas TappedCanvas; 
    [SerializeField] Canvas ObjCanvas;
    public GameObject TapImage;
    [SerializeField] GameObject TutorialText;
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

        if (objOn)
        {
            //GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>().TextDebug.text = eventArgs.added.Count.ToString() + ":" + eventArgs.updated.Count.ToString() + ":" + eventArgs.removed.Count.ToString();
            foreach (var trackedImage in eventArgs.added)
            {
                var imageName = trackedImage.referenceImage.name;

                if (imageName.Contains("OneWay") && !_instantiatedPrefabs.ContainsKey(imageName))
                {
                    if (!GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>().prize)
                    {
                        TappedCanvas.enabled = true;
                        ObjCanvas.enabled = false;
                        TapImage.SetActive(true);
                        TutorialText.SetActive(false);
                    }
                    else
                    {
                        GameObject.Find("EndManager").GetComponent<EndManager>().EndGameInitate();
                        TutorialText.SetActive(false);
                    }
                    var newPrefab = Instantiate(Prizes[PlayerPrefs.GetInt("Prize",0)], trackedImage.transform);
                    GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>().foundObject = true;
                    _instantiatedPrefabs[imageName] = newPrefab;
                }
                else
                {
                    foreach (var curPrefab in ArPrefabs)
                    {
                        //TextDebug.text += "curprefab:" + curPrefab.name;


                        if (string.Compare(curPrefab.name, imageName, StringComparison.OrdinalIgnoreCase) == 0 && !_instantiatedPrefabs.ContainsKey(imageName))
                        {
                            if (!GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>().prize)
                            {
                                TappedCanvas.enabled = true;
                                ObjCanvas.enabled = false;
                                TapImage.SetActive(true);
                                TutorialText.SetActive(false);
                            }
                            else
                            {
                                GameObject.Find("EndManager").GetComponent<EndManager>().EndGameInitate();
                                TutorialText.SetActive(false);
                            }
                            var newPrefab = Instantiate(curPrefab, trackedImage.transform);
                            GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>().foundObject = true;
                            _instantiatedPrefabs[imageName] = newPrefab;
                        }
                    }
                }
            }

            foreach (var trackedImage in eventArgs.updated)
            {
                var imageName = trackedImage.referenceImage.name;
                if (imageName.Contains("OneWay") && !_instantiatedPrefabs.ContainsKey(imageName))
                {
                    if (!GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>().prize)
                    {
                        TappedCanvas.enabled = true;
                        ObjCanvas.enabled = false;
                        TapImage.SetActive(true);
                        TutorialText.SetActive(false);
                    }
                    else
                    {
                        GameObject.Find("EndManager").GetComponent<EndManager>().EndGameInitate();
                        TutorialText.SetActive(false);
                    }
                    var newPrefab = Instantiate(Prizes[PlayerPrefs.GetInt("Prize", 0)], trackedImage.transform);
                    GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>().foundObject = true;
                    _instantiatedPrefabs[imageName] = newPrefab;
                }
                else
                {
                    foreach (var curPrefab in ArPrefabs)
                    {
                        if (string.Compare(curPrefab.name, imageName, StringComparison.OrdinalIgnoreCase) == 0 && !_instantiatedPrefabs.ContainsKey(imageName))
                        {
                            if (!GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>().prize)
                            {
                                TappedCanvas.enabled = true;
                                ObjCanvas.enabled = false;
                                TapImage.SetActive(true);
                                TutorialText.SetActive(false);
                            }
                            else
                            {
                                GameObject.Find("EndManager").GetComponent<EndManager>().EndGameInitate();
                                TutorialText.SetActive(false);
                            }
                            var newPrefab = Instantiate(curPrefab, trackedImage.transform);
                            GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>().foundObject = true;
                            _instantiatedPrefabs[imageName] = newPrefab;

                        }
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
}