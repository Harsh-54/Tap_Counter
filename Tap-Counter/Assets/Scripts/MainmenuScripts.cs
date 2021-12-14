using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainmenuScripts : MonoBehaviour
{
    //public GameManager gamemanager;
    public GameObject MainMenu;
    public GameObject Credits;
    public GameObject HowToPlay;
    public GameObject Settings;
    public GameObject SettingsBtn;
    public AudioSource gameAudio;
    public AudioClip Clip;
    public Animator Settingsanimator;
    public GameObject FadePanel;
    public GameObject ResetBtn;
    public GameObject Notification;
    public Animator NotificationAnimator;
    public Text ToggleMusicText;
    public static int gameAud;

    void Start()
    {
        Fadein();
        gameAudio.Play();
        MainMenu.SetActive(true);
        Credits.SetActive(false);
        HowToPlay.SetActive(false);
        Settings.SetActive(false);
        Notification.SetActive(false);
        gameAud = PlayerPrefs.GetInt("gameAud",1);
        GameStartAudio();
        if ( gameAudio.isPlaying)
        {        
            ToggleMusicText.text = "Music OFF";

        }
        else
        {
            ToggleMusicText.text = "Music ON";
        }
    }

    public void  ToggleMusic()
    {
        
        if (gameAudio.isPlaying )
        {
            gameAudio.Pause();
            GameplayUI.gameAud = false;
            ToggleMusicText.text = "Music ON";
            gameAud = 0;
        }
        else if (!gameAudio.isPlaying)
        {
            gameAudio.Play();
            //gmui.Audio.Play();
            GameplayUI.gameAud = true;
            ToggleMusicText.text = "Music OFF";
            gameAud = 1;
        }
        PlayerPrefs.SetInt("gameAud", gameAud);
    }

    public void HowToPlaybtnClicked()
    {
        MainMenu.SetActive(false);
        HowToPlay.SetActive(true);
        //Debug.Log("CreditsbtnClicked() called");
        ClickSound();

    }

    public void CreditsbtnClicked()
    {
        MainMenu.SetActive(false);
        Credits.SetActive(true);
        //Debug.Log("HowToPlaybtnClicked() called");
        ClickSound();

    }

    public void SettingsbtnClicked()
    {
        MainMenu.SetActive(false);
        Settings.SetActive(true);
        Debug.Log("SettingsbtnClicked() called");
        SettingsBtn.SetActive(false);
        Settingsanimator.SetTrigger("Slide-In");
        ClickSound();
    }

    public void ResetBtnClicked()
    {
        int LevelNum = 1;
        PlayerPrefs.SetInt("LevelNum", LevelNum);
        Notification.SetActive(true);
        NotificationAnimator.SetTrigger("Notif-Slide");
        ClickSound();
        StartCoroutine(DisableNotificationPanel());
    }
    public IEnumerator DisableNotificationPanel()
    {
        yield return new WaitForSeconds(2);
        Notification.SetActive(false);
    }

    public void BackbtnClicked()
    {
        MainMenu.SetActive(true);
        Credits.SetActive(false);
        HowToPlay.SetActive(false);
        Settingsanimator.SetTrigger("Slide-out");
        //Invoke("Disablesettingspanel", 1);
        StartCoroutine(Disablesettingspanel());
        Debug.Log("BackbtnClicked() called");
        ClickSound();
    }

    public IEnumerator Disablesettingspanel()
    {
        yield return new WaitForSeconds(1);
        Settings.SetActive(false);
        SettingsBtn.SetActive(true);
    }

    public void PlaybtnClicked()
    {
        fadeout();
        StartCoroutine(LoadSceneafterFadeout());
        ClickSound();
    }

    IEnumerator LoadSceneafterFadeout()
    {
        Debug.Log("LoadSceneafterFadeout");
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(1);
    }

    public void ClickSound()
    {
        if (!gameAudio.isPlaying)
        {
            gameAudio.Pause();

        }
        else
        {           
            gameAudio.PlayOneShot(Clip);
        }
        
    }
    public void GameStartAudio()
    {
        if (gameAud ==0)
        {
            gameAudio.Pause();

        }
        else
        {
            gameAudio.Play();
        }
    }

    void Fadein()
    {
        FadePanel.GetComponent<Animator>().SetTrigger("Fade-in");
    }

    void fadeout()
    {
        FadePanel.GetComponent<Animator>().SetTrigger("Fade-out");

    }

    
}

