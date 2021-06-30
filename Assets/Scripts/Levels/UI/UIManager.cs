using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Inst;

    [SerializeField]
    private GameObject _TipsAndTrics;

    [SerializeField]
    private GameObject _GameOverScreen;

    [SerializeField]
    private GameObject _Task;

    [SerializeField]
    private Text _TaskTimeLimit;

    [SerializeField]
    private Text _TaskThreeStarTimeLimit;

    [SerializeField]
    private Text[] _TimeDisplay;

    [SerializeField]
    private Text _ClockText;

    [SerializeField]
    private GameObject _UrineMinigame;

    [SerializeField]
    private GameObject _BloodMinigame;

    [SerializeField]
    private GameObject _WashMinigame;

    [SerializeField]
    private GameObject _FeedMinigame;

    [SerializeField]
    private GameObject _LevelCompleted;

    [SerializeField]
    private Text _LevelCompletedTimeText;

    [SerializeField]
    private GameObject[] _LevelCompletedStars;

    [SerializeField]
    private GameObject _LevelFailed;

    [SerializeField]
    private GameObject _PauseMenu;

    private void Awake()
    {
        Inst = this;

        if(!LevelDataHolder.Inst.TutorialCompleted)
        {
            _TipsAndTrics.SetActive(true);
        }
    }

    public void OnClickCloseTipsAndTricks()
    {
        LevelDataHolder.Inst.TutorialCompleted = true;
        _TipsAndTrics.SetActive(false);
    }

    public void OnClickDisplayOperations()
    {
        ValidateAndStart(() =>
        {
            _Task.SetActive(true);
            _TaskTimeLimit.text = TaskManager.Inst.TaskDuration.ToString() + " SEKUNDI";
            _TaskThreeStarTimeLimit.text = TaskManager.Inst.ThreeStarDuration.ToString() + " SEKUNDI = ";
            TaskManager.Inst.HandCleaned = false;
        });
    }

    public void OnClickStartTask()
    {
        _Task.SetActive(false);
        TaskManager.Inst.StartTask();
    }

    public void UpdateTimer()
    {
        _ClockText.text = TaskManager.Inst.TaskDuration.ToString();
        
        foreach(var timer in _TimeDisplay)
        {
            timer.text = TaskManager.Inst.TaskDuration.ToString() + " SEKUNDI";
        }
    }

    public void OnClickStartUrineMinigame()
    {
        ValidateAndStart(() => _UrineMinigame.gameObject.SetActive(true));
    }

    public void OnClickStartBloodMinigame()
    {
        ValidateAndStart(() => _BloodMinigame.gameObject.SetActive(true));
    }

    public void OnClickHandClean()
    {
        TaskManager.Inst.HandCleaned = true;
    }

    public void OnClickLoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnClickWashMinigame()
    {
        ValidateAndStart(() => _WashMinigame.gameObject.SetActive(true));
    }

    public void OnClickFeedMinigame()
    {
        ValidateAndStart(() => _FeedMinigame.gameObject.SetActive(true));
    }

    public void OnClickOpenPauseMenu()
    {
        Time.timeScale = 0;
        _PauseMenu.SetActive(true);
    }

    public void OnClickClosePauseMenu()
    {
        Time.timeScale = 1;
        _PauseMenu.SetActive(false);
    }

    public void OnClickLoadMainMenuFromPause()
    {
        Time.timeScale = 1;
        LevelDataHolder.Inst.GameLoaded = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void StopMinigames()
    {
        _UrineMinigame.gameObject.SetActive(false);
        _BloodMinigame.gameObject.SetActive(false);
        _WashMinigame.gameObject.SetActive(false);
        _FeedMinigame.gameObject.SetActive(false);
    }

    public void DisplayLevelFailed()
    {
        StopMinigames();
        _LevelFailed.gameObject.SetActive(true);
    }

    internal void DisplayVictoryScreen(int remainingTime, int stars)
    {
        _LevelCompleted.SetActive(true);
        _LevelCompletedTimeText.text = remainingTime.ToString() + " SEKUNDI";

        for(int i=0; i< _LevelCompletedStars.Length; i++)
        {
            if(i+1 == stars)
            {
                _LevelCompletedStars[i].SetActive(true);
            }
            else
            {
                _LevelCompletedStars[i].SetActive(false);
            }
        }
    }

    private void ValidateAndStart(Action valid)
    {
        if(TaskManager.Inst.HandCleaned)
        {
            valid();
        }
        else
        {
            _GameOverScreen.SetActive(true);
        }
    }
}
