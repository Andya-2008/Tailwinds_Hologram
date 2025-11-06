using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanLauncher : MonoBehaviour
{
    [SerializeField] GameObject AndrewBlock;
    [SerializeField] float BlockSpeed;
    [SerializeField] GameObject BlockParent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = new Quaternion(Random.Range(-.2f,.2f), Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 1);
        GameObject newBlock = Instantiate(AndrewBlock, this.transform.position, this.transform.rotation);
        newBlock.transform.parent = BlockParent.transform;
        newBlock.GetComponent<Rigidbody>().AddForce(-this.transform.forward * BlockSpeed * 100f);
    }
}
