using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoalType
{
    Gathering,
    Investigate,
}

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public bool isReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void ItemFound()
    {
        if (goalType == GoalType.Gathering)
            currentAmount++;
    }

    public void ItemInvestigated()
    {
        if (goalType == GoalType.Investigate)
            currentAmount++;
    }

}
