using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLaunch : MonoBehaviour
{
    float countdown;
    float startTime;
    bool startedIgnition = false;
    [SerializeField] GameObject Rocket;
    [SerializeField] float thrust = .1f;
    [SerializeField] GameObject JetParticles;
    [SerializeField] GameObject Tower;
    float emissionValue = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartIgnition();
    }

    private void StartIgnition()
    {
        countdown = 10;
        GetComponent<AudioSource>().Play();
        Rocket.GetComponent<AudioSource>().Play();
        startTime = Time.time;
        startedIgnition = true;
    }

    // Update is called once per frame
    void Update()
    {
        Rocket.GetComponent<AudioSource>().volume = .1f * (Time.time - startTime);
        this.GetComponent<AudioSource>().volume = 1 - .02f * (Time.time - startTime);
        if (Time.time - startTime > 13 && startedIgnition)
        {
            Debug.Log("Lift-Off!");
            Rocket.GetComponent<Rigidbody>().AddForce(transform.up * thrust);

            if (Tower.transform.rotation.x <= .7f)
            {
                Debug.Log(Tower.transform.rotation.x);
                Tower.transform.Rotate(.1f, 0, 0);
            }
            
            var emission = JetParticles.GetComponent<ParticleSystem>().emission;
            emission.rateOverTime = emissionValue;
            if (emissionValue <= 1000)
            {
                emissionValue = 150 * .1f * (Time.time - startTime);
            }
        }
        else
        {
            var emission = JetParticles.GetComponent<ParticleSystem>().emission;
            emission.rateOverTime = emissionValue;
            emissionValue = 50 * .1f * (Time.time - startTime);
        }

    }
}
