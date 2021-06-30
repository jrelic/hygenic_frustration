using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Inst;

    public bool HandCleaned;

    public int UnlockLevelOnCompleted;

    public int TaskDuration;

    public int TwoStarDuration;

    public int ThreeStarDuration;

    public List<Task> AllTasks { get; set; }

    public Task CurrentTask { get; set; }

    public bool TaskStarted { get; set; }

    private void Awake()
    {
        Inst = this;
        HandCleaned = false;
        AllTasks = new List<Task>();
        TaskStarted = false;
    }

    public void StartTask()
    {
        TaskStarted = true;
        foreach(var task in AllTasks)
        {
            task.Activate();
        }

        StartCoroutine(StartIteration());
    }

    public bool CheckIfAllTasksAreCompleted()
    {
        var completed = true;

        foreach (var task in AllTasks)
        {
            completed &= task.IsCompleted;
        }

        return completed;
    }

    private IEnumerator StartIteration()
    {
        while(TaskDuration > 0)
        {
            UIManager.Inst.UpdateTimer();
            yield return new WaitForSeconds(1);
            TaskDuration--;

            if(CheckIfAllTasksAreCompleted() && HandCleaned)
            {
                var stars = 1;
                if(TaskDuration > ThreeStarDuration)
                {
                    stars = 3;
                }
                else if(TaskDuration > TwoStarDuration)
                {
                    stars = 2;
                }

                UIManager.Inst.DisplayVictoryScreen(TaskDuration, stars);
                LevelDataHolder.Inst.UpdateCurrentLevelData(stars);
                LevelDataHolder.Inst.UnlockLevel(UnlockLevelOnCompleted);
                yield break;
            }
        }

        UIManager.Inst.DisplayLevelFailed();
        yield break;
    }

    public void CompleteCurrentTask()
    {
        if(CurrentTask != null)
        {
            CurrentTask.Complete();
            HandCleaned = false;
        }
        UIManager.Inst.StopMinigames();
    }
}
