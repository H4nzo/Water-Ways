using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Quest
{
    public string description;
    public bool isActive;

    public QuestGoal goal;

    public UnityEvent OnComplete;

    public void Complete()
    {
        OnComplete?.Invoke();
        isActive = false;
    }



}
