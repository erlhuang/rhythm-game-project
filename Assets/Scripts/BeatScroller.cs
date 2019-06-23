using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Timers;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;

    public bool hasStarted;
    public GameObject[] beatsHolder;
    private static System.Timers.Timer aTimer;

    // Start is called before the first frame update
    void Start()
    {
        beatTempo = beatTempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            /*if (Input.anyKeyDown)
            {
                hasStarted = true;
            } */
        }
        else
        {
            aTimer = new System.Timers.Timer(2000);
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }
}
