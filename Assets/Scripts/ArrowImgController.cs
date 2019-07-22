using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowImgController : MonoBehaviour
{
    private SpriteRenderer theSR;
    public Sprite defaultImg;
    public Sprite starImg;  

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    //Updates arrow to Star Power sprite if star power is on
    void Update()
    {
        if(GameManager.instance.starOn == true)
        {
            theSR.sprite = starImg; 
        }
        else
        {
            theSR.sprite = defaultImg;
        }
    }
}
