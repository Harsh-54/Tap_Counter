using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random; 

public class GameplayUI : MonoBehaviour
{

    public GameManager gm;
    public Text TapCountText;
    public Text TapCountTimer;
    public GameObject GameoverPanel;
    public GameObject GameWinPanel;
    public Text HighScore;
    public Text LevelNum;
    public GameObject ResumePanel;
    public bool IsPaused;
    public Text CountDowntimer;
    public AudioSource Audio;
    public AudioClip SoundClip;
    public AudioClip[] BtntapSounds;
    public Text TargetCount;
    public static bool gameAud;



    void Start()
    {
        GameAudioPlay();
        PlayBtnSound();
    }
    
    public void BackBtnClicked()
    {
        SceneManager.LoadScene(0);
    }


    public void Update()
    {
        TapCountTimer.text = "Timer: " + Math.Round(gm.Timer, 1).ToString();
        if (!gm.CountDowntimerhasended )
        {
            CountDowntimer.text = "Get Ready \n" + Math.Round(gm.CountDowntimer, 0).ToString();
    
        }
       
    }

    public void DisableCountDownTimer()
    {
        CountDowntimer.gameObject.SetActive(false);
    }
    public void UpdateTapCountText()
    {
        //TapCountText.fontSize = CounterFontSize * 2;
        TapCountText.text = gm.tapcount.ToString();
        PlayBtnSound();
        Debug.Log("clicked");


    }

    public void WinOrLosePanel()
    {
        if(gm.Haswon == true)
        {
            GameWinPanel.SetActive(true);           
        }
        else
        {
            GameoverPanel.SetActive(true);
        }
    }

    public void ShowHighScorePanel()
    {
        HighScore.text = "HighScore : " + GameManager.HighScore.ToString();
    }

    public void ShowtargetCountPanel()
    {
        TargetCount.text = "Target " + gm.GetTapTargetCount().ToString();
    }

    public void ShowLevelNumPanel()
    {
        LevelNum.text = "Level " + gm.GetlevelNum().ToString();
    }

    public void Replay()
    {
        SceneManager.LoadScene(1);
    }

    public void Paused()
    {
        PlayBtnSound();
        if (!IsPaused)
        {
            ResumePanel.SetActive(true);
            Time.timeScale = 0;
            IsPaused = true;
        }
        else
        {
            ResumePanel.SetActive(false);
            Time.timeScale = 1;
            IsPaused = false;
        }
        
    }

    public void MainMenuPaused()
    {
        Time.timeScale = 1;
        IsPaused = false;
        SceneManager.LoadScene(0);
        PlayBtnSound();
    }

    public void GameAudioPlay()
    {
        if (gameAud)
        {
            Audio.Play();

        }
        else
        {
            Audio.Pause();
        }

    }

    public void PlayBtnSound()
    {
        int _randomindex = Random.Range(0, BtntapSounds.Length);
        AudioClip _clip = BtntapSounds[_randomindex];
        if (gameAud)
        {
            Audio.PlayOneShot(_clip);

        }
        else
        {
            Audio.Pause();
        }
        
    }

    public void NextLevelBtn()
    {
        gm.IncreaselevelNum();
        Debug.Log("Going to " + gm.GetlevelNum());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
