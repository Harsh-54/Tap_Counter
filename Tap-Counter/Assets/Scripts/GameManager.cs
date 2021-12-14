using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    

    public int tapcount;
    public GameplayUI gameplay;
    public float Timer;
    public float DefaultTimer = 30;
    int TargetScore;
    public bool Timerended;
    public bool Haswon;
    public static int HighScore;
    public float CountDowntimer;
    public bool CountDowntimerhasended;
    int LevelNum = 1;
    int BaseLevelMultiplier = 5;
    void Start()
    {
        CountDowntimer = 3;
        CountDowntimerhasended = false;
        Timer = DefaultTimer;
        GetHighScore();
        gameplay.ShowHighScorePanel();
        gameplay.ShowLevelNumPanel();
        gameplay.ShowtargetCountPanel();
        LevelNum= PlayerPrefs.GetInt("LevelNum",1);
        Debug.Log("level no " + LevelNum);
        TargetScore = GetTapTargetCount();

        Debug.Log("target Count " + TargetScore);
    }

    // Update is called once per frame
    void Update()
    {
        if (!CountDowntimerhasended)
        {
            CountDowntimer -= Time.deltaTime;
            if(CountDowntimer <= 0)
            {
                CountDowntimerhasended = true;
                gameplay.DisableCountDownTimer();
            }
        }

        if (CountDowntimerhasended && !Timerended)
        {
            Timer = Timer - Time.deltaTime;
            gameplay.ShowHighScorePanel();
            

            if (Timer <= 0)
            {
                
                Timerended = true;
                Timer = 0;
                if (tapcount >= TargetScore)
                {
                    Haswon = true;
                    
                }
                else
                {
                    Haswon = false;
                }
                if (tapcount > HighScore)
                {
                    HighScore = tapcount;
                }
                SaveHighScore();

                gameplay.WinOrLosePanel();
                gameplay.ShowHighScorePanel();

            }
  

            if (!gameplay.IsPaused && Input.GetMouseButtonDown(0))
            {
                tapcount = tapcount + 1;
                gameplay.UpdateTapCountText();
                gameplay.Update();
            }

        }        
    }

    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", HighScore);
        Debug.Log("HighScore Saved");
    }

    public void GetHighScore()
    {
        HighScore =PlayerPrefs.GetInt("HighScore");
        Debug.Log("HighScore Retreived");
    }

    public int GetTapTargetCount()
    {
        int temp = 0;
        LevelNum = PlayerPrefs.GetInt("LevelNum", 1);
        temp = BaseLevelMultiplier * LevelNum;
        
        return temp;
    }

    public void IncreaselevelNum()
    {
        LevelNum++;
        PlayerPrefs.SetInt("LevelNum", LevelNum);
        
    }
    public int GetlevelNum()
    {
        LevelNum = PlayerPrefs.GetInt("LevelNum", 1);
        
        return LevelNum;
    }

    public void Reset()
    {
        LevelNum = 1;
        PlayerPrefs.SetInt("LevelNum", LevelNum);
    }

   
}
