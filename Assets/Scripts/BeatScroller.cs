using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;

    public bool hasStarted;
    public GameObject[] beatsHolder;
    // Start is called before the first frame update
    void Start()
    {
        //hard coded for now, will add more precise stuff later 
        for (int i = 0; i < 7; i++)
        {
            beatsHolder[i].SetActive(true);
        }
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
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }


}
