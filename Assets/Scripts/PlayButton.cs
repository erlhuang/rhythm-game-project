using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public GameObject titleText;
    public GameObject theButton;
    public GameObject backButton;
    public void OnPlayPressed()
    {
        if (!GameManager.instance.startPlaying)
        {
            StartCoroutine(StartSong(2));
        }
    }

    public void OnBackPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    IEnumerator StartSong(int halt)
    {

        yield return new WaitForSeconds(halt); //after 2 seconds do this
        theButton.SetActive(false);
        GameManager.instance.startPlaying = true;
        BeatScroller.beatScrollInstance.songStarted = true;
        BeatScroller.beatScrollInstance.keepScrolling = true;
        GameManager.instance.theMusic.Play();
        for (int i = 0; i < GameManager.instance.hudText.Length; i++)
        {
            GameManager.instance.hudText[i].gameObject.SetActive(true);
            GameManager.instance.hudText[i].color = Color.white;
        }
        titleText.SetActive(false);
        backButton.SetActive(false);
    }
}
