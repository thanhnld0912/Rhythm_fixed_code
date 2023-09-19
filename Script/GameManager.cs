using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private KeyCode keyToPress;

    public bool isPlaying = false;

    [SerializeField] GameObject pauseMenu;

    [SerializeField] private AudioSource theMusic;

    public bool startPlaying;

    public BeatScroller theBS;

    public static GameManager instance;

  

    [SerializeField] private int currentScore;
    [SerializeField] private int scorePerNote = 100;
    [SerializeField] private int scorePerGoodNote = 125;
    [SerializeField] private int scorePerPerfect = 150;


    [SerializeField] private int currentMultiplier;
    [SerializeField] private int multiTracker;
    [SerializeField] private int[] multiHold;



    [SerializeField] private Text scoreText;
    [SerializeField] private Text multiText;


    [SerializeField] private float totalNotes;
    [SerializeField] private float normalHits;
    [SerializeField] private float goodHits;
    [SerializeField] private float perfectHits;
    [SerializeField] private float missedHits;
    [SerializeField] private float tempNotes;

    [SerializeField] private GameObject resultScr;
    [SerializeField] private Text percentHitText;
    [SerializeField] private Text normalHitText;
    [SerializeField] private Text goodHitText;
    [SerializeField] private Text perfectHitText;
    [SerializeField] private Text missedHitText;
    [SerializeField] private Text rankText;
    [SerializeField] private Text finalText;

    public float transitionDuration = 1f;
    public Color transitionColor = Color.black;

    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0";
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;
        tempNotes = FindObjectsOfType<NoteObject>().Length;

    }
    public void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            resultScr.SetActive(false);
            theMusic.Pause();
            isPlaying = false;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (theMusic.isPlaying)
            {
                theMusic.Pause();
                isPlaying = false;
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
                resultScr.SetActive(false);
            }
            
            else
            {
                theMusic.UnPause();
                isPlaying = true;
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
                resultScr.SetActive(false);
            }
            if (!theMusic.isPlaying)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
                resultScr.SetActive(false);
            }
        }
        
        if (!startPlaying)
        {
            
                if (missedHits != 0 || goodHits != 0 || normalHits != 0 || perfectHits != 0)
                {
                    {
                        startPlaying = true;
                        theBS.hasStarted = true;

                        theMusic.Play();
                    }
                }
            
        }
        else
        {
            if(!theMusic.isPlaying && !resultScr.activeInHierarchy && !pauseMenu.activeInHierarchy)
            {
                resultScr.SetActive(true);

                normalHitText.text = "" + normalHits;
                goodHitText.text = goodHits.ToString();
                perfectHitText.text = perfectHits.ToString();
                missedHitText.text = "" + missedHits;

                float totalHit = normalHits + goodHits + perfectHits;
                float percentHit = (totalHit / totalNotes) * 100f;
                
                percentHitText.text = percentHit.ToString("F1") + "%";

                string rankVal = "F";

                if (percentHit > 40)
                {
                    rankVal = "D";
                    if(percentHit > 55)
                    {
                        rankVal = "C";
                        if(percentHit > 70)
                        {
                            rankVal = "B";
                            if(percentHit > 85)
                            {
                                rankVal = "A";
                                if(percentHit > 90)
                                {
                                    rankVal = "S";
                                    if(percentHit > 95)
                                    {
                                        rankVal = "SS";
                                    }
                                }
                            }
                        }
                    }
                }
                rankText.text = rankVal;

                finalText.text = currentScore.ToString();
                

            }
        }
    }

    
    public void advanceToNextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
        resultScr.SetActive(false);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void Home()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
    public void NoteHit()
    {
        Debug.Log("Hit on time");

        if (currentMultiplier - 1 < multiHold.Length)
        {
            multiTracker++;


            if (multiHold[currentMultiplier - 1] <= multiTracker)
            {
                multiTracker = 0;
                currentMultiplier++;
            }
            multiText.text = "Multiplier: x" + currentMultiplier;

            /*
            currentScore += scorePerNote * currentMultiplier;
            */
            scoreText.text = "Score: " + currentScore * currentMultiplier;
            
        }
        
    }
    public void NoteMiss()
    {
        Debug.Log("Miss");

        currentMultiplier = 1;
        multiTracker = 0;
        multiText.text = "Multiplier: x" + currentMultiplier;

        

    }
    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();

        normalHits++;
      
    }
    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();

        goodHits++;

       
    }
    public void PerfectHit()
    {
        currentScore += scorePerPerfect * currentMultiplier;
        NoteHit();

        perfectHits++;
        
    }


    public void MissedNotes()
    {
        
        NoteMiss();
        missedHits++;
    }
}
