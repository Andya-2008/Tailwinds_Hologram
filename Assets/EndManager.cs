using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    [SerializeField] Canvas EndCanvas;
    [SerializeField] Canvas[] Canvases;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGameInitate()
    {
        for(int x = 0; x < Canvases.Length; x++)
        {
            Canvases[x].gameObject.SetActive(false);
        }
        EndCanvas.enabled = true;
    }
}
