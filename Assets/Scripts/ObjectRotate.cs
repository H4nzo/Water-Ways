using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    [Range(2f, 4f)]
    public float RotationSpeed;

    // Update is called once per frame
    void Update()
    {
        // if (GameController.isPaused == false)
        // {
            transform.Rotate(Vector3.right * RotationSpeed);
        // }
        // else{
        //     Time.timeScale = 0f;
        // }


    }
}
