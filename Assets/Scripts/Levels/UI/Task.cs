using System;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    public bool IsCompleted;

    public GameObject CompletedCheckmark;

    private Button button { get; set; }

    private void Start()
    {
        IsCompleted = false;
        TaskManager.Inst.AllTasks.Add(this);
        button = GetComponent<Button>();

        if(!TaskManager.Inst.TaskStarted)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    public void OnClickSetCurrentTask()
    {
        TaskManager.Inst.CurrentTask = this;
    }

    public void Complete()
    {
        IsCompleted = true;
        CompletedCheckmark.SetActive(true);
        button.interactable = false;
    }
}
