using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public TextMeshProUGUI[] TMPdescriptions;
    public List<string> allQuestDescriptions = new List<string>();

    public void AddQuestDescription(string questDescription)
    {
        allQuestDescriptions.Add(questDescription);
    }

    public void AssignDescription()
    {

        if (TMPdescriptions.Length == allQuestDescriptions.Count)
        {

            for (int i = 0; i < TMPdescriptions.Length; i++)
            {
                TMPdescriptions[i].text = allQuestDescriptions[i];
            }
        }
        else
        {
            return;
        }
    }
}
