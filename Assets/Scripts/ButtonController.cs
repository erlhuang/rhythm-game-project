using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer theSR;
    public Sprite defaultImg;
    public Sprite pressedImg;
    //public GameObject specialEffect; 
    public KeyCode keyToPress;
    //public static ButtonController bcInstance;

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

    /*public void playEffect()
    {
        Instantiate(specialEffect, transform.position, specialEffect.transform.rotation);
    } */
}
