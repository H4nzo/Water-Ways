using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoalType
{
    Gathering,
    Meetup,
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

    public void ItemRetrieved()
    {
        if (goalType == GoalType.Meetup)
            currentAmount++;
    }

}
