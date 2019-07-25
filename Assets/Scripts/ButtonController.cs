using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public SpriteRenderer theSR;
    public Sprite defaultImg;
    public Sprite pressedImg;
    public KeyCode keyToPress;

    void Start()
    {
      //  bcInstance = this;
        theSR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            theSR.sprite = pressedImg;
        }

        if (Input.GetKeyUp(keyToPress))
        {
            theSR.sprite = defaultImg;
        }

    }

}
