using System.Collections;
using System.Collections.Generic;
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

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public GameObject resultsScreen;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText; 

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0";
        currentMultiplier = 1;
        totalNotes = FindObjectsOfType<NoteObject>().Length; 
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
    }

    public void NoteHit() //goes here whenever notes are hit succesfully
    {
        Debug.Log("Hit on time");


        if (currentMultiplier - 1 < multiplierThresholds.Length)
        { //check to make sure we dont go over lenght of multipler array
            multiplierTracker++;
            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker) // check to see if we need to move on to next multiplier 
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
 
        }
        multiText.text = "Multiplier: x " + currentMultiplier; 
        //currentScore += scorePerNote * currentMultiplier; //add to score
        //scoreText.text = "Score: " + currentScore; //update visual score
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore; 
        NoteHit();

        normalHits++;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
        NoteHit();

        goodHits++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
        NoteHit();

        perfectHits++;
    }
    public void NoteMissed() //goes here whenever notes are missed 
    {
        Debug.Log("Missed note");
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = "Multiplier: x " + currentMultiplier;

        missedHits++;
    } 
}
