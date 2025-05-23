using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    public float timer = 60f;
    private float timerRemaining;
    public TextMeshProUGUI timertext;
    public TextMeshProUGUI Scoretext;
 [HideInInspector] public PlayerController Controller;
    public TextMeshProUGUI gameOver;
    public TextMeshProUGUI timesup;
    public TextMeshProUGUI highscoreText;
    private  bool timerrun;// Make this public to assign in the Inspector
   private static int  Highscore;
    public Button restart;
    public string Scene;
    public AudioClip collide;
    public AudioClip HighPick;
    public AudioClip CollectPower;

    private AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        SCORETEXT();
        timerRemaining = timer;
        timerrun = true;
        Controller = FindObjectOfType<PlayerController>(); // Get the PlayerController instance
        audiosource = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("HighScore"))
        {
            Highscore = PlayerPrefs.GetInt("HighScore");
        }
    }
  
    // Update is called once per frame
    void Update()
    {
        if (timerrun)
        {
            if (timerRemaining>0f && !Controller.gameOver )
            {
                timerRemaining -= Time.deltaTime;
                timerUp();
            }
            else
            {
                timerRemaining = 0;
                timesover();
                timerrun = false;
               
            }
        }
    }

    public void Score(int value)
    {
        if (Controller != null && !Controller.gameOver)
        {
            score += value;
            int hscore = score;
            if (Highscore < hscore)
            {
             Highscore =  hscore;
               PlayerPrefs.SetInt("HighScore", Highscore);
               
            }
            SCORETEXT();
            Hscore();
            Debug.Log("Score: " + score);
        }
    }

    private void timerUp()
    {
        float minutes = Mathf.FloorToInt(timerRemaining / 60);
        float second = Mathf.FloorToInt(timerRemaining % 60);
        timertext.text = string.Format("{0:00}:{1:00}", minutes, second);

    }
   public void SCORETEXT()
    {
        Scoretext.text = string.Format("Score:{00}",score);

    }
    public void GameOver()
    {
        gameOver.text = "Game Over";
        restart.gameObject.SetActive(true);
    }
    public void timesover()
    {
        timesup.text = "Time Up!";
        restart.gameObject.SetActive(true);
    }
    public void Hscore()
    {
        highscoreText.text = string.Format("H-Score:{00}", Highscore);
    }
     public void scenechanger()
    {
        SceneManager.LoadSceneAsync(Scene);

    }
  public  void  highPeak()
    {  if(Controller.powerUp)
        {
            audiosource.PlayOneShot(HighPick);
        } else
        {
            audiosource.PlayOneShot(collide);        }
         }
    public void power()
    {
        audiosource.PlayOneShot(CollectPower);
            }
}