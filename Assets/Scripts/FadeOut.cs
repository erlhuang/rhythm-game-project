using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    SpriteRenderer theSR;
    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    IEnumerator FadingOut()
    {
        for (float f =  1f; f >= -0.05f; f -= 0.05f)
        {
            Color c = theSR.material.color;
            c.a = f;
            theSR.material.color = c;
            yield return new WaitForSeconds(0.05f);

        }
    }

    public void startFading()
    {
        StartCoroutine("FadingOut");
    }
}
