using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
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
            // _TaskTimeLimit.text = TaskManager.Inst.TaskDuration.ToString() + " SEKUNDI";
            // _TaskThreeStarTimeLimit.text = TaskManager.Inst.ThreeStarDuration.ToString() + " SEKUNDI = ";
            setLocalText("broj_sekundi", _TaskTimeLimit, new Dictionary<string, object> { ["NUM_SEC"] = TaskManager.Inst.TaskDuration.ToString() });
            setLocalText("broj_sekundi", _TaskThreeStarTimeLimit, new Dictionary<string, object> { ["NUM_SEC"] = TaskManager.Inst.ThreeStarDuration.ToString() });
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
            // timer.text = TaskManager.Inst.TaskDuration.ToString() + " SEKUNDI";
            setLocalText("broj_sekundi", timer, new Dictionary<string, object> { ["NUM_SEC"] = TaskManager.Inst.TaskDuration.ToString()});
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
        // _LevelCompletedTimeText.text = remainingTime.ToString() + " SEKUNDI";
        setLocalText("broj_sekundi", _LevelCompletedTimeText, new Dictionary<string, object> { ["NUM_SEC"] = remainingTime.ToString() });

        for (int i=0; i< _LevelCompletedStars.Length; i++)
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

    private void setLocalText(string key, Text textComponent, Dictionary<string, object> dict)
    {
        IList<object>  args = new List<object> { dict };
        AsyncOperationHandle<string> op = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("UI", key, args);
        if (op.IsDone)
        {
            textComponent.text = op.Result;
        }
        else
        {
            op.Completed += (o) => textComponent.text = o.Result;
        }
    }
}
