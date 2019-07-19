using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float songTempo;

    public bool songStarted;
    public static BeatScroller beatScrollInstance;
    private int noteTracker;
    public bool debugMode;
    public bool keepScrolling;
    public GameObject[] sections; 
    public float skipTime;
    void Start()
    {
        beatScrollInstance = this;
        songTempo = songTempo / 60f;

    }
    void Update()
    {
        if(songStarted)
        {
            if (keepScrolling == true)
            {
                //physics causes notes to fall at constant speed in conjunction with our set BPM
                transform.position -= new Vector3(0f, songTempo * Time.deltaTime, 0f);
            }
        }
    }

    public void SkipStartTime()
    {
        transform.position -= new Vector3(0f, songTempo * skipTime, 0f);
        //section2.SetActive(true);
        //section3.SetActive(true);
        //section4.SetActive(true);
        //sections[4].SetActive(true);
        //sections[5].SetActive(true);
        //sections[6].SetActive(true);
        //sections[7].SetActive(true);
        sections[8].SetActive(true);
        sections[9].SetActive(true);
    }
   
}
