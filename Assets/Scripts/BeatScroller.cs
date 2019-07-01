using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;

    public bool hasStarted;
    public GameObject[] beatsHolder;
    public int[] lengthBeats;
    public int lengthBeatInd;
    public static BeatScroller beatScrollInstance;
    private int noteTracker;
    public bool debugMode;
    //public float speedUp; 
    // Start is called before the first frame update
    void Start()
    {
        beatScrollInstance = this;
        //hard coded for now, will add more precise stuff later 
        //if(debugMode != true) {
            for (int i = 0; i < 45; i++)
            {
                beatsHolder[i].SetActive(true);
            }
        //}
        beatTempo = beatTempo / 60f;
        noteTracker = 45;
        lengthBeatInd = 0;
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
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }

    public void StartNewNotes()
    {
        for (int i = noteTracker; i < noteTracker + lengthBeats[lengthBeatInd]; i++)
        {
            beatsHolder[i].SetActive(true);
        }
        noteTracker = noteTracker + lengthBeats[lengthBeatInd];
        lengthBeatInd++;
    }

    public void SkipStartTime()
    {
        transform.position -= new Vector3(0f, beatTempo * 30f, 0f);
        StartNewNotes();
    }
   


}
