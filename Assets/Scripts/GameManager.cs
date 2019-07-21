using System; 
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;
    public AudioSource pauseSound; 
    public bool startPlaying;

    public BeatScroller theBS;

    public static GameManager instance;

    public int currentScore;
    public int scorePerNote = 50;
    public int scorePerGoodNote = 100;
    public int scorePerPerfectNote = 300;
    public int sectionInd;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds; 
    public int lifeMeter;
    public int totalCombo;
    //Text related to HUD
    public Text[] hudText;
    // 0 is scoretext, 1 multiText, 2 starText, 3 comboText, 4 lifeText
    //public Text scoreText, multiText, starText, comboText, lifeText;
    public GameObject[] buttons;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public int starPower, starMeter; 
    public bool starOn; //true if star power is on 
    public GameObject resultsScreen;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;
    public GameObject overScreen;
    public Text overText;

    public AudioSource gotStar;
    public bool alreadyPressed;

    public bool gameOver;

    // Initialize all values at start of song
    void Start()
    {
        instance = this;

        hudText[0].text = "0";
        currentMultiplier = 1;
        hudText[2].text = "STAR: 0";
        hudText[3].text = "0";
        totalNotes = FindObjectsOfType<NoteObject>().Length;
        for (int i = 0; i < 3; i++)
        {
            hudText[i].color = Color.white;
        }
        alreadyPressed = false;
        lifeMeter = 10;
        totalCombo = 0;
        sectionInd = 1;
    }

    void Update()
    {
        /* if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.songStarted = true;
                theBS.keepScrolling = true;
                if (theBS.debugMode == true)
                {
                    //if debug mode true we can skip our start time
                    Debug.Log("In debug mode!");
                    theMusic.time = theBS.skipTime;
                    theBS.SkipStartTime();
                }
                theMusic.Play();
            }
        } */
      

        if (Input.GetKeyDown("space")) //activate star power if we hit space 
        {
            if(starMeter > 0)
            {
                StartCoroutine(ActivateStar(7*starMeter));
            }
            starMeter = 0;
        }

        if (Input.GetKeyDown(KeyCode.Backspace)) //pause button 
        {
            PauseButtonPressed();
        }

        if (!theMusic.isPlaying && !resultsScreen.activeInHierarchy && startPlaying && !alreadyPressed && !gameOver) //results screen 
        {
            resultsScreen.SetActive(true);
            normalsText.text = normalHits.ToString();
            goodsText.text = goodHits.ToString();
            perfectsText.text = perfectHits.ToString();
            missesText.text = missedHits.ToString();
            float totalHit = (float)(normalHits * 0.5 + goodHits * 0.66 + perfectHits); 
            float percentHit = (totalHit / totalNotes) * 100f;
            percentHitText.text = percentHit.ToString("F1") + "%";
            finalScoreText.text = currentScore.ToString();
        }
    }

    public void NoteHit() //goes here whenever notes are hit succesfully
    {
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        { //check to make sure we dont go over lenghth of multipler array
            multiplierTracker++;
            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker) // check to see if we need to move on to next multiplier 
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
 
        }
        if (starOn)
        {
            hudText[1].text = "x" + currentMultiplier * 2;
        }
        else
        {
            hudText[1].text = "x" + currentMultiplier;
        }
        totalCombo++;
        hudText[3].text = "" + totalCombo;
        if(totalCombo % 10 == 0)
        {
            if(lifeMeter < 10) {
                lifeMeter++;
                hudText[4].text = "HP: " + lifeMeter;
            }
        }
 
    }

    public void NormalHit()
    {
        if (starOn)
        {
            currentScore += scorePerNote * currentMultiplier * 2;
        }
        else
        {
            currentScore += scorePerNote * currentMultiplier;
        }
        hudText[0].text = "" + currentScore; 
        NoteHit();
        normalHits++;
    }

    public void GoodHit()
    {
        if (starOn)
        {
            currentScore += scorePerGoodNote * currentMultiplier * 2;
        }
        else
        {
            currentScore += scorePerGoodNote * currentMultiplier;
        }
        hudText[0].text = "" + currentScore;
        NoteHit();
        goodHits++;
    }

    public void PerfectHit()
    {
        if (starOn)
        {
            currentScore += scorePerPerfectNote * currentMultiplier * 2;
        }
        else
        {
            currentScore += scorePerPerfectNote * currentMultiplier;
        }
        hudText[0].text = "" + currentScore;
        NoteHit();
        perfectHits++;
    }

    public void NoteMissed() //goes here whenever notes are missed 
    {
        //Debug.Log("Missed note");
        currentMultiplier = 1;
        multiplierTracker = 0;
        hudText[1].text = "x" + currentMultiplier;
        missedHits++;
        totalCombo = 0;
        hudText[3].text = "" + totalCombo;
        lifeMeter--;
        if(lifeMeter <= 0)
        {
            overScreen.SetActive(true);
            overText.text = "GAME OVER";
            theBS.keepScrolling = false;
            gameOver = true;
            theMusic.Stop();
            Debug.Log("Game over!");
            hudText[4].text = "HP: 0";
        }
        else
        {
            hudText[4].text = "HP: " + lifeMeter;
        } 
    } 

    public void PauseButtonPressed()
    {
        if (alreadyPressed == true) //already in pause so we should unpause
        {
            theMusic.Play();
            alreadyPressed = false;
            theBS.keepScrolling = true;
        }
        else
        { //we must pause 
            theMusic.Pause();
            pauseSound.Play();
            alreadyPressed = true;
            theBS.keepScrolling = false;
        }
    }

    public void UpdateStar(int starLength)
    {
        hudText[2].text = "STAR: " + starLength;
    }

    IEnumerator ActivateStar(int halt)
    {
        starOn = true; //Star meter turned on, 
        for (int i = 0; i < hudText.Length; i++)
        {
            hudText[i].color = Color.blue;
        }
        hudText[2].text = "STAR: ON";
        hudText[3].text = "x" + currentMultiplier*2;
        yield return new WaitForSeconds(halt); //after star meter is done set everything back to normal 
        starOn = false;
        hudText[2].text = "STAR: 0";
        for(int i = 0; i < hudText.Length; i++)
        {
            hudText[i].color = Color.white;
        }
    }

}
