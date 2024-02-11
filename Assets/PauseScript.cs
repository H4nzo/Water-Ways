using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;
using System;

public class PauseScript : MonoBehaviour
{
    public static bool isPaused = false;

    public UnityEvent OnPause;
    public UnityEvent OnResume;


    private void Update()
    {
        if (isPaused)
            PauseHandler();
        else
            ResumeHandler();
    }

    public void ResumeHandler()
    {
        OnResume?.Invoke();
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseHandler()
    {
        OnPause?.Invoke();
        Time.timeScale = 0f;
        isPaused = true;

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }




}
