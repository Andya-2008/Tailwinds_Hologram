using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTest : MonoBehaviour
{

    [SerializeField] public GameObject curPrefab;
    [SerializeField] public Canvas TapCanvas;
    // Start is called before the first frame update
    void Start()
    {
        var newPrefab = Instantiate(curPrefab, this.transform);
        TapCanvas.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
