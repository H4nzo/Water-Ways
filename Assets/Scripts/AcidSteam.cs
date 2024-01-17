using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSteam : MonoBehaviour
{
     
     void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Player")
           Debug.Log("Detected "+other.name);
      
    }
}
