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

    // Start is called before the first frame update
    void Start()
    {
        beatScrollInstance = this;
        //hard coded for now, will add more precise stuff later 
        for (int i = 0; i < beatsHolder.Length; i++)
        {
            beatsHolder[i].SetActive(true);
        }
        beatTempo = beatTempo / 60f;
        noteTracker = 10;
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
        /* for (int i = noteTracker; i < lengthBeats[lengthBeatInd]; i++)
         {
             beatsHolder[i].SetActive(true);
         }
         noteTracker = noteTracker + lengthBeats.Length; 
         lengthBeatInd++;
         */
        Debug.Log("Special note encountered");
    }


}
