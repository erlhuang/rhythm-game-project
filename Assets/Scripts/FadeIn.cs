using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public SpriteRenderer theSR;

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        Color c = theSR.material.color;
        c.a = 0f;
        theSR.material.color = c;
    }

    IEnumerator FadingIn()
    {
        for (float f = 0.05f; f <=1; f += 0.05f)
        {
            Color c = theSR.material.color;
            c.a = f;
            theSR.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void startFading()
    {
        StartCoroutine("FadingIn");
    }


}
