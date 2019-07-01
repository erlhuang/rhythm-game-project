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

    public bool startPlaying;

    public BeatScroller theBS;

    public static GameManager instance;

    public int currentScore;
    public int scorePerNote = 50;
    public int scorePerGoodNote = 100;
    public int scorePerPerfectNote = 300;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds; 

    public Text scoreText;
    public Text multiText;
    public Text starText; 
    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public int starPower; //records the notes in each star power section
    public int starMeter; //our actual star meter 
    public bool starOn; //true if star power is on 
    public GameObject resultsScreen;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText; 

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        scoreText.text = "0";
        currentMultiplier = 1;
        starText.text = "STAR: 0";
        totalNotes = FindObjectsOfType<NoteObject>().Length;
        multiText.color = Color.red;
        scoreText.color = Color.red;
        starText.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;
                if (theBS.debugMode == true)
                {
                    Debug.Log("In debug mode!");
                    theMusic.time = 30f;
                    theBS.SkipStartTime();
                }
                theMusic.Play();
            }
        }
        else
        {
            if(!theMusic.isPlaying && !resultsScreen.activeInHierarchy)
            {
                resultsScreen.SetActive(true);
                normalsText.text = normalHits.ToString();
                goodsText.text = goodHits.ToString();
                perfectsText.text = perfectHits.ToString();
                missesText.text = missedHits.ToString();

                float totalHit =  (float) (normalHits*0.16 + goodHits*0.33 + perfectHits); //modify this with percentages for normal/good 
                float percentHit = (totalHit / totalNotes) * 100f;

                percentHitText.text = percentHit.ToString("F1") + "%";

                finalScoreText.text = currentScore.ToString();
            }
        }

        if (Input.GetKeyDown("space"))
        {
            if(starMeter > 0)
            {
                Debug.Log("Activate Star power!");
                StartCoroutine(ActivateStar(7*starMeter));
            }
            starMeter = 0;
        }
    }

    public void NoteHit() //goes here whenever notes are hit succesfully
    {
        //Debug.Log("Hit on time");


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
            multiText.text = "x" + currentMultiplier * 2;
        }
        else
        {
            multiText.text = "x" + currentMultiplier;
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
        scoreText.text = "" + currentScore; 
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
        scoreText.text = "" + currentScore;
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
        scoreText.text = "" + currentScore;
        NoteHit();
        perfectHits++;
    }

    public void NoteMissed() //goes here whenever notes are missed 
    {
        //Debug.Log("Missed note");
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = "x" + currentMultiplier;

        missedHits++;
    } 

    public void UpdateStar(int starLength)
    {
        starText.text = "STAR: " + starLength;
    }

    IEnumerator ActivateStar(int halt)
    {
        starOn = true;
        multiText.color = Color.blue;
        scoreText.color = Color.blue;
        starText.color = Color.blue;
        starText.text = "STAR: ON";
        multiText.text = "x" + currentMultiplier*2;
        yield return new WaitForSeconds(halt);
        starOn = false;
        starText.text = "STAR: 0";
        multiText.color = Color.red;
        scoreText.color = Color.red;
        starText.color = Color.red;
    }





}
