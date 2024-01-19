using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{

    private float deltaTime = 0.0f;
    public int maxFPS = 50;

    private void Start() {
        Application.targetFrameRate = maxFPS;
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        int fps = Mathf.RoundToInt(1.0f / deltaTime);
        string text = $"FPS: {fps}";

        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(10, 50, 150, 30);

        style.fontSize = 20;
        style.normal.textColor = Color.red;

        GUI.Label(rect, text, style);
    }



}
