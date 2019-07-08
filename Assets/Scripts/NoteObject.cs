using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public KeyCode keyToPress;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    public AudioSource hitSound;


    public static NoteObject noteInstance;  
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
              
                if(gameObject.tag == "StarPower")
                {
                    Debug.Log("Star Power encountered");
                    GameManager.instance.starPower++;
                }
                else if(gameObject.tag == "StarPower2")
                {
                    Debug.Log("Encounted STARPOWER2");
                    if (GameManager.instance.starPower == 1)
                    {
                        Debug.Log("Update meter");
                        GameManager.instance.starPower = 0;
                        GameManager.instance.starMeter++; 
                        GameManager.instance.UpdateStar(GameManager.instance.starMeter);
                        GameManager.instance.gotStar.Play(); 
                    }
                }
                gameObject.SetActive(false);
                // GameManager.instance.NoteHit();
                if (Mathf.Abs(transform.position.y) > 0.25)
                {
                    //Debug.Log("Normal hit");
                    hitSound.Play();
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if(Mathf.Abs(transform.position.y) > 0.11f)
                {
                    //Debug.Log("Good hit");
                    hitSound.Play();
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else
                {
                    //Debug.Log("Perfect hit!");
                    hitSound.Play();
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;
            if(this.tag == "NewNoteStarter")
            {
                BeatScroller.beatScrollInstance.StartNewNotes(); 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = false;
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            GameManager.instance.NoteMissed();
            gameObject.SetActive(false);
            if(this.tag == "StarPower")
            {
                Debug.Log("Set star power false");
                GameManager.instance.starPower = 0;
            }
        }
    }

    public void PlayHitSound()
    {
        hitSound.Play();
    }

}
