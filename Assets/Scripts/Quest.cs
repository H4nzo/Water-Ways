using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

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

[System.Serializable]
public class QuestAssist
{
    [Header("This class is optional")]
    [HideInInspector] public string description;
    public TextMeshProUGUI descriptionText_Optional;


}

