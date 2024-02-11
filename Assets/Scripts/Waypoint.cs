using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // Time each character spends at this waypoint
    public float idleDuration = 10f;

    [SerializeField]
    private bool isOccupied = false;

       [SerializeField]
    private GameObject visitingNPC;


    public bool Occupy(GameObject npc)
    {
        if (!isOccupied)
        {
            isOccupied = true;
            visitingNPC = npc;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Release()
    {
        isOccupied = false;
        visitingNPC = null;
    }


    public bool IsOccupied()
    {
        return isOccupied;
    }

    public GameObject GetVisitingNPC()
    {
        return visitingNPC;
    }
}
