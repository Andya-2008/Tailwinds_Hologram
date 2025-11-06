using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

public class ObjectiveManager : MonoBehaviour
{
    public TMP_Text TextDebug;
    //[SerializeField] List<Image> images = new List<Image>();
    //[SerializeField] List<XRReferenceImageLibrary> XRRefs = new List<XRReferenceImageLibrary>();
    //[SerializeField] List<Image> miniImages = new List<Image>();
    [SerializeField] Canvas objectiveCanvas;
    [SerializeField] Canvas miniCanvas;
    public int num = 0;
    float startTime;
    public bool foundObject = false;
    [SerializeField] GameObject ARSessionOrigin;
    [SerializeField] List<Image> regObjectives = new List<Image>();
    [SerializeField] Slider slider;
    List<Image> currentObjectives = new List<Image>();
    [SerializeField] Image OneWay;
    public bool prize;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < regObjectives.Count; i++)
        {
            regObjectives[i].gameObject.SetActive(false);
        }
        num = PlayerPrefs.GetInt("num");
        if (PlayerPrefs.GetInt("Prize", 0) != 0)
        {
            currentObjectives.Clear();
            currentObjectives.Add(OneWay);
            slider.gameObject.SetActive(false);
            prize = true;
        }
        else
        {
            currentObjectives = regObjectives;
        }
        for (int i = 0; i < currentObjectives.Count; i++)
        {
            if (PlayerPrefs.GetInt(currentObjectives[i].name,0) == 1)
            {
                currentObjectives[i].GetComponent<ImagePulse>().FoundObjectParent.SetActive(true);
            }
        }
        slider.maxValue = currentObjectives.Count - 1;
        //TextDebug.text = num.ToString();
        currentObjectives[0].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FoundObject(string name)
    {
        num++;
        PlayerPrefs.SetInt("num", num);
        PlayerPrefs.Save();
        //TextDebug.text = num.ToString();
        for (int i = 0; i < currentObjectives.Count; i++)
        {
            currentObjectives[i].gameObject.SetActive(false);
        }
        
        for (int i = 0; i < currentObjectives.Count; i++)
        {
            if (name.Contains(currentObjectives[i].gameObject.name))
            {
                currentObjectives[i].GetComponent<ImagePulse>().FoundObjectParent.SetActive(true);
                PlayerPrefs.SetInt(currentObjectives[i].gameObject.name, 1);
                PlayerPrefs.Save();
            }
        }
        if (num >= regObjectives.Count)
        {
            currentObjectives.Clear();
            currentObjectives.Add(OneWay);
            slider.gameObject.SetActive(false);
            PlayerPrefs.SetInt("Prize", Random.Range(1, 14));
            PlayerPrefs.Save();
            prize = true;
            TextDebug.text = "Prize: " + prize;
        }
        currentObjectives[0].gameObject.SetActive(true);
        objectiveCanvas.enabled = true;
            
        GameObject.Find("ARSO").GetComponent<PlaceTrackedImages>().TappedCanvas.enabled = false;
        miniCanvas.enabled = false;


        //images[num - 1].gameObject.SetActive(false);
        slider.value = 0;
            
        //images[num].gameObject.SetActive(true);
        
        ARSessionOrigin.GetComponent<PlaceTrackedImages>().objOn = false;
        
    }
    public void CallNextObjective(string name)
    {
        //ARSessionOrigin.GetComponent<ARTrackedImageManager>().referenceLibrary = XRRefs[num + 1];
        FoundObject(name);
        foundObject = false;
    }
    public void OnSliderChanged()
    {
        for (int i = 0; i < currentObjectives.Count; i++)
        {
            currentObjectives[i].gameObject.SetActive(false);
        }
        currentObjectives[(int)slider.value].gameObject.SetActive(true);
    }
    public void Clicked()
    {
        if (!foundObject)
        {
            if (objectiveCanvas.enabled)
            {
                objectiveCanvas.enabled = false;
                miniCanvas.enabled = true;
                ARSessionOrigin.GetComponent<PlaceTrackedImages>().objOn = true;
            }
            else
            {
                ARSessionOrigin.GetComponent<PlaceTrackedImages>().objOn = false;
                objectiveCanvas.enabled = true;
                miniCanvas.enabled = false;
            }
        }
    }
}
